using FluentValidation.Results;
using Microsoft.IdentityModel.Tokens;
using NetApi.App_Start;
using NetApi.Context;
using NetApi.Interfaces;
using NetApi.Models;
using NetApi.Models.Request;
using NetApi.Models.Response;
using NetApi.Validations.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using BC = BCrypt.Net;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace NetApi.Services
{
    public class AuthService : IAuth
    {
        private readonly ApiContext _bd;

        public AuthService(ApiContext bd)
        {
            _bd = bd;
        }

        public Response<AuthResponse, List<ValidationFailure>> Auth(LoginRequest model)
        {
            Response<AuthResponse, List<ValidationFailure>> userResponse = new Response<AuthResponse, List<ValidationFailure>>();
            try
            {
                var validator = new LoginValidations();

                ValidationResult results = validator.Validate(model);

                if (!results.IsValid)
                {
                    userResponse.Success = false;
                    userResponse.Message = "Usuario y/o contraseña invalidos";
                    userResponse.Data = null;
                    userResponse.Errors = results.Errors;

                    return userResponse;
                }

                User oUser = _bd.Users
                    .Where(u => u.UserName == model.UserName && u.Active == true)
                    .FirstOrDefault();

                if (oUser == null)
                {
                    userResponse.Success = false;
                    userResponse.Message = "Usuario y/o Contraseña invalidos";
                    userResponse.Data = null;
                    userResponse.Errors = results.Errors;

                    return userResponse;
                }

                if (!BC.BCrypt.Verify(model.Password, oUser.Password))
                {
                    userResponse.Success = false;
                    userResponse.Message = "Usuario y/o Contraseña invalidos";
                    userResponse.Data = null;
                    userResponse.Errors = results.Errors;

                    return userResponse;
                }

                AuthResponse auth = new AuthResponse
                {
                    Email = oUser.Email,
                    Token = GenerateToken(oUser),
                    Name = oUser.Name,
                    UserName = oUser.UserName,
                    UserId = oUser.Id,
                };

                userResponse.Data = auth;
                userResponse.Message = "Login successful";
                userResponse.Success = true;

                return userResponse;
            }
            catch (Exception)
            {
                userResponse.Success = false;
                userResponse.Message = "Upss hubo un error";
                userResponse.Data = null;

                return userResponse;
            }
        }

        private string GenerateToken(User user)
        {
            var key = Startup.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
           {
               new Claim(ClaimTypes.Name, user.Name),
               new Claim(ClaimTypes.Role, "Admin"),
               new Claim(ClaimTypes.Sid, user.Id.ToString()),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(ClaimTypes.UserData, user.UserName)
           };

            var jwtToken = new JwtSecurityToken(
                 issuer: Startup.GetIssuer(),
                 audience: Startup.GetAudience(),
                 expires: DateTime.Now.AddHours(4),
                 signingCredentials: credentials,
                 claims: claims
            );

            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return tokenHandler;
        }
    }
}