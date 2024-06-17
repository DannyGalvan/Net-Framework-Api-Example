
namespace NetApi.Models.Request
{
    public class ProductRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal? Cost { get; set; }
        public decimal? SalePrice { get; set; }
        public int? Stock { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}