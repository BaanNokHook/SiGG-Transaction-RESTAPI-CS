using AuthenticationController.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Data;

namespace WEPAPI_Microservice.Controllers
{
    [ApiController]
    [Route("/")]  

    public class ConfigurationController
    {
        private readonly CommonSettings _commonSettings;    

        public ConfigurationController(IOptions<CommonSettings> commonSettings)
        {
            _commonSettings = commonSettings.Value;   
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [Produces("application/json")]   
        public void ConfigureCommonSettings([FromBody]
                                            CommonSettings commonSettings)
        {
            _commonSettings.BaseApplicationPath = commonSettings.BaseApplicationPath;
            _commonSettings.IdentitySettings = commonSettings.IdentitySettings;
            _commonSettings.PasswordSettings = commonSettings.PasswordSettings;   
        }
    }
}


