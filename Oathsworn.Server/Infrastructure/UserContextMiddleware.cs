using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.VisualBasic;

namespace Oathsworn.Infrastructure
{
    public class UserContextMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var claims = context.User.Claims;

            if (!claims.Any())
            {
                await _next(context);
                return;
            }

            int encounterId = 0;
            context.Request.Cookies.TryGetValue(GlobalConstants.ENCOUNTER_ID, out var encounterIdString);
            var hasEncounterId = encounterIdString is not null && int.TryParse(encounterIdString, out encounterId);
            var email = claims.Where(x => x.Type.Equals(JwtRegisteredClaimNames.Email)).Single().Value;
            var givenName = claims.Where(x => x.Type.Equals(JwtRegisteredClaimNames.GivenName)).Single().Value;
            var familyName = claims.Where(x => x.Type.Equals(JwtRegisteredClaimNames.FamilyName)).Single().Value;

            await context.RequestServices.GetRequiredService<IUserContextFactory>().SetContext(email, givenName, familyName, hasEncounterId ? encounterId : null);
            await _next(context);
        }
    }

    public static class UserContextMiddlewareMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserContext(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserContextMiddleware>();
        }
    }
}