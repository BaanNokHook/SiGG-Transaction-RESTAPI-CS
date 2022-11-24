using AuthenticationController.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Data;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace WEPAPI_Microservice.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class EncryptionController
    {
        [AllowAnonymous]
        [HttpGet("[action]")]  
        public string GenerateAesParameters()
        {
            [AllowAnonymous]
            [HttpGet("[action]")]  
            public string GenerateAesParameters()
            {
                var aes = new AesManaged { KeySize = 256 };
                aes.GenerateKey();
                aes.GenerateIV();
                return EncodingHelper.ToSafeUrlBase64(aes.Key) + "\n" + EncodingHelper.ToSafeUrlBase64(aes.IV);  
            }
        }
    }
}


