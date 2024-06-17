
using NetApi.Models.Request;
using NetApi.Models.Response;
using System.Collections.Generic;

namespace NetApi.Interfaces
{
    public interface IUserService
    {
        Response<List<UserResponse>> GetAll();
        Response<UserResponse> GetById(int id);
        Response<UserResponse, List<ValidationError>> Create(UserRequest model);
        Response<UserResponse, List<ValidationError>> Update(UserRequest model);
        Response<UserResponse, List<ValidationError>> PartialUpdate(UserRequest model);
    }
}