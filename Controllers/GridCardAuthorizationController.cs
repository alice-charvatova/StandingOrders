using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StandingOrders.API.Models.Dto;
using StandingOrders.API.Services;
using StandingOrders.API.Services.Encryption;
using System;
using System.Globalization;
using System.Text;

namespace StandingOrders.API.Controllers
{
    [ApiController]
    [Route("api/grid-card")]
    public class GridCardAuthorizationController : ControllerBase
    {

        private readonly IAuthorizationService _authorization;
        private readonly IEncryptionService _encryption;

        public GridCardAuthorizationController(
            IAuthorizationService authorization,
            IEncryptionService encryption)
        {
            _authorization = authorization;
            _encryption = encryption;
        }

        /// <summary>
        /// Generates coordinates for the grid card authentication.
        /// </summary> 
        /// <remarks>
        /// Sends randomly generated grid card coordinates in the form of a 2-digit number.
        /// </remarks>
        /// <response code="200">Success.</response> 
        [HttpGet("init")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public IActionResult GetCoordinate()
        {
            int coordinate = _authorization.GenerateCoordinate();

            return Ok(coordinate);
        }

        /// <summary>
        /// Authorizes PIN code inserted by user.
        /// </summary> 
        /// <remarks>
        /// Recieves authorization object (PIN code inserted by user and originally generated coordinates), 
        /// than finds out if the PIN code matches the coordinates. If there is a match, sends encrypted token.
        /// </remarks>
        /// <response code="200">Success.</response> 
        /// <response code="400">If the input PIN code is invalid.</response>  
        [HttpPost("validate")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult AuthorizeCode([FromBody] AuthorizationDto authorizationDto)
        {

            if (_authorization.ValidatePinCode(authorizationDto))
            {
                string token = DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                string encryptedToken = _encryption.Encrypt(token);
                return Ok(encryptedToken);
            }
            else
            {
                return BadRequest("PIN code invalid.");
            }

        }
    }
}
