using Gerontocracy.Core.BusinessObjects.Account;
using System;

namespace Gerontocracy.Core.BusinessObjects.Affair
{
    public class VorfallData
    {
        public long Id { get; set; }
        public string Titel { get; set; }
        public DateTime ErstelltAm { get; set; }
        public Account.User ErstelltVon { get; set; }
    }
}
