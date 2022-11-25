using Microsoft.AspNetCore.Mvc;

namespace WEPAPI_Microservice.Settings
{
    public class TokenStoreDatabaseSettings : ITokenStoreDatabaseSettings
    {

        public string TokenCollectionName { get; set; }

        public string ConnectionString { get; set; }  


        public string DatabaseName { get; set; }

        public string TokensCollectionName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public interface ITokenStoreDatabaseSettings
    {

        public string TokensCollectionName { get; set; }   

        public string ConnectionString { get; set; }  


        public string DatabaseName { get; set; }

    }
}


