using Microsoft.AspNetCore.Mvc;
using PlanoContas.Aplicacao.Base.Security;
using PlanoContas.Aplicacao.Conta.Commands.Conta.Create;
using PlanoContas.Aplicacao.Conta.Commands.Delete;
using PlanoContas.Aplicacao.Conta.Commands.Edit;
using PlanoContas.Aplicacao.Conta.Commands.NextCode;
using PlanoContas.Aplicacao.Conta.Queries.Detail;
using PlanoContas.Aplicacao.Conta.Queries.List;

namespace PlanoContas.API.Controllers
{
    [Authorize]
    public class ContaController : ApiControllerBase
    {
        [HttpPost("codigo/proximo")]
        public async Task<ActionResult<NextCodeViewModel>> GetNextCode(string? codigoContaPai)
        {
            return await Mediator.Send(new ContaProximoCodigoCommand { CodigoPai = codigoContaPai });
        }

        [HttpGet]
        public async Task<ActionResult<ContaListViewModel>> Get()
        {
            return await Mediator.Send(new GetContasListQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContaDetailViewModel>> Get(int id)
        {
            return await Mediator.Send(new GetContaDetailQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(ContaCreateCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ContaEditCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new ContaDeleteCommand(id));

            return NoContent();
        }
    }
}
