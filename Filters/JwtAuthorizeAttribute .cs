using EmployeeDetailsWithTab.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace EmployeeDetailsWithTab.Filters
{
    //This is added in app_start filterconfig.cs file
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // Skip validation if action/controller allows anonymous access
            bool skipAuthorization = filterContext.ActionDescriptor
                .IsDefined(typeof(AllowAnonymousAttribute), inherit: true) ||
                filterContext.ActionDescriptor.ControllerDescriptor
                .IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuthorization)
                return;

            var request = filterContext.HttpContext.Request;
            var token = request.Headers["Authorization"]?.Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                token = request.Cookies["accessToken"]?.Value;
            }
            if (string.IsNullOrEmpty(token))
            {
                filterContext.Result = new RedirectResult("/User/SignIn");
                return;
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("mysupersecret_secretkey!123dfsddfbdfbdvsdbdfbdfvsdfsddfb"); // same key used to sign token
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = "https://localhost:7244/",

                    ValidateAudience = true,
                    ValidAudience = "https://localhost:7244/",

                    ValidateLifetime = true, // also validate expiration
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var uniqueNameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
                if (!string.IsNullOrEmpty(uniqueNameClaim))
                {
                    var userObj = JsonConvert.DeserializeObject<InsertSignDataModel>(uniqueNameClaim);
                    filterContext.HttpContext.Items["User"] = userObj;
                }
            }
            catch
            {
               // filterContext.Controller.TempData["ErrorMessage"] = "Invalid username Or Password";
                filterContext.Result = new RedirectResult("/User/SignIn");
            }
        }
    }
}