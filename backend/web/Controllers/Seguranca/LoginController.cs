using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using seguranca;
using services.commands.command.seguranca;
using web.Extensions;

namespace Seguranca
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public LoginController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            IConfiguration configuration,
            ILogger<LoginController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.logger = logger;
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<object> CheckAsync()
        //{
        //    if (!HttpContext.User.Identity.IsAuthenticated)
        //        return BadRequest("Permissão negada");

        //    var user = await userManager.GetJwtUserAsync(HttpContext.User);
        //    var returnValue = new
        //    {
        //        IsAuthenticated = user != null,
        //        user = new
        //        {
        //            user.Id,
        //            user.Nome,
        //        }
        //    };
        //    return returnValue;
        //}

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> TokenAsync([FromBody] LoginCommand model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user == null)
                user = await userManager.FindByEmailAsync(model.UserName);

            if (user != null && !user.Desativado)
            {
                var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    var value = GenerateToken(user);
                    return base.Ok(value);
                }

            }

            return BadRequest("Não foi possível realizar o login");
        }

        private object GenerateToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //Custom
                new Claim(ReagroClaimNames.Name, user.Nome),
                new Claim(ReagroClaimNames.UserId, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Tokens:Issuer"],
                configuration["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: creds);

            return new { token = new JwtSecurityTokenHandler().WriteToken(token) };
        }

        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            await Task.Delay(0);
            return Ok();
        }
    }

    public struct ReagroClaimNames
    {
        public const string Name = "name";
        public const string UserId = "userId";
    }
}
