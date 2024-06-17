using FluentValidation.Results;
using NetApi.Models.Request;
using NetApi.Models.Response;
using System.Collections.Generic;

namespace NetApi.Interfaces
{
    public interface IAuth
    {
        Response<AuthResponse, List<ValidationFailure>> Auth(LoginRequest model);
    }
}
