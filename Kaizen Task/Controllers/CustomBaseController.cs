using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsRepository;
using ModelsRepository.Models;
using System.Security.Claims;

namespace Kaizen_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {

        protected readonly IConfiguration _configuration;
        protected readonly IManagerUnit _manager;
        protected readonly User _currentUser;
        public CustomBaseController(IConfiguration configuration, IManagerUnit manager)
        {
            _configuration = configuration;
            _manager = manager;
            _currentUser = GetCurrentUser();
        }


        protected User CurrentUser
        {
            get
            {
                return _currentUser;
            }
        }
        private User GetCurrentUser()
        {
            if (HttpContext?.User != null)
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                if (identity != null)
                {
                    var userClaims = identity.Claims;

                    return new User
                    {
                        FullName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                        Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                        Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                    };
                }

            }
            return null;
        }
    }
}
