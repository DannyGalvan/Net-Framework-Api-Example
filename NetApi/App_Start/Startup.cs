using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Owin;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using System.Text;

[assembly: OwinStartup(typeof(NetApi.App_Start.Startup))]
namespace NetApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public static void ConfigureAuth(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = GetAudience(),
                    ValidIssuer = GetIssuer(),
                    IssuerSigningKey = GetSymmetricSecurityKey(),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                },
            });
        }

        public static string GetIssuer()
        {
            return ConfigurationManager.AppSettings["JwtIssuer"];
        }

        public static string GetAudience()
        {
            return ConfigurationManager.AppSettings["JwtAudience"];
        }

        public static string GetSecurityKey()
        {
            return ConfigurationManager.AppSettings["JwtSecret"];
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            var issuerSigningKey = GetSecurityKey();
            byte[] data = Encoding.UTF8.GetBytes(issuerSigningKey);

            var result = new SymmetricSecurityKey(data);

            return result;
        }
    }
}