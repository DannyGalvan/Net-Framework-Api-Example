using NetApi.Interfaces;
using NetApi.Models.Request;
using NetApi.Models.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace NetApi.Controllers
{
    /// <summary>
    /// Controlador de Productos.
    /// </summary>
    [Authorize]
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="productService">Servicio de productos inyectado</param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        // GET api/values
        public IHttpActionResult Get()
        {
           var response = _productService.GetAll();

            if (response.Success)
            {
                return Ok(response);
            }

            return Content(HttpStatusCode.BadRequest, response);
        }

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto.</param>
        /// <returns>Producto.</returns>
        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            var response = _productService.GetById(id);

            if (response.Success)
            {
                return Ok(response);
            }

            return Content(HttpStatusCode.BadRequest, response);
        }

        /// <summary>
        /// Crea un producto.
        /// </summary>
        /// <param name="value">Producto a crear</param>
        /// <returns>Producto creado</returns>
        // POST api/values
        public IHttpActionResult Post([FromBody] ProductRequest value)
        {
            
            Response<List<ValidationError>> errorResponse = new Response<List<ValidationError>>();

            if (value == null)
            {
                errorResponse.Success = false;
                errorResponse.Message = "No se recibieron datos";
                errorResponse.Data = new List<ValidationError>() { new ValidationError("Id","No se recibieron datos")};

                return Content(HttpStatusCode.BadRequest, errorResponse);
            }

            value.CreatedBy = GetUserId();
            var response = _productService.Create(value);

            if (response.Success)
            {
                Response<ProductResponse> successResponse = new Response<ProductResponse>
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

        /// <summary>
        /// Modifica un producto.
        /// </summary>
        /// <param name="value">Producto a modificar</param>
        /// <returns>Producto modificado</returns>
        // PUT api/values
        public IHttpActionResult Put([FromBody] ProductRequest value)
        {
            Response<List<ValidationError>> errorResponse = new Response<List<ValidationError>>();

            if (value == null)
            {
                errorResponse.Success = false;
                errorResponse.Message = "No se recibieron datos";
                errorResponse.Data = new List<ValidationError>() { new ValidationError("Id", "No se recibieron datos") };

                return Content(HttpStatusCode.BadRequest, errorResponse);
            }

            value.UpdatedBy = GetUserId();
            var response = _productService.Update(value);

            if (response.Success)
            {
                Response<ProductResponse> successResponse = new Response<ProductResponse>();

                successResponse.Success = true;
                successResponse.Message = response.Message;
                successResponse.Data = response.Data;

                return Ok(successResponse);
            }

            errorResponse.Data = response.Errors;
            errorResponse.Success = response.Success;
            errorResponse.Message = response.Message;

            return Content(HttpStatusCode.BadRequest, errorResponse);
        }

        /// <summary>
        /// Modifica un producto parcialmente.
        /// </summary>
        /// <param name="value">Producto a modificar</param>
        /// <returns>Producto modificado</returns>
        // Patch api/values
        public IHttpActionResult Patch([FromBody] ProductRequest value)
        {
            Response<List<ValidationError>> errorResponse = new Response<List<ValidationError>>();

            if (value == null)
            {
                errorResponse.Success = false;
                errorResponse.Message = "No se recibieron datos";
                errorResponse.Data = new List<ValidationError>() { new ValidationError("Id", "No se recibieron datos") };

                return Content(HttpStatusCode.BadRequest, errorResponse);
            }

            value.UpdatedBy = GetUserId();
            var response = _productService.PartialUpdate(value);

            if (response.Success)
            {
                Response<ProductResponse> successResponse = new Response<ProductResponse>
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

        private int GetUserId()
        {
            try
            {
                Claim claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);

                return int.Parse(claim.Value);
            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
