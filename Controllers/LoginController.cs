using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Repositories;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly ITokenService _tokenService;

        public LoginController(IRepository<User> userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            User? user = _userRepository.GetByConditionAsync(x => x.Email == model.Email).Result;
            if (user == null || !PasswordHasher.VerifyPassword(model.Password, user.PasswordHash))
                return Unauthorized("Geçersiz email ya da şifre.");

            var token = _tokenService.createToken(user);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            if(_userRepository.GetByConditionAsync(x => x.Email == register.Email).Result != null)
                return BadRequest("Bu email zaten kayıtlı.");

            var hashedPassword = PasswordHasher.HashPassword(register.Password);

            if(register.Role.Trim().ToLower() != "admin" && register.Role.Trim().ToLower() != "user")
                return BadRequest("Geçersiz rol. Sadece 'Admin' veya 'User' olabilir. Değer girmeyebilirsiniz.");

            User newUser = new()
            {
                FullName = register.FullName,
                Email = register.Email,
                PasswordHash = hashedPassword,
                Role = register.Role
            };

            await _userRepository.Ekle(newUser);
            return Ok(new { message = "Kayıt başarılı. Giriş yapabilirsiniz." });
        }
    }
}
