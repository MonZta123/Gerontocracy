using Gerontocracy.Core.BusinessObjects.Sync;
using System.Collections.Generic;
using System.Net.Http;

namespace Gerontocracy.Core.Strategies.Sync
{
    internal interface IParlamentImporter
    {
        Parlament GetParlament(IHttpClientFactory clientFactory);

        List<Partei> GetParteien(IHttpClientFactory clientFactory);

        List<Politiker> GetPolitiker(IHttpClientFactory clientFactory);
    }
}
