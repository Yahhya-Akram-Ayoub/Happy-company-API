using Kaizen_Task.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelsRepository;
using ModelsRepository.Models;
using Serilog;

namespace Kaizen_Task.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserContraller : CustomBaseController
    {
        public UserContraller( IConfiguration configuration, IManagerUnit managerUnit) : base(configuration, managerUnit)
        { }

        [HttpPost]
        [Route("DeleteUser")]
               //[Authorize(Roles = "Admin")]
        public IActionResult Delete([FromForm] string userId)
        {
            try
            {
                User user = _manager.Users.GetEntity((_user) => _user.Id.Equals(new Guid(userId)));
                if (!user.Role.Equals("Admin") && userId != null)
                {
                    _manager.Users.DeleteEntity((x) => x.Id.Equals(new Guid(userId)));
                    _manager.Complete();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUsers")]
         //[Authorize]
        public IActionResult GetUsers([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                int count = 0;
                List<User> users = _manager.Users.GetPaged(page, pageSize, out count).ToList();
                return Ok(new { users, count });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetRoles")]
               //[Authorize(Roles = "Admin")]
        public IActionResult GetRoles()
        {
            try
            {
                int count = 0;
                List<Role> roles = _manager.Roles.GetPaged(0, int.MaxValue, out count).ToList();
                return Ok(new { roles, count });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUser")]
               //[Authorize(Roles = "Admin")]
        public IActionResult GetUser([FromQuery] string userId)
        {
            try
            {
                User user = _manager.Users.GetEntity((user) => user.Id.Equals(new Guid(userId)));
                return Ok(new { user });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
