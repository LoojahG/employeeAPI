using System.ComponentModel.DataAnnotations;

namespace Employee_API.DTOs
{
    public class UpdateEmployeeDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(18, 70)]
        public int Age { get; set; }
    }
}
