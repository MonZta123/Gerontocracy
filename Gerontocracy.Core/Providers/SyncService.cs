using Gerontocracy.Core.BusinessObjects.Sync;
using Gerontocracy.Core.Interfaces;
using Gerontocracy.Data;

using CodeHollow.FeedReader;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Gerontocracy.Core.Config;
using Gerontocracy.Data.Entities.Global;
using Gerontocracy.Data.Entities.News;

using en = Gerontocracy.Data.Entities;

namespace Gerontocracy.Core.Providers
{
    public class SyncService : ISyncService
    {
        #region Fields

        private readonly IHttpClientFactory _clientFactory;
        private readonly GerontocracySettings _gerontocracySettings;
        #endregion Fields

        #region Constructors

        public SyncService(
            GerontocracySettings gerontocracySettings,
            IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _gerontocracySettings = gerontocracySettings;
        }

        #endregion Constructors

        #region Methods

        public async Task SyncPolitiker()
        {
            var parLoad = await LoadParteien();
            var polNationalratLoad = await LoadPolitiker(_gerontocracySettings.UrlNationalrat);
            var polRegierungLoad = await LoadPolitiker(_gerontocracySettings.UrlRegierung);

            using (var context = new ContextFactory().CreateDbContext())
            {
                context.Database.BeginTransaction();

                var parliament = EnsureParliamentCreated(context);
                UpdateParteien(context, parLoad, parliament.Id);
                UpdatePolitiker(context, polNationalratLoad, parliament.Id);
                UpdatePolitiker(context, polRegierungLoad, parliament.Id);

                context.Database.CommitTransaction();
            }
        }

        public async Task SyncSource(long id)
        {
            using (var context = new ContextFactory().CreateDbContext())
            {
                var source = context.RssSource.Single(n => n.Id == id);
                await Sync(context, source);
                context.SaveChanges();
            }
        }

        public async Task SyncSources()
        {
            using (var context = new ContextFactory().CreateDbContext())
            {
                var sources = context.RssSource.ToList();

                foreach (var source in sources)
                    await Sync(context, source);

                context.SaveChanges();
            }
        }

        private async Task<List<Partei>> LoadParteien()
        {
            var result = new List<Partei>();

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _gerontocracySettings.UrlParteien);
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var buffer = await response.Content.ReadAsByteArrayAsync();

                var data = CodePagesEncodingProvider.Instance.GetEncoding(1252)
                    .GetString(buffer, 0, buffer.Length)
                    .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1);

                result = data
                    .Select(n =>
                    {
                        var tokens = n.Split(';');

                        return new Partei()
                        {
                            ExternalId = Convert.ToInt64(tokens[2]),
                            Kurzzeichen = tokens[1],
                            Name = tokens[12]
                        };
                    })
                    .ToList();
            }

            return result;
        }

        private Parlament EnsureParliamentCreated(GerontocracyContext context)
        {
            var dbObj = context.Parlament.SingleOrDefault(n => n.Code.Equals("AT", StringComparison.CurrentCultureIgnoreCase));
            if (dbObj == null)
            {
                dbObj = new Parlament()
                {
                    Code = "AT",
                    Langtext = "Österreichisches Parlament"
                };

                context.Add(dbObj);
                context.SaveChanges();
            }

            return dbObj;
        }

        private async Task<List<Politiker>> LoadPolitiker(string url)
        {
            var result = new List<Politiker>();

            var feed = await FeedReader.ReadAsync(url);

            foreach (var item in feed.Items)
            {
                var desc = item.Description.Replace("\n", string.Empty);
                var tokens = desc.Split("<br />", StringSplitOptions.RemoveEmptyEntries);

                var dict = tokens
                    .Select(n => n.Trim())
                    .ToDictionary(
                        n => n.Split(":")[0].Trim(),
                        n => Regex.Replace(n.Split(":")[1].Trim(), "<.*?>", string.Empty)
                    );

                result.Add(new Politiker
                {
                    ExternalId = Convert.ToInt64(item.Link.Split("/")[4].Split("_")[1]),
                    Vorname = dict.GetValueOrDefault("Vorname"),
                    Nachname = dict.GetValueOrDefault("Nachname"),
                    AkadGradPost = dict.GetValueOrDefault("Ak. Grad nachg."),
                    AkadGradPre = dict.GetValueOrDefault("Ak. Grad"),
                    Bundesland = dict.GetValueOrDefault("Bundesland"),
                    ParteiKurzzeichen = dict.GetValueOrDefault("Fraktion"),
                    Wahlkreis = dict.GetValueOrDefault("Wahlkreis")
                });
            }

            return result;
        }

        private async Task Sync(GerontocracyContext context, RssSource source)
        {
            var feed = await FeedReader.ReadAsync(source.Url);

            var newItems = feed.Items.Select(n => new Artikel()
            {
                Author = n.Author,
                Description = n.Description,
                Link = n.Link,
                Title = n.Title,
                PubDate = n.PublishingDate,
                Identifier = n.Id,
                RssSourceId = source.Id
            }).ToList();

            var identifiers = newItems.Select(n => n.Identifier);

            var availableIds = context.Artikel
                .Select(n => n.Identifier)
                .Intersect(identifiers)
                .ToList();

            newItems = newItems.Where(n => !availableIds.Contains(n.Identifier)).ToList();

            context.AddRange(newItems);
        }

        private void UpdateParteien(GerontocracyContext context, List<Partei> parteien, long parlamentId)
        {
            var parDb = context.Partei.ToList();

            parDb.ToList().ForEach(n =>
            {
                var newObject = parteien.SingleOrDefault(m => m.ExternalId == n.ExternalId);
                if (newObject != null)
                {
                    n.Name = newObject.Name;
                    n.Kurzzeichen = newObject.Kurzzeichen;
                    n.ExternalId = newObject.ExternalId;
                    n.ParlamentId = parlamentId;
                }
            });

            var parNew = parteien
                .Where(n => !parDb.Select(m => m.ExternalId).Contains(n.ExternalId))
                .Select(n => new en.Party.Partei()
                {
                    Kurzzeichen = n.Kurzzeichen,
                    ExternalId = n.ExternalId,
                    Name = n.Name,
                    Id = parDb.SingleOrDefault(m => m.ExternalId == n.ExternalId)?.Id ?? 0,
                    ParlamentId = parlamentId
                })
                .ToList();

            context.AddRange(parNew.Where(n => n.Id == 0).ToList());
            context.SaveChanges();
        }

        private void UpdatePolitiker(GerontocracyContext context, List<Politiker> politiker, long parlamentId)
        {
            var polDb = context.Politiker.ToList();
            var partys = context.Partei.ToList();

            polDb.ToList().ForEach(n =>
            {
                var newObject = politiker
                    .SingleOrDefault(m => m.ExternalId == n.ExternalId);

                if (newObject != null)
                {
                    n.Nachname = newObject.Nachname;
                    n.Vorname = newObject.Vorname;
                    n.Wahlkreis = newObject.Wahlkreis;
                    n.Bundesland = newObject.Bundesland;
                    n.AkadGradPost = newObject.AkadGradPost;
                    n.AkadGradPre = newObject.AkadGradPre;
                    n.ExternalId = newObject.ExternalId;
                    n.ParteiId = partys.SingleOrDefault(m => m.Kurzzeichen == newObject.ParteiKurzzeichen)?.Id;
                    n.ParlamentId = parlamentId;
                }
            });

            var polNewMapped = politiker
                .Where(n => !polDb.Select(m => m.ExternalId)
                .Contains(n.ExternalId))
                .Select(n => new en.Party.Politiker
                {
                    Nachname = n.Nachname,
                    Vorname = n.Vorname,
                    Wahlkreis = n.Wahlkreis,
                    Bundesland = n.Bundesland,
                    AkadGradPost = n.AkadGradPost,
                    AkadGradPre = n.AkadGradPre,
                    ExternalId = n.ExternalId,
                    ParteiId = partys.SingleOrDefault(m => m.Kurzzeichen == n.ParteiKurzzeichen)?.Id,
                    ParlamentId = parlamentId
                });

            context.AddRange(polNewMapped.Where(n => n.Id == 0).ToList());
            context.SaveChanges();
        }
        #endregion Methods
    }
}
