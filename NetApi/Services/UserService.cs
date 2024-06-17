
using AutoMapper;
using FluentValidation.Results;
using NetApi.Context;
using NetApi.Interfaces;
using NetApi.Models;
using NetApi.Models.Request;
using NetApi.Models.Response;
using NetApi.Utils;
using NetApi.Validations.User;
using System.Collections.Generic;
using System.Linq;

namespace NetApi.Services
{
    public class UserService : IUserService
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public UserService(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Response<List<UserResponse>> GetAll()
        {
            Response<List<UserResponse>> response = new Response<List<UserResponse>>();

            try
            {
                List<User> users = _context.Users.ToList();

                response.Success = true;
                response.Message = "Usuarios obtenidos correctamente";
                response.Data = _mapper.Map<List<User>, List<UserResponse>>(users);

                return response;
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = null;

                return response;
            }
        }

        public Response<UserResponse> GetById(int id)
        {
            Response<UserResponse> response = new Response<UserResponse>();

            try
            {
                User user = _context.Users.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Usuario no encontrado";
                    response.Data = null;

                    return response;
                }

                response.Success = true;
                response.Message = "Usuario obtenido correctamente";
                response.Data = _mapper.Map<User, UserResponse>(user);

                return response;
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = null;

                return response;
            }
        }

        public Response<UserResponse, List<ValidationError>> Create(UserRequest user)
        {
            Response<UserResponse, List<ValidationError>> response = new Response<UserResponse, List<ValidationError>>();

            try
            {
                var validator = new CreateUserValidations();
                var result = validator.Validate(user);

                if (!result.IsValid)
                {
                    response.Success = false;
                    response.Message = "Validación fallida";
                    response.Errors = _mapper.Map<List<ValidationFailure>,List<ValidationError>>(result.Errors);
                    response.Data = null;

                    return response;
                }

                User entity = _mapper.Map<UserRequest, User>(user);
                entity.UpdatedAt = null;

                _context.Users.Add(entity);
                _context.SaveChanges();

                response.Success = true;
                response.Message = "Usuario creado correctamente";
                response.Data = _mapper.Map<User, UserResponse>(entity);

                return response;
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = null;
                response.Errors = new List<ValidationError>() { new ValidationError("Id", ex.Message) };

                return response;
            }
        }

        public Response<UserResponse, List<ValidationError>> Update(UserRequest user)
        {
            Response<UserResponse, List<ValidationError>> response = new Response<UserResponse, List<ValidationError>>();

            try
            {
                var validator = new UpdateUserValidations();
                var result = validator.Validate(user);

                if (!result.IsValid)
                {
                    response.Success = false;
                    response.Message = "Validación fallida";
                    response.Errors = _mapper.Map<List<ValidationFailure>, List<ValidationError>>(result.Errors);
                    response.Data = null;

                    return response;
                }

                User entity = _context.Users.FirstOrDefault(u => u.Id == user.Id);

                if (entity == null)
                {
                    response.Success = false;
                    response.Message = "Usuario no encontrado";
                    response.Data = null;

                    return response;
                }

                User model = _mapper.Map<UserRequest, User>(user);

                Util.UpdateProperties(entity, model);

                _context.SaveChanges();

                response.Success = true;
                response.Message = "Usuario actualizado correctamente";
                response.Data = _mapper.Map<User, UserResponse>(entity);

                return response;
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = null;
                response.Errors = new List<ValidationError>() { new ValidationError("Id", ex.Message) };

                return response;
            }
        }

        public Response<UserResponse, List<ValidationError>> PartialUpdate(UserRequest model)
        {
            Response<UserResponse, List<ValidationError>> response = new Response<UserResponse, List<ValidationError>>();

            try
            {
                var validator = new PartialUpdateUserValidations();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    response.Success = false;
                    response.Message = "Validación fallida";
                    response.Errors = _mapper.Map<List<ValidationFailure>, List<ValidationError>>(result.Errors);
                    response.Data = null;

                    return response;
                }

                User entity = _context.Users.FirstOrDefault(u => u.Id == model.Id);

                if (entity == null)
                {
                    response.Success = false;
                    response.Message = "Usuario no encontrado";
                    response.Data = null;

                    return response;
                }

                User user = _mapper.Map<UserRequest, User>(model);

                Util.UpdateProperties(entity, user);

                _context.SaveChanges();

                response.Success = true;
                response.Message = "Usuario actualizado correctamente";
                response.Data = _mapper.Map<User, UserResponse>(entity);

                return response;
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = null;
                response.Errors = new List<ValidationError>() { new ValidationError("Id", ex.Message) };

                return response;
            }
        }
    }
}