using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IndiaEvents2.Models;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace IndiaEvents2
{
    public class userSecurity : AuthorizationFilterAttribute
    {
        public static bool Login(string userName, string password)
        {
            using (IndiaEvents2Entities entities = new IndiaEvents2Entities())
            {
                return entities.users.Any(x => x.userName.Equals(userName, StringComparison.OrdinalIgnoreCase) && x.password == password);
            }
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                HttpContext.Current.Response.StatusCode = 401; 
            }
            else
            {
                string authendicationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodeAuthendicationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authendicationToken));
                string[] userDetails = decodeAuthendicationToken.Split(':');
                string userName = userDetails[0];
                string password = userDetails[1];

                if (Login(userName, password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
                }
                else
                {
                    HttpContext.Current.Response.StatusCode = 401;  
                }
            }
        }
    }
}