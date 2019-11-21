using core.seedwork;
using core.utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using services.services.pessoa;
using services.services.pessoa.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Controllers
{

    public static class Permissoes
    {
        public const string ID_NOME_URL = "pessoaid";
        public const string ID_URL = "{" + ID_NOME_URL + "}";
        public const string MODULO_PESSOA = "PESSOA";

        public const string READ = "READ";
        public const string VIEW = "VIEW";
        public const string CREATE = "CREATE";
        public const string UPDATE = "UPDATE";
        public const string DELETE = "DELETE";
    }


    [Produces("application/json")]
    [Route("api/[controller]"), Permission(Permissoes.MODULO_PESSOA)]
    public class PessoaController : BaseController
    {
        private readonly IMediator mediator;
        private readonly QueryPessoa query;

        public PessoaController(IMediator mediator, QueryPessoa query)
        {
            this.mediator = mediator;
            this.query = query;
        }

        [HttpPost("BuscarTodas"), Permission(Permissoes.READ)]
        public async Task<IActionResult> GetAsync([FromBody] ReadPessoaCommand command)
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
        public async Task<IActionResult> PostAsync([FromBody] CreatePessoaCommand command)
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

        [HttpGet("Membros/{equipeid}")]
        public async Task<IActionResult> GetPessoasPorEquipe(Guid equipeid)
        {
            try
            {
                var result = await query.GetPessoasPorEquipe(equipeid);
                return Ok(new Response(result));
            }
            catch (Exception e)
            {
                return BadRequest(new Response(e));
            }
        }

        [HttpPut, Permission(Permissoes.UPDATE)]
        public async Task<IActionResult> PutAsync([FromBody] UpdatePessoaCommand command)
        {
            if (command == null)
                return BadRequest("Command não pode ser nulo");

            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}"), Permission(Permissoes.DELETE)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeletePessoaCommand(id);
            if (command == null)
                return BadRequest("Command não pode ser nulo");

            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("id não pode ser nulo");

            var response = await query.GetPorId(id);
            return Ok(response);
        }

        [HttpGet("AtivarPessoa/{id}")]
        public async Task<IActionResult> AtivarPessoa(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("id não pode ser nulo");

            var response = await query.AtivarPessoa(id);
            return Ok(response);
        }
    }
}
