using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using web.Controllers;
using System;
using web;
using services.services.equipe;
using core.seedwork;
using services.services.equipe.commands;

namespace controllers
{
    public static class PermissoesEquipe
    {
        public const string ID_NOME_URL = "pessoaid";
        public const string ID_URL = "{" + ID_NOME_URL + "}";
        public const string MODULO_EQUIPE = "EQUIPE";

        public const string READ = "READ";
        public const string VIEW = "VIEW";
        public const string CREATE = "CREATE";
        public const string UPDATE = "UPDATE";
        public const string DELETE = "DELETE";
    }

    [Produces("application/json")]
    [Route("api/[controller]"), Permission(PermissoesEquipe.MODULO_EQUIPE)]
    public class EquipeController : BaseController
    {
        private readonly IMediator mediator;
        private readonly QueryEquipe query;

        public EquipeController(IMediator mediator, QueryEquipe query)
        {
            this.mediator = mediator;
            this.query = query;
        }
        

        [HttpGet("BuscarPorId/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var result = query.GetById(id);
                return Ok(new Response(result));
            }
            catch (Exception e)
            {
                return BadRequest(new Response(e));
            }
        }

        [HttpPost("BuscarTodas"), Permission(Permissoes.READ)]
        public async Task<IActionResult> GetAsync([FromBody] ReadEquipeCommand command)
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
        public async Task<IActionResult> PostAsync([FromBody] CreateEquipeCommand command)
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

        [HttpPut, Permission(Permissoes.UPDATE)]
        public async Task<IActionResult> PutAsync([FromBody] UpdateEquipeCommand command)
        {
            if (command == null)
                return BadRequest("Command não pode ser nulo");

            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}"), Permission(Permissoes.DELETE)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var command = new DeleteEquipeCommand(id);
            if (command == null)
                return BadRequest("Command não pode ser nulo");

            var response = await mediator.Send(command);
            return Ok(response);
        }
    }
}
