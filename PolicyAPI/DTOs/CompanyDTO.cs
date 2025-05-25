using System.ComponentModel.DataAnnotations;

namespace PolicyAPI.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 50000000, ErrorMessage = "Salary offered must be between 0 and 5 crores")]
        public decimal SalaryOffered { get; set; }

        [Required(ErrorMessage = "Company category is required")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
        public string Category { get; set; } = string.Empty;

        public bool IsHighPaying { get; set; }
    }
}
