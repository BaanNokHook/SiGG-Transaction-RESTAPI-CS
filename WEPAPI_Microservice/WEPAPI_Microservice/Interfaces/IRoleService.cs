using Microsoft.AspNetCore.Mvc;

namespace WEPAPI_Microservice.Interfaces
{
    public interface IRoleService
    {

        string this[string index] { get; set; }

        List<string> Roles { get; set; }
    }
}