using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Gerontocracy.Core.BusinessObjects.Sync;
using Gerontocracy.Core.Config;
using Gerontocracy.Core.Interfaces;
using Gerontocracy.Data;
using Gerontocracy.Data.Entities.News;

using CodeHollow.FeedReader;

using db = Gerontocracy.Data.Entities;

namespace Gerontocracy.Core.Providers
{
    internal class ExternalIdComparer : IEqualityComparer<Politiker>
    {
        #region Methods

        public bool Equals(Politiker x, Politiker y) => y != null && x != null && x.ExternalId == y.ExternalId;
        public int GetHashCode(Politiker obj) => obj.ExternalId.GetHashCode();

        #endregion Methods
    }

    internal class SyncService : ISyncService
    {
        #region Fields

        private readonly IHttpClientFactory _clientFactory;
        private readonly GerontocracySettings _gerontocracySettings;
        private readonly ImporterRepository _importerRepository;
        #endregion Fields

        #region Constructors

        public SyncService(
            GerontocracySettings gerontocracySettings,
            IHttpClientFactory clientFactory,
            ImporterRepository importerRepository)
        {
            _clientFactory = clientFactory;
            _gerontocracySettings = gerontocracySettings;
            _importerRepository = importerRepository;
        }

        #endregion Constructors

        #region Methods

        public void SyncPolitiker()
        {
            foreach (var importer in _importerRepository.Importers)
            {
                var parlament = importer.GetParlament(_clientFactory);
                var parteien = importer.GetParteien(_clientFactory);
                var politiker = importer.GetPolitiker(_clientFactory);

                using (var context = new ContextFactory().CreateDbContext())
                {
                    context.Database.BeginTransaction();

                    var parliament = EnsureParliamentCreated(context, parlament);
                    UpdateParteien(context, parteien, parliament.Id);
                    UpdatePolitiker(context, politiker, parliament.Id);

                    context.Database.CommitTransaction();
                }
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
                var sources = context.RssSource.Where(n => n.Enabled).ToList();

                foreach (var source in sources)
                    await Sync(context, source);

                context.SaveChanges();
            }
        }

        private db.Party.Parlament EnsureParliamentCreated(GerontocracyContext context, Parlament parlament)
        {
            var dbObj = context.Parlament.SingleOrDefault(n => n.Code.Equals(parlament.Code, StringComparison.CurrentCultureIgnoreCase));
            if (dbObj == null)
            {
                dbObj = new db.Party.Parlament()
                {
                    Code = parlament.Code,
                    Langtext = parlament.Langtext
                };

                context.Add(dbObj);
                context.SaveChanges();
            }

            return dbObj;
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
                .Select(n => new db.Party.Partei()
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
            var newPol = politiker.Distinct(new ExternalIdComparer()).ToList();

            polDb.ToList().ForEach(n =>
            {
                var newObject = newPol
                    .SingleOrDefault(m => m.ExternalId == n.ExternalId);

                if (newObject != null)
                {
                    n.Name = newObject.Name;
                    n.Wahlkreis = newObject.Wahlkreis;
                    n.Bundesland = newObject.Bundesland;
                    n.ExternalId = newObject.ExternalId;
                    n.ParteiId = partys.SingleOrDefault(m => m.Kurzzeichen == newObject.ParteiKurzzeichen)?.Id;
                    n.ParlamentId = parlamentId;
                    n.IsInactive = false;
                }
                else
                {
                    n.IsInactive = true;
                }
            });

            var polNewMapped = newPol
                .Where(n => !polDb.Select(m => m.ExternalId)
                .Contains(n.ExternalId))
                .Select(n => new db.Party.Politiker
                {
                    Name = n.Name,
                    Wahlkreis = n.Wahlkreis,
                    Bundesland = n.Bundesland,
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
