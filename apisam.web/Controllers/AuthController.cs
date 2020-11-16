namespace apisam.web.Controllers
{
    using apisam.entities.ViewModels;
    using apisam.interfaces;
    using apisam.web.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    [EnableCors("Todos")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signinManager;

        private readonly IConfiguration _config;

        private readonly IUsuario _usuariosRepo;

        public AuthController(UserManager<User> userManager, SignInManager<User> signinManager, IConfiguration config, IUsuario usuariosRepo)
        {
            _userManager = userManager;

            _signinManager = signinManager;
            _config = config;

            _usuariosRepo = usuariosRepo;
        }

        [HttpPost("login", Name = "Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            TimeZoneInfo hondurasTime = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
            var _user = await _userManager.FindByNameAsync(model.UserName);
            if (_user == null) return BadRequest("usuario no existe");
            var result = _signinManager.CheckPasswordSignInAsync(_user, model.Password, true).Result;
            int failedAccess = _signinManager.UserManager.GetAccessFailedCountAsync(_user).Result;
            var horaBloqueo = _userManager.GetLockoutEndDateAsync(_user).Result;
            DateTime horaFin;
            if (horaBloqueo != null)
            {
                horaFin = TimeZoneInfo.ConvertTime(horaBloqueo.Value, hondurasTime).DateTime;
            }
            else
            {
                horaFin = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, hondurasTime);
            }

            if (result.Succeeded && _user.EmailConfirmed && _user.Activo)
            {
                return Ok(new LoginResponseViewModel()
                {
                    Resultado = result,
                    Token = JwtTokenGeneratorMachine(_user),
                    Intentos = failedAccess,
                    HoraDesbloqueo = horaFin,
                });

            }
            return Ok(new LoginResponseViewModel()
            {
                Resultado = result,
                Token = "",
                Intentos = failedAccess,
                HoraDesbloqueo = horaFin,
            });
        }

        [HttpPost("confirmemail", Name = "ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailViewModel model)
        {
            var _user = await _userManager.FindByIdAsync(model.UserId);
            //var confirm = await _userManager.ConfirmEmailAsync(_user, Uri.UnescapeDataString(model.Token));
            var confirm = await _userManager.ConfirmEmailAsync(_user, model.Token);
            if (confirm.Succeeded)
            {
                return Ok(new
                {
                    respuesta = "Se ha confirmado el email",
                });
            }
            return Unauthorized();
        }

        [HttpPost("resetpassword", Name = "ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return BadRequest(new { message = "Usuario no existe" });

            if (user != null && user.EmailConfirmed && user.Activo)
            {
                //Send Email
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                return Ok(new
                {
                    tokenChangePassword = token,
                    userId = user.Id
                });
            }
            return Unauthorized();
        }

        [HttpPost("changepassword", Name = "ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var _user = await _userManager.FindByIdAsync(model.UserId);
            var resetPasswordResult = await _userManager.ResetPasswordAsync(_user, Uri.UnescapeDataString(model.Token), model.Password);
            if (resetPasswordResult.Succeeded)
            {
                return Ok(new
                {
                    respuesta = "Se ha cambiado la contraseña",
                });
            }
            return Unauthorized();
        }

        [Authorize(Roles = "1,2")]
        [HttpPost("unlockuser/id/{id}")]
        public async Task<IActionResult> UnlockUser([FromRoute] string id)
        {
            var _user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.SetLockoutEndDateAsync(_user, new DateTimeOffset(DateTime.UtcNow));

            await _signinManager.UserManager.ResetAccessFailedCountAsync(_user);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    ok = true,
                    respuesta = "Se ha desbloqueado el usuario",
                });
            }
            return Unauthorized();
        }

        [Authorize(Roles = "1,2")]
        [HttpGet("roles", Name = "GetRoles")]
        public IActionResult GetRoles()
        {
            return Ok(_usuariosRepo.Roles);
        }

        [HttpGet("tokenconfirmemail/id/{id}", Name = "GetTokenConfirmEmail")]
        public async Task<IActionResult> GetTokenConfirmEmail([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return Ok(new
            {
                token
            });
        }

        private string JwtTokenGeneratorMachine(User userInfo)
        {

            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,userInfo.Id),
            new Claim(ClaimTypes.Name,userInfo.UserName),
            new Claim(ClaimTypes.Role, userInfo.RolId.ToString()),


            };



            var securityKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var _credentials
              = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = _credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
