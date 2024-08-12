namespace API.Controllers

{
    using Entities.Abstract;
    using Entities.Concrete;
    using Entities.Dtos;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Services.Abstract;
    using Shared.Entities.Token;
    using Shared.Utilities_araçlar_.Results;
    using System.Threading.Tasks;
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        public UserController(UserManager<User> userManager,IUserService userService, SignInManager<User> signIngManager, ITokenService tokenService)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signIngManager;
            _tokenService = tokenService;
           
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {

            var result = await _userService.Add(userAddDto);
            if (result.Data != null && !string.IsNullOrEmpty(result.Data.Email))
            {

                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);

                
                if (user == null)
                    return Unauthorized("Invalid Email or Password");
                
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded)
                    return Unauthorized("Invalid Email or Password");

                var roles = await _userManager.GetRolesAsync(user); // Await the task
                var rolesMessage = roles.Any() ? $"Roles: {string.Join(", ", roles)}" : "No roles assigned";
                return Ok(new NewUserDto
                {
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                }) ;
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            var result = await _userService.Update(userUpdateDto);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.Delete(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _userService.Get(id);
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("Get All")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            if (result.ResultStatus == ResultStatus.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
