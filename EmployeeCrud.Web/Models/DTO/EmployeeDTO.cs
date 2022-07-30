using System.ComponentModel.DataAnnotations;

namespace EmployeeCrud.Web.Models.DTO
{
    public class EmployeeDTO
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
    }
}
