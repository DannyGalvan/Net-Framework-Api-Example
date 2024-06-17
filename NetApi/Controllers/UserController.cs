using NetApi.Interfaces;
using System.Web.Http;
using System.Net;
using NetApi.Models.Request;
using System.Collections.Generic;
using NetApi.Models.Response;

namespace NetApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IHttpActionResult Get()
        {
            var response = _userService.GetAll();

            if (response.Success)
            {
                return Ok(response);
            }

            return Content(HttpStatusCode.BadRequest, response);
        }

        public IHttpActionResult Get(int id)
        {
            var response = _userService.GetById(id);

            if (response.Success)
            {
                return Ok(response);
            }

            return Content(HttpStatusCode.BadRequest, response);
        }

        public IHttpActionResult Post([FromBody] UserRequest value)
        {
            Response<List<ValidationError>> errorResponse = new Response<List<ValidationError>>();

            if (value == null)
            {
                errorResponse.Success = false;
                errorResponse.Message = "No se recibieron datos";
                errorResponse.Data = new List<ValidationError>() { new ValidationError("Id", "No se recibieron datos") };

                return Content(HttpStatusCode.BadRequest, errorResponse);
            }

            var response = _userService.Create(value);

            if (response.Success)
            {
                Response<UserResponse> successResponse = new Response<UserResponse>
                {
                    Success = true,
                    Message = response.Message,
                    Data = response.Data
                };

                return Ok(successResponse);
            }

            errorResponse.Data = response.Errors;
            errorResponse.Success = response.Success;
            errorResponse.Message = response.Message;

            return Content(HttpStatusCode.BadRequest, errorResponse);
        }

        public IHttpActionResult Put([FromBody] UserRequest value)
        {
            Response<List<ValidationError>> errorResponse = new Response<List<ValidationError>>();

            if (value == null)
            {
                errorResponse.Success = false;
                errorResponse.Message = "No se recibieron datos";
                errorResponse.Data = new List<ValidationError>() { new ValidationError("Id", "No se recibieron datos") };

                return Content(HttpStatusCode.BadRequest, errorResponse);
            }

            var response = _userService.Update(value);

            if (response.Success)
            {
                Response<UserResponse> successResponse = new Response<UserResponse>
                {
                    Success = true,
                    Message = response.Message,
                    Data = response.Data
                };

                return Ok(successResponse);
            }

            errorResponse.Data = response.Errors;
            errorResponse.Success = response.Success;
            errorResponse.Message = response.Message;

            return Content(HttpStatusCode.BadRequest, errorResponse);
        }

        public IHttpActionResult Patch([FromBody] UserRequest value)
        {
            Response<List<ValidationError>> errorResponse = new Response<List<ValidationError>>();

            if (value == null)
            {
                errorResponse.Success = false;
                errorResponse.Message = "No se recibieron datos";
                errorResponse.Data = new List<ValidationError>() { new ValidationError("Id", "No se recibieron datos") };

                return Content(HttpStatusCode.BadRequest, errorResponse);
            }

            var response = _userService.PartialUpdate(value);

            if (response.Success)
            {
                Response<UserResponse> successResponse = new Response<UserResponse>
                {
                    Success = true,
                    Message = response.Message,
                    Data = response.Data
                };

                return Ok(successResponse);
            }

            errorResponse.Data = response.Errors;
            errorResponse.Success = response.Success;
            errorResponse.Message = response.Message;

            return Content(HttpStatusCode.BadRequest, errorResponse);
        }
    }
}