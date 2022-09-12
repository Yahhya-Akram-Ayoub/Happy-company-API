using Kaizen_Task.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsRepository;
using ModelsRepository.Models;

namespace Kaizen_Task.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IJWTUnit _jwtService;
        public AuthController(IJWTUnit jwtService, IConfiguration configuration, IManagerUnit managerUnit) : base(configuration, managerUnit)
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLogin user)
        {
            try
            {
                User LoginUser = _manager.Users.GetEntity((_user) => _user.Email.Equals(user.username) && _user.Password.Equals(user.password));

                if (LoginUser != null)
                {
                    if (LoginUser.Active)
                    {
                        string token = _jwtService.GenerateToken(LoginUser);
                        return Ok(new
                        {
                            token,
                            role = LoginUser.Role,
                            username = LoginUser.Email
                        });
                    }
                    else
                    {
                        return Ok(new { message = "your acount is disabled,contact support" });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "Admin")]
        public IActionResult Register([FromBody] User user)
        {
            try
            {

                if (user.Id != Guid.Empty) // update user if already exist
                {
                    _manager.Users.Update(user);
                }
                else
                {
                    User RegisterUser = _manager.Users.GetEntity((_user) => _user.Email.Equals(user.Email));

                    if (RegisterUser == null)
                    {
                        _manager.Users.Add(user);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }

                _manager.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangePassword([FromForm] string userId, [FromForm] string newPass, [FromForm] string confPass)
        {
            try
            {
                User user = _manager.Users.GetEntity((_user) => _user.Id.Equals(new Guid(userId)));
                if (user != null && newPass.Equals(confPass))
                {
                    user.Password = newPass;
                    _manager.Complete();
                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
