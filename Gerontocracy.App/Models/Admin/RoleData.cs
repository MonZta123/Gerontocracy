namespace Gerontocracy.App.Models.Admin
{
    /// <summary>
    /// Data required for granting or revoking a role from a user
    /// </summary>
    public class RoleData
    {
        /// <summary>
        /// user id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// role name
        /// </summary>
        public string Role { get; set; }
    }
}
