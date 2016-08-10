namespace BC.Security.Internal.Contracts.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual long Permissions { get; set; }
    }
}