using System;
using System.ComponentModel.DataAnnotations;

namespace Company.G02.PL.Dots
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Hiring Date is required")]
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; }

        [Required(ErrorMessage = "Created At date is required")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }


        public int? DepartmentId { get; set; }


        public string? ImageName { get; set; }
        public IFormFile? Image { get; set; }
    }
}