using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Linq;
using Gerontocracy.Core.BusinessObjects.Sync;
using CodeHollow.FeedReader;
using System.Text.RegularExpressions;

namespace Gerontocracy.Core.Strategies.Sync
{
    internal class AustriaImporter : IParlamentImporter
    {
        private readonly string UrlParteien = "https://www.data.gv.at/katalog/dataset/3179c5b2-9bb5-4a7f-a573-5491ccb0110b/resource/c6fa1a8f-d506-4cc3-b9d4-e668221fb949/download/parteienreihungn1710.csv";
        private readonly string UrlNationalrat = "https://www.parlament.gv.at/WWER/NR/AKT/filter.psp?view=RSS&jsMode=&xdocumentUri=&filterJq=&view=&FUNK=ALLE&R_WF=FR&FR=ALLE&R_PBW=PLZ&PLZ=&W=W&M=M&listeId=2&FBEZ=FW_002";
        private readonly string UrlRegierung = "https://www.parlament.gv.at/WWER/BREG/filter.psp?view=RSS&jsMode=&xdocumentUri=&filterJq=&view=&FUNK=ALLE&RESS=ALLE&SUCH=&R_ZEIT=AKT&listeId=18&FBEZ=FW_018";

        public Parlament GetParlament(IHttpClientFactory clientFactory)
            => new Parlament()
            {
                Code = "AT",
                Langtext = "Österreichisches Parlament"
            };

        public List<Partei> GetParteien(IHttpClientFactory clientFactory)
            => LoadParteien(clientFactory);

        public List<Politiker> GetPolitiker(IHttpClientFactory clientFactory)
            => LoadPolitiker(UrlNationalrat).Concat(LoadPolitiker(UrlRegierung)).ToList();

        private List<Partei> LoadParteien(IHttpClientFactory clientFactory)
        {
            var result = new List<Partei>();

            var client = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, UrlParteien);
            var response = client.SendAsync(request).Result;

            if (response.IsSuccessStatusCode)
            {
                var buffer = response.Content.ReadAsByteArrayAsync().Result;

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

        private List<Politiker> LoadPolitiker(string url)
        {
            var result = new List<Politiker>();

            var feed = FeedReader.ReadAsync(url).Result;

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
                    Name = dict.GetValueOrDefault("Name"),
                    Bundesland = dict.GetValueOrDefault("Bundesland"),
                    ParteiKurzzeichen = dict.GetValueOrDefault("Fraktion"),
                    Wahlkreis = dict.GetValueOrDefault("Wahlkreis")
                });
            }

            return result;
        }
    }
}
