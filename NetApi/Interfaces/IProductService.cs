
using NetApi.Models.Request;
using NetApi.Models.Response;
using System.Collections.Generic;

namespace NetApi.Interfaces
{
    public interface IProductService
    {
        Response<List<ProductResponse>> GetAll();
        Response<ProductResponse> GetById(int id);
        Response<ProductResponse, List<ValidationError>> Create(ProductRequest model);
        Response<ProductResponse, List<ValidationError>> Update(ProductRequest model);
        Response<ProductResponse, List<ValidationError>> PartialUpdate(ProductRequest model);
    }
}