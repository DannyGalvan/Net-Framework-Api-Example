using System;
using System.ComponentModel.DataAnnotations;

namespace NetApi.Models.Request
{
    
    public class UserRequest
    {
        public int? Id { get; set; }
        [EmailAddress(ErrorMessage = "Dirección de correo invalida")]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool Active { get; set; }
        public string Number { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }       
    }
}