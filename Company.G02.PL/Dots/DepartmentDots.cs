using System.ComponentModel.DataAnnotations;

namespace Company.G02.PL.Dots
{
    public class DepartmentDots
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Creation date is required.")]
        public DateTime CreatedeAt { get; set; }
    }
}
