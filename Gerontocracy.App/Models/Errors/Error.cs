namespace Gerontocracy.App.Models.Errors
{
    /// <summary>
    /// Describes an Error Dto
    /// </summary>
    public abstract class Error
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">The hex error-code</param>
        public Error(string code) => this.Code = code;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">The hex error-code</param>
        /// <param name="title">The title</param>
        public Error(string code, string title) : this(code) => this.Title = title;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">The hex error-code</param>
        /// <param name="title">The title</param>
        /// <param name="description">the specified description</param>
        public Error(string code, string title, string description) : this(code, title) => this.Description = description;

        /// <summary>
        /// The hex error-code
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// The title
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The specified description
        /// </summary>
        public string Description { get; }
    }
}
