using Microsoft.AspNetCore.Mvc;

namespace WEPAPI_Microservice.Settings
{

    public class UserStoreDatabaseSettings : IUserStoreDatabaseSettings
    {

        public string UsersCollectionName { get; set; }

        public string ConnectionString { get; set; }  

        public string DatabaseName { get; set; }  
    
    
    }

    public interface IUserStoreDatabaseSettings
    {

        public string UsersCollectionName { get; set; }   

        public string ConnectionString { get; set; }  

        public string DatabaseName { get; set; }

    }
}


