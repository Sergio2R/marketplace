using System;
using System.Threading.Tasks;
using Marketplace.Bl;
using Marketplace.Core.Bl;
using Marketplace.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Marketplace.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserBl userBl;

        public UserController(ILogger<UserController> logger, IUserBl userBl)
        {
            this.logger = logger;
            this.userBl = userBl;
        }

        /// <summary>
        /// Retrieves the list of users.
        /// </summary>
        /// <returns>A collection of users.</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var users = await userBl.GetUsersAsync().ConfigureAwait(false);
                return Ok(users);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }
        }

        /// <summary>
        /// Validates the existence of a user by username and retrieves their ID.
        /// </summary>
        /// <param name="username">The username of the user to validate.</param>
        /// <returns>The ID of the user if found, otherwise null.</returns>
        [HttpGet("validate-existence")]
        public async Task<ActionResult<int?>> GetUserIdByUsername(string username)
        {
            try
            {
                var userId = await userBl.GetUserIdByUsername(username).ConfigureAwait(false);
                return userId;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
