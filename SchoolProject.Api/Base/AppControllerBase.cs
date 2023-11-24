using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Bases;
using System.Net;

namespace SchoolProject.Api.Base
{
    [ApiController]
    public class AppControllerBase : ControllerBase
    {
        #region Actions

        public ObjectResult NewResult<T>(Response<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response);

                case HttpStatusCode.Created:
                    return Created(string.Empty, response);

                case HttpStatusCode.Unauthorized:
                    return Unauthorized(response);

                case HttpStatusCode.BadRequest:
                    return BadRequest(response);

                case HttpStatusCode.NotFound:
                    return NotFound(response);

                case HttpStatusCode.Accepted:
                    return Accepted(string.Empty, response);

                case HttpStatusCode.UnprocessableEntity:
                    return UnprocessableEntity(response);

                default:
                    return BadRequest(response);
            }
        }

        #endregion Actions
    }
}