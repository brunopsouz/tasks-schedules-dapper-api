using Microsoft.AspNetCore.Mvc;
using TaskNoteManager.Application.Services.User.Register;
using TaskNoteManager.Communication.Requests;
using TaskNoteManager.Communication.Responses;

namespace TaskNoteManager.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Registra um novo usuário no sistema.
        /// 
        /// Recebe os dados de cadastro, valida as informações, cria o usuário na base de dados
        /// e retorna os detalhes do usuário recém-criado.
        /// </summary>
        /// <param name="service">Serviço responsável pela lógica de cadastro de usuários.</param>
        /// <param name="request">Objeto contendo os dados necessários para o registro do usuário.</param>
        /// <returns>
        /// Retorna <see cref="ResponseRegisterUser"/> com as informações do usuário criado
        /// e o status HTTP 201 (Created).
        /// </returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ResponseRegisterUser), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUserService service,
            [FromBody] RequestRegisterUser request)
        {
            var result = await service.Register(request);

            return Created(string.Empty, result);
        }

    }
}
