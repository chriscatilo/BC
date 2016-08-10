namespace BC.EQCS.Domain.Schema
{
    public enum ValueConstraint
    {
        /// <summary>
        /// Value is set to NULL and cannot be altered.
        /// </summary>
        NotApplicable, 

        /// <summary>
        /// Value cannot be altered and viewed
        /// </summary>
        Restricted,  

        /// <summary>
        /// Value can only be read but not altered.
        /// </summary>
        ViewOnly,

        /// <summary>
        /// Value is automatically determined by the server and cannot be set or altered.
        /// </summary>
        ServerResolved,

        /// <summary>
        /// Value can be read, set and altered to any valid value including NULL.
        /// </summary>
        Optional,

        /// <summary>
        /// Value can be read, set and altered to any valid value excluding NULL.
        /// </summary>
        Mandatory
    }
}