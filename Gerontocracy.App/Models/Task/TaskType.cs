namespace Gerontocracy.App.Models.Task
{
    /// <summary>
    /// The type of the task
    /// </summary>
    public enum TaskType
    {
        /// <summary>
        /// Type is an affair report
        /// </summary>
        AffairReport,

        /// <summary>
        /// Task is a post report
        /// </summary>
        PostReport,

        /// <summary>
        /// Task is a user report
        /// </summary>
        UserReport,

        /// <summary>
        /// Task is to combine affair and thread
        /// </summary>
        AffairThreadTask
    }
}
