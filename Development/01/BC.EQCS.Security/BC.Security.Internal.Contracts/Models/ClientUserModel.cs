using System;
using System.Security.Claims;

namespace BC.Security.Internal.Contracts.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientUserModel : UserModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public override ClaimsIdentity GetClaimsIdentity()
        {
            throw new NotSupportedException("Claims Identity not available in client user model");
        }
    }
}