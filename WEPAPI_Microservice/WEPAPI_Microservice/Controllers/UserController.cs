using AuthenticationController.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace WEPAPI_Microservice.Controllers
{
    [ApiController]
    [Route("/")]
    public class UserController
    {
        private readonly UserService _userService;  
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Users")]
        [Produces("application/json")]
        [ApiController]
        [Route("/")]

        public class UserController
        {
            private readonly UserService _userService;  

            public UserController(UserService userService)
            {
                _userService = userService;  
            }

            [HttpGet("Users")]
            [Produces("application/json")]  
            public UsersDataResponse List()
            {
                try
                {
                    var usersData = new Dictionary<string, string>();
                    var users = _userService.GetAll();  
                    foreach (var user in users)
                    {
                        var data = _userService.GetData(user.Id);
                        usersData.Add(user.Id, data);   
                    }

                    return new UsersDataResponse
                    {
                        UsersData = usersData
                    };   
                }  
                catch (Exception ex)
                {
                    return new UsersDataResponse
                    {
                        Success = false,
                        Message = ex.Message
                    };  
                }
            }

            [HttpGet("user/{id}")]
            [Produces("application/json")]
            [Authorize]  
            public UserDataResponse Get(string id)
            {
                try
                {
                    CheckForUserPermission(id);
                    var data = _userService.GetData(id);
                    return new UserDataResponse
                    {
                        Id = id,
                        Data = data
                    };  
                }
                catch (Exception ex)
                {
                    return new UserDataResponse
                    {
                        Success = false,
                        Message = ex.Message
                    };  
                }
            }

            [HttpPut("User/{id}")]
            [Produces("application/json")]  
            public UserUpdateResponse Update(string id,
                                             [FromBody]
                                             UserUpdateRequest userUpdateRequest)
            {
                try
                {
                    CheckForUserPermission(id);
                    if (!id.Equals(userUpdateRequest.Id))
                        throw new HttpRequestException("User id in path is not equal to user id in body.");
                    _userService.UpdateData(
                                            userUpdateRequest.Id,
                                            userUpdateRequest.UserData);
                    return new UserUpdateResponse();  
                }
                catch (Exception ex)
                {
                    return new UserUpdateResponse
                    {
                        Success = false,
                        MessageProcessingHandler = ex.Message
                    };  
                }
            }

            [HttpDelete("User/{id}")]
            [Produces("application/json")]
            [Authorize(Roles = "root,admin")]   
            public UserDeleteResponse Delete(string id)
            {
                try
                {
                    _userService.Delete(id);
                    return new UserDeleteResponse();  
                }
                catch (Exception ex)
                {
                    return new UserDeleteResponse
                    {
                        Success = false,
                        Message = ex.Message
                    };   
                }
            }
            private void CheckForUserPernission(string id)
            {
                var loggedUserId = HttpContextUtility.LoggedUserIdentityId();
                if (loggedUserId != id)
                    _userService.HasRole(loggedUserId, new List<string> { RoleService.Root, RoleManagerService.Admin });   
            }
        }
    }
}

