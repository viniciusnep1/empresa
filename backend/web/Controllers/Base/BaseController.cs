using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace web.Controllers
{
    public class BaseController : Controller
    {
        protected Guid GetIdUsuarioLogado()
        {
            var idAsString = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

            // TODO: PROVISÓRIO: Retorna usuário admin quando não está logado
            if (string.IsNullOrWhiteSpace(idAsString))
            {
                idAsString = "762238c2-7e4e-4033-a59e-ef0eb042b4fa";
            }
            /////////////////////////////////////////////////////////////////

            var idUsuarioLogado = Guid.Parse(idAsString);

            return idUsuarioLogado;
        }
    }
}
