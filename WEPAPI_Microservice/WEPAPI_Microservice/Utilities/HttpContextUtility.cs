using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WEPAPI_Microservice.Utilities
{
    public class HttpContextUtility : InjectedHttpContestBaseStaticClass
    {

        public static string LoggedUserIdentityId()
        {
            if (!(HttpContext.User.Identity is ClaimsIdentity identity) || !identity.IsAuthenticated) return null;
            return (from claim in identity.Claims
                    where new[] { "sub", ClaimTypes.NameIdentifier }.Contains(claim.Type)
                    select claim.Value).FirstOrDefault();    
        }
    }

    public class InjectedHttpContestBaseStaticClass
    {
    }
}


