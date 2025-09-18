//using CMSApi.Application.DTO.AuthDto;
//using CMSApi.Application.Interfaces;
//using Microsoft.AspNetCore.Mvc;


//namespace CMSApi.Presentation.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Tags("Auth")]
//    public class AuthController : ControllerBase
//    {
//        private readonly IAuthService _authService;

//        public AuthController(IAuthService authService)
//        {
//            _authService = authService;
//        }

//        [HttpPost("signup")]
//        public async Task<ActionResult> SignUp([FromBody] RegisterModel model)
//        {
//            var employee = await _authService.SignUp(model);

//            if (employee == null)
//                return BadRequest(new { Message = "Email already exists" });

//            return Ok(employee);
//        }


//            [HttpPost("signin")]
//        public async Task<IActionResult> Login([FromBody] LoginModel loginmodel)
//        {
//            var result = await _authService.SignIn(loginmodel);
//                if (result == null)
//                    return BadRequest("Email already registered");

//                return Ok(result);
//        }
//    }
//}
