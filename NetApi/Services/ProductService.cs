
using AutoMapper;
using FluentValidation.Results;
using NetApi.Context;
using NetApi.Interfaces;
using NetApi.Models;
using NetApi.Models.Request;
using NetApi.Models.Response;
using NetApi.Utils;
using NetApi.Validations;
using NetApi.Validations.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NetApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Response<List<ProductResponse>> GetAll()
        {
            Response<List<ProductResponse>> response = new Response<List<ProductResponse>>();

            try
            {
                List<Product> products = _context.Products
                    .Include(p => p.UserCreator)
                    .Include(p => p.UserUpdater).ToList();

                response.Success = true;
                response.Message = "Productos obtenidos correctamente";
                response.Data = _mapper.Map<List<Product>,List<ProductResponse>>(products);

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = null;

                return response;
            }
        }

        public Response<ProductResponse> GetById(int id)
        {
            Response<ProductResponse> response = new Response<ProductResponse>();

            try
            {
                Product product = _context.Products
                    .Include(p => p.UserCreator)
                    .Include(p => p.UserUpdater)
                    .SingleOrDefault(x => x.Id == id);

                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Producto no encontrado";
                    response.Data = null;

                    return response;
                }

                response.Success = true;
                response.Message = "Producto obtenido correctamente";
                response.Data = _mapper.Map<ProductResponse>(product);

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = null;

                return response;
            }
        }

        public Response<ProductResponse, List<ValidationError>> Create(ProductRequest model)
        {
            Response<ProductResponse, List<ValidationError>> response = new Response<ProductResponse, List<ValidationError>>();

            try
            {
                var validator = new CreateProductValidations();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    response.Success = false;
                    response.Message = "Error de validación";
                    response.Errors = _mapper.Map<List<ValidationFailure>, List<ValidationError>>(result.Errors);
                    response.Data = null;

                    return response;
                }

                Product entity = _mapper.Map<Product>(model);

                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = null;

                _context.Products.Add(entity);
                _context.SaveChanges();

                response.Success = true;
                response.Message = "Producto creado correctamente";
                response.Data = _mapper.Map<ProductResponse>(entity);
                response.Errors = null;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors = new List<ValidationError>() { new ValidationError("Id", ex.Message) };
                response.Data = null;

                return response;
            }
        }

        public Response<ProductResponse, List<ValidationError>> Update(ProductRequest model)
        {
            Response<ProductResponse, List<ValidationError>> response = new Response<ProductResponse, List<ValidationError>>();

            try
            {
                var validator = new UpdateProductValidations();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    response.Success = false;
                    response.Message = "Error de validación";
                    response.Errors = _mapper.Map<List<ValidationFailure>, List<ValidationError>>(result.Errors);
                    response.Data = null;

                    return response;
                }

                Product entity = _context.Products.SingleOrDefault(x => x.Id == model.Id);

                if (entity == null)
                {
                    response.Success = false;
                    response.Message = "Producto no encontrado";
                    response.Errors = new List<ValidationError>() { new ValidationError("Id", "Producto no encontrado") };
                    response.Data = null;

                    return response;
                }

                Product product = _mapper.Map<Product>(model);
                product.UpdatedAt = DateTime.Now;

                Util.UpdateProperties(entity, product);

                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();

                response.Success = true;
                response.Message = "Producto actualizado correctamente";
                response.Data = _mapper.Map<ProductResponse>(entity);
                response.Errors = null;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors = new List<ValidationError>() { new ValidationError("Id", ex.Message) };
                response.Data = null;

                return response;
            }
        }

        public Response<ProductResponse, List<ValidationError>> PartialUpdate(ProductRequest model)
        {
            Response<ProductResponse, List<ValidationError>> response = new Response<ProductResponse, List<ValidationError>>();

            try
            {
                var validator = new PartialUpdateProductValidations();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    response.Success = false;
                    response.Message = "Error de validación";
                    response.Errors = _mapper.Map<List<ValidationFailure>, List<ValidationError>>(result.Errors);
                    response.Data = null;

                    return response;
                }

                Product entity = _context.Products.SingleOrDefault(x => x.Id == model.Id);               

                if (entity == null)
                {
                    response.Success = false;
                    response.Message = "Producto no encontrado";
                    response.Errors = new List<ValidationError>() { new ValidationError("Id", "Producto no encontrado") };
                    response.Data = null;

                    return response;
                }

                Product product = _mapper.Map<Product>(model);
                product.UpdatedAt = DateTime.Now;

                Util.UpdateProperties(entity, product);                

                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();

                response.Success = true;
                response.Message = "Producto actualizado correctamente";
                response.Data = _mapper.Map<ProductResponse>(entity);
                response.Errors = null;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Errors = new List<ValidationError>() { new ValidationError("Id", ex.Message) };
                response.Data = null;

                return response;
            }
        }
    }
}