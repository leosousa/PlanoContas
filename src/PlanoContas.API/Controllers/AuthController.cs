using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlanoContas.Aplicacao.Base.Results;
using PlanoContas.Aplicacao.Conta.Queries.List;
using PlanoContas.Infraestrutura.Identity.Commands.Login;
using PlanoContas.Infraestrutura.Identity.Commands.RefreshToken;
using PlanoContas.Infraestrutura.Identity.Commands.Register;
using PlanoContas.Infraestrutura.Identity.Commands.Register.Admin;
using PlanoContas.Infraestrutura.Identity.Commands.Register.User;

namespace PlanoContas.API.Controllers
{
    public class AuthController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<TokenViewModel>> Login(UserLoginCommand login)
        {
            var response = await Mediator.Send(login);

            if (!response.Result!.Succeeded)
            {
                return BadRequest(response.Result.Errors.ToArray());
            }

            return Ok(response.Token);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterViewModel>> Register(UserRegisterCommand user)
        {
            var response = await Mediator.Send(user);

            if (!response.Result!.Succeeded)
            {
                return BadRequest(response.Result.Errors.ToArray());
            }

            return Ok(response);
        }

        [HttpPost("register-admin")]
        public async Task<ActionResult<UserRegisterViewModel>> RegisterAdmin(AdminRegisterCommand user)
        {
            var response = await Mediator.Send(user);

            if (!response.Result!.Succeeded)
            {
                return BadRequest(response.Result.Errors.ToArray());
            }

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenViewModel>> RefreshToken(RefreshTokenCommand token)
        {
            if (token is null)
            {
                return BadRequest("Token inválido");
            }

            var response = await Mediator.Send(token);

            if (!response.Result!.Succeeded)
            {
                return BadRequest(response.Result.Errors.ToArray());
            }

            return Ok(response);
        }
    }
}
