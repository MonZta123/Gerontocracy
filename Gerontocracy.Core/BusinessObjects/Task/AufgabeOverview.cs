using System;

namespace Gerontocracy.Core.BusinessObjects.Task
{
    public class AufgabeOverview
    {
        public long Id { get; set; }
        public TaskType TaskType { get; set; }
        public DateTime EingereichtAm { get; set; }
        public string Einreicher { get; set; }
        public bool Uebernommen { get; set; }
        public bool Erledigt { get; set; }
    }
}
