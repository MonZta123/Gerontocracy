using System;

namespace Gerontocracy.Core.BusinessObjects.Task
{
    public class AufgabeDetail
    {
        public long Id { get; set; }
        public TaskType TaskType { get; set; }
        public string Beschreibung { get; set; }
        public DateTime EingereichtAm { get; set; }
        public string MetaData { get; set; }
        public bool Erledigt { get; set; }

        public long EinreicherId { get; set; }
        public string Einreicher { get; set; }

        public long? BearbeiterId { get; set; }
        public string Bearbeiter { get; set; }
    }
}
