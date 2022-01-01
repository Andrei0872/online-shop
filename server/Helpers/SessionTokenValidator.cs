using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using server.DAL.UnitOfWork;

namespace server.Helpers
{
    public class SessionTokenValidator
    {
        public static async Task ValidateSessionToken(TokenValidatedContext context)
        {
        var unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();

        if (context.Principal.HasClaim(c => c.Type.Equals(JwtRegisteredClaimNames.Jti)))
        {
            var jti = context.Principal.Claims.FirstOrDefault(c => c.Type.Equals(JwtRegisteredClaimNames.Jti)).Value;

            var tokenInDb = await unitOfWork.SessionToken.GetByJTI(jti);
            if (tokenInDb != null && tokenInDb.ExpirationDate > DateTime.Now)
            {
            return;
            }
        }

        context.Fail("");
        }
    }
}