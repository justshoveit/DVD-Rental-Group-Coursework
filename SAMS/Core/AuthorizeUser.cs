using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Core
{
    public class AuthorizeUser : AuthorizeAttribute
    {
        public string Roles { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized) return false;

            var ctx = httpContext.GetOwinContext();
            IEnumerable<Claim> claims = ctx.Authentication.User.Claims;
         
            Claim roleClaim = claims.First(x => x.Type == ClaimTypes.Role);

            return Roles.Split(',').Contains(roleClaim.Value);
        }
    }
}