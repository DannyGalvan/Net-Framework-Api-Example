
using System;
using System.Collections.Generic;

namespace NetApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool Active { get; set; }
        public bool Confirm { get; set; }
        public bool Reset { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DateToken { get; set; }
        public string Number { get; set; } = string.Empty;
        public string RecoveryToken { get; set; }

        public virtual List<Product> ProductsCreated { get; set; }
        public virtual List<Product> ProductsUpdated { get; set; }
    }
}