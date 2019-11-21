using core.seedwork;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using services.services.pessoa_tipo;
using services.services.pessoa_tipo.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Controllers
{
    public static class PermissoesPessoaTipo
    {
        public const string ID_NOME_URL = "pessoaid";
        public const string ID_URL = "{" + ID_NOME_URL + "}";
        public const string MODULO_PESSOA_TIPO = "PESSOA";

        public const string READ = "READ";
        public const string VIEW = "VIEW";
        public const string CREATE = "CREATE";
        public const string UPDATE = "UPDATE";
        public const string DELETE = "DELETE";
    }

    [Produces("application/json")]
    [Route("api/[controller]"), Permission(PermissoesPessoaTipo.MODULO_PESSOA_TIPO)]
    public class PessoaTipoController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly QueryPessoaTipo query;
        public PessoaTipoController(IMediator mediator, QueryPessoaTipo query)
        {
            this.mediator = mediator;
            this.query = query;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("id não pode ser nulo");

            var response = query.GetPorId(id);
            return Ok(response);
        }

        [HttpPost("BuscarTodas"), Permission(Permissoes.READ)]
        public async Task<IActionResult> GetAsync([FromBody] ReadPessoaTipoCommand command)
        {
            if (command == null)
                return BadRequest("Command não pode ser nulo");
            try
            {
                var response = await mediator.Send(command);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new Response(e));
            }
        }

        [HttpPost, Permission(Permissoes.CREATE)]
        public async Task<IActionResult> PostAsync([FromBody] CreatePessoaTipoCommand command)
        {
            if (command == null)
                return BadRequest("Command não pode ser nulo");

            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpPut, Permission(Permissoes.UPDATE)]
        public async Task<IActionResult> PutAsync([FromBody] UpdatePessoaTipoCommand command)
        {
            if (command == null)
                return BadRequest("Command não pode ser nulo");

            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}"), Permission(Permissoes.DELETE)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeletePessoaTipoCommand(id);
            if (command == null)
                return BadRequest("Command não pode ser nulo");

            var response = await mediator.Send(command);
            return Ok(response);
        }
    }
}
