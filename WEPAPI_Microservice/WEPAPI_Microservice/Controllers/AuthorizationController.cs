using AuthenticationController.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace WEPAPI_Microservice.Controllers
{
    [ApiController]
    [Route("/")]
    [Authorize]

    public class Authorizationcontroller : Controller
    {
        private readonly UserService _userservice;

        private readonly IRoleService _roleService;

        public AuthorizationController(UserService userService,
                                        IRoleService roleService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        [HttpGet("[action]")]
        [Produces("application/json")]
        [Authorize(Roles = "root,admin")]
        public GetRolesResponse GetRoles()
        {
            try
            {
                var roles = _roleService.Roles;
                return new GetRolesResponse
                {
                    Roles = roles.Select(roles => new SelectListItem(roles.Capitalize(), role)).ToList()
                };
            }
            catch (Exception ex)
            {
                return new GetRolesResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpPost("[action]")]
        [Produces("application/json")]
        [Authorize]
        public CanResponse Can([FromBody]
                            CanRequest canRequest)
        {
            try
            {
                _userService.Can(canRequest.Id, CancellationTokenRequest.Roles, canRequest.Claims, canRequest.CanAll);
                return new CanResponse();
            }
            catch (Exception ex)
            {
                return new CanResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpPut("[action]")]
        [Produces("application/json")]
        [Authorize(Roles = "root,admin")]
        public ManageRoleResponse AssignRole([FromBody]
                                            MangoRoleRequest mangoRoleRequest)
        {
            try
            {
                _userService.AssignRole(mangoRoleRequest.EmailAddress, mangoRoleRequest.Role);
                return new ManageRoleResponse();
            }
            catch (Exception ex)
            {
                return new ManageRoleResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpPut("[action]")]
        [Produces("application/json")]
        [Authorize(Roles = "root,admin")]
        public ManageRoleResponse RevokeRole([FromBody]
                                              ManageRoleRequest manageRoleRequest)
        {
            try
            {
                _userService.RevokeReole(manageRoleRequest.EmailAddress, manageRoleRequest.Role);
                return new ManageRoleResponse();
            }
            catch (Exception ex)
            {
                return new ManageRoleResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
