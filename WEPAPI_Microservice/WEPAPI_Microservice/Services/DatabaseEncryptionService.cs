using Microsoft.AspNetCore.Mvc;
using System.Text;
using WEPAPI_Microservice.Services;
using WEPAPI_Microservice.Settings;

namespace WEPAPI_Microservice.Services
{
    public class DatabaseEncryptionService

    {
        private readonly byte[] _aesKey;

        private readonly byte[] _aesIv;
        private object EncodingHelper;
        private object AesHelper;

        public DatabaseEncryptionService(IEncryptionSettings encryptionSettings)
        {
            _aesKey = EncodingHelper.FromSafeUrlBase64(encryptionSettings.DataEncryptionKey);
            _aesIv = EncodingHelper.FromSafeUrlBase64(encryptionSettings.DataEncryptionIv);   
        }  

        public string Encrypt(string data)
        {
            var encrypted = AesHelper.EncryptStringToBytes_Aes(data, _aesKey, _aesIv);
            return EncodingHelper.ToSafeUrlBase64(encrypted);   
        }

        public string Decrypt(string data)
        {
            var encrypted = EncodingHelper.FromSafeUrlBase64(data);
            return AesHelper.DecryptStringFromBytes_Aes(encrypted, _aesKey, _aesIv);
        }
    }
}



