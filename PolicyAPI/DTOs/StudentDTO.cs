using System.ComponentModel.DataAnnotations;

namespace PolicyAPI.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Student name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Range(0.0, 10.0, ErrorMessage = "CGPA must be between 0 and 10")]
        public double Cgpa { get; set; }

        public bool IsPlaced { get; set; }

        [Range(0, 50000000, ErrorMessage = "Current salary must be between 0 and 5 crores")]
        public decimal CurrentSalary { get; set; }

        [Range(0, 10, ErrorMessage = "Companies applied cannot be negative or exceed 10")]
        public int CompaniesApplied { get; set; }

        [Range(0, 50000000, ErrorMessage = "Dream offer must be between 0 and 5 crores")]
        public decimal DreamOffer { get; set; }

        [StringLength(50, ErrorMessage = "Dream company name cannot exceed 50 characters")]
        public string DreamCompany { get; set; } = string.Empty;
    }
}
