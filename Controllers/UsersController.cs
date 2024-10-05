using ApiDevBP.ApplicationServices;
using ApiDevBP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ApiDevBP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        #region Declarations

        private readonly UserApplicationService userApplicationService;
        private readonly ILogger<UsersController> _logger;

        private readonly string messageErrorTime = $"---> Ocurrido {DateTime.UtcNow}";

        #endregion

        public UsersController(ILogger<UsersController> logger,
            UserApplicationService _userApplicationService)
        {
            userApplicationService = _userApplicationService;
            _logger = logger;
        }

        /// <summary>
        /// Agrega un Usuario a la Fuente de Datos
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveUser(UserModel user)
        {
            try
            {
                int result = await userApplicationService.AddAsync(user);

                return Ok(buildResponse($"Usuario {user.Id} se ingreso correctamente", HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                string error = $"{ex.Message} {messageErrorTime}";
                _logger.LogError(error);
                return BadRequest(buildResponse(error, HttpStatusCode.BadRequest));
            }
        }

        /// <summary>
        /// Obtiene una Lista de los Usuarios existentes en la Fuente de Datos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                IEnumerable<UserModel> listUsers = await userApplicationService.GetUsersAsync();
                return Ok(buildResponse(listUsers, HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                string error = $"{ex.Message} {messageErrorTime}";
                _logger.LogError(error);
                return NotFound(buildResponse(error, HttpStatusCode.BadRequest));
            }
        }

        /// <summary>
        /// Obtiene los datos de un Usuario que exista en la Fuente de Datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsers(int id)
        {
            try
            {
                UserModel users = await userApplicationService.GetUserAsync(id);

                return Ok(buildResponse(users, HttpStatusCode.OK));
            }
            catch (Exception ex)
            {
                string error = $"{ex.Message} {messageErrorTime}";
                _logger.LogError(error);
                return NotFound(buildResponse(error, HttpStatusCode.BadRequest));
            }
        }

        /// <summary>
        /// Actualiza un Usuario que encuentre en la Fuente de Datos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(int id, UserModel user)
        {
            try
            {
                bool userExist = await userApplicationService.UserExistAsync(id);
                if (!userExist)
                    return NotFound(buildResponse("El Usuario que busca no existe", HttpStatusCode.NotFound ));

                await userApplicationService.UpdateAsync(user);
                return Accepted(buildResponse(JsonSerializer.Serialize(user), HttpStatusCode.Accepted));
            }
            catch (Exception ex)
            {
                string error = $"{ex.Message} {messageErrorTime}";
                _logger.LogError(error);
                return BadRequest(buildResponse(error, HttpStatusCode.BadRequest));
            }
        }

        /// <summary>
        /// Elimina un Usuario que exista en la Fuente de Datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                UserModel findUser = await userApplicationService.GetUserAsync(id);
                if (findUser == null)
                    return NotFound(buildResponse("El Usuario que busca no existe", HttpStatusCode.NotFound));

                await userApplicationService.DeleteAsync(findUser);
                return Accepted(buildResponse($"Usuario {id} eliminado correctamente", HttpStatusCode.Accepted));
            }
            catch (Exception ex)
            {
                string error = $"{ex.Message} {messageErrorTime}";
                _logger.LogError(error);
                return BadRequest(buildResponse(error, HttpStatusCode.BadRequest));
            }
        }

        /// <summary>
        /// This method is better placed somewhere else to use on all controllers
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="httpStatusCode"></param>
        /// <returns></returns>
        private dynamic buildResponse(object _data, HttpStatusCode httpStatusCode)
        {
            return new { data = _data, code = httpStatusCode };
        }

    }
}
