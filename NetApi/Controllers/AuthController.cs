using FluentValidation.Results;
using NetApi.Interfaces;
using NetApi.Models.Request;
using NetApi.Models.Response;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace NetApi.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Login(LoginRequest model)
        {
            var response = _authService.Auth(model);

            if (response.Success)
            {
                Response<AuthResponse> authResponse = new Response<AuthResponse>()
                {
                    Data = response.Data,
                    Success = response.Success,
                    Message = response.Message
                };

                return Ok(authResponse);
            }
            else
            {

                Response<List<ValidationFailure>> errorResponse = new Response<List<ValidationFailure>>()
                {
                    Data = response.Errors,
                    Success = response.Success,
                    Message = response.Message
                };

                return Content(HttpStatusCode.BadRequest, errorResponse);
            }
        }
    }
}
