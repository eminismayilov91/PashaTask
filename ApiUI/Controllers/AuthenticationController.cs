using ApiUI.Model;
using Bussiness.Consrete;
using Microsoft.AspNetCore.Mvc;

namespace ApiUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JWTAuthenticationManager _jwtAuthenticationManager;
        public AuthenticationController(JWTAuthenticationManager jwtAuthenticationManager)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost("Authorize")] 
        public IActionResult Authorize(AuthorizationModel authorization)
        {
            var result = _jwtAuthenticationManager.Authenticate(authorization.UserName, authorization.Password);
            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
