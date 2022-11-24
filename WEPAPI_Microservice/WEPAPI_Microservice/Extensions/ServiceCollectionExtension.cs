using AuthenticationController.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WEPAPI_Microservice;
using WEPAPI_Microservice;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Identity;

namespace WEPAPI_Microservice.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddEncryptionService(this IServiceCollection services,
                                                IConfiguration configuration)

        {
            services.Configure<EncryptionSettings>
                (configuration.GetSection(nameof(EncryptionSettings)));

            services.AddSingleton<IEncryptionSettings>
                (sp => sp.GetRequiredService<IOptions<Encryption>>().Value);

            services.AddSingleton<DatabaseEncryptionService>();  

        }

        public static void AddUserervice(this IServiceCollection services,  
                                         IConfiguration configuration)

        {
            services.Configure<IUserStoreDatabaseSettings>
                (configuration.GetSection(nameof(UserStoreDatabaseSettings)));

            services.AddSingleton<IUserStoreDatabaseSettings>
                (sp => sp.GetRequiredService<IOptions<UserStoreDatabaseSettings>>().Value);

            services.AddSingleton<UserService>(); 
        }

        public static void AddTokenService(this IServiceCollection services,  
                                            IConfiguration configuration)
        {
            services.Configure<TokenStoreDatabaseSettings>
                (configuration.GetSection(nameofof(TokenStoreDatabaseSettings)));

            services.AddSingleton<ITokenStoreDatabaseSettings>
                (sp => sp.GetServiceRequiredService<IOptions<TokenStoreDatabaseSettings>>().Value);

            services.AddSingleton<TokenService>();  
        }
    }
}



