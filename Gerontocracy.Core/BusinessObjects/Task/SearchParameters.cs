namespace Gerontocracy.Core.BusinessObjects.Task
{
    public class SearchParameters
    {
        public string Username { get; set; }

        public bool IncludeDone { get; set; }

        public TaskType? TaskType { get; set; }
    }
}
