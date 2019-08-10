using System.Collections.Generic;

namespace Gerontocracy.Core.BusinessObjects.Party
{
    public class ParteiSelection
    {
        public long Id { get; set; }
        public string Kurzzeichen { get; set; }
        public string Name { get; set; }

        public List<PolitikerSelection> Politiker { get; set; }
    }
}
