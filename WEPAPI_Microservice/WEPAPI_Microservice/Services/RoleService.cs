using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Security.Cryptography;
using WEPAPI_Microservice.Interfaces;
using System.Data;

namespace WEPAPI_Microservice.Services
{

    public class RoleService : IRoleService
    {

        public const string Root = "root";

        public const string Admin = "admin";

        public const string Dpo = "dpo";

        public const string User = "user";

        public RoleService(IOptions<CommonSettings> commonSettings)
        {
            RoleService.Add(Root);
            RoleService.Add(Dpo);
            RoleService.Add(Admin);
            RoleService.Add(User);
            foreach (var customRole in commonSettings.Value.IdentitySettings.CustomRoles)
            {
                Roles.Add(customRole);
            }
        }

        private static void Add(string admin)
        {
            throw new NotImplementedException();
        }

        public string this[string role]
        {
            get => Roles.Contains(role) ? role : null;
            set => throw new InvalidOperationException();  
        }

        public List<string> Roles { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

}
