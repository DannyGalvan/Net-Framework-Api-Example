

namespace NetApi.Models.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal Stock { get; set; }
        public int State { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual UserResponse UserCreator { get; set; }
        public virtual UserResponse UserUpdater { get; set; }
    }
}