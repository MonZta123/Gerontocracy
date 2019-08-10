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
using Gerontocracy.Data.Entities.News;
using en = Gerontocracy.Data.Entities;

namespace Gerontocracy.Core.Providers
{
    public class SyncService : ISyncService
    {
        #region Fields

        private readonly GerontocracySettings _gerontocracySettings;

        private readonly IHttpClientFactory _clientFactory;

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
            var polLoad = await LoadNationalratPolitiker();

            using (var context = new ContextFactory().CreateDbContext())
            {
                context.Database.BeginTransaction();

                var parDb = context.Partei.ToList();

                parDb.ToList().ForEach(n =>
                {
                    var newObject = parLoad.SingleOrDefault(m => m.ExternalId == n.ExternalId);
                    if (newObject != null)
                    {
                        n.Name = newObject.Name;
                        n.Kurzzeichen = newObject.Kurzzeichen;
                        n.ExternalId = newObject.ExternalId;
                    }
                });

                var parNew = parLoad
                    .Where(n => !parDb.Select(m => m.ExternalId).Contains(n.ExternalId))
                    .Select(n => new en.Party.Partei()
                    {
                        Kurzzeichen = n.Kurzzeichen,
                        ExternalId = n.ExternalId,
                        Name = n.Name,
                        Id = parDb.SingleOrDefault(m => m.ExternalId == n.ExternalId)?.Id ?? 0
                    })
                    .ToList();

                context.AddRange(parNew.Where(n => n.Id == 0).ToList());
                context.SaveChanges();

                var polDb = context.Politiker.ToList();

                polDb.ToList().ForEach(n =>
                {
                    var newObject = polLoad.SingleOrDefault(m => m.ExternalId == n.ExternalId);
                    if (newObject != null)
                    {
                        n.Nachname = newObject.Nachname;
                        n.Vorname = newObject.Vorname;
                        n.Wahlkreis = newObject.Wahlkreis;
                        n.Bundesland = newObject.Bundesland;
                        n.AkadGradPost = newObject.AkadGradPost;
                        n.AkadGradPre = newObject.AkadGradPre;
                        n.ExternalId = newObject.ExternalId;
                        n.ParteiId = parNew.Concat(parDb)
                            .SingleOrDefault(m => m.Kurzzeichen == newObject.ParteiKurzzeichen)?.Id;
                        n.IsNationalrat = true;
                    }
                    else
                    {
                        n.IsNationalrat = false;
                    }
                });

                var polNew = polLoad.Where(n => !polDb.Select(m => m.ExternalId).Contains(n.ExternalId));
                var polNewMapped = polNew.Select(n => new en.Party.Politiker
                {
                    Nachname = n.Nachname,
                    Vorname = n.Vorname,
                    Wahlkreis = n.Wahlkreis,
                    Bundesland = n.Bundesland,
                    AkadGradPost = n.AkadGradPost,
                    AkadGradPre = n.AkadGradPre,
                    ExternalId = n.ExternalId,
                    ParteiId = parNew.Concat(parDb).SingleOrDefault(m => m.Kurzzeichen == n.ParteiKurzzeichen)?.Id,
                    IsNationalrat = true
                });

                context.AddRange(polNewMapped.Where(n => n.Id == 0).ToList());
                context.SaveChanges();

                context.Database.CommitTransaction();
            }
        }

        public async Task SyncApa()
        {
            var feed = await FeedReader.ReadAsync("https://www.ots.at/rss/politik");

            var newItems = feed.Items.Select(n => new Artikel()
            {
                Author = n.Author,
                Description = n.Description,
                Link = n.Link,
                Title = n.Title,
                PubDate = n.PublishingDate,
                Identifier = n.Id
            })
            .ToList();

            var identifiers = newItems.Select(n => n.Identifier);

            using (var context = new ContextFactory().CreateDbContext())
            {
                var availableIds = context.Artikel.Select(n => n.Identifier).Intersect(identifiers).ToList();

                newItems = newItems.Where(n => !availableIds.Contains(n.Identifier)).ToList();

                context.AddRange(newItems);
                context.SaveChanges();
            }
        }

        private async Task<List<Politiker>> LoadNationalratPolitiker()
        {
            var result = new List<Politiker>();

            var feed = await FeedReader.ReadAsync(_gerontocracySettings.UrlNationalrat);

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

        #endregion Methods
    }
}
