using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using StandingOrders.API.Services.Encryption;
using System;
using System.Globalization;

namespace StandingOrders.API.Filters
{
    public class SecondFactorAuthorizationFilter : Attribute, IAuthorizationFilter
    {

        private readonly IConfiguration _config;
        private readonly IEncryptionService _encryption;

        public SecondFactorAuthorizationFilter(IConfiguration config, IEncryptionService encryption)
        {
            _config = config;
            _encryption = encryption;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            StringValues authorizationData;
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationData);

            var token = _encryption.Decrypt(authorizationData[0]);
            var tokenToDate = DateTime.ParseExact(token, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            var tokenExpirationTime = _config.GetValue<double>("AuthorizationSettings:tokenExpirationTime");


            if (tokenToDate.AddSeconds(tokenExpirationTime) < DateTime.Now) //if token is older than 30 seconds
            {
                context.Result = new ObjectResult($"Unauthorized access.")
                {
                    StatusCode = 401
                };
            }
  
        }
    }
}
