namespace Company.G02.PL.Dots
{
    public class EmployeeDto
    {
        public string Name { get; set; }

        public int? Age { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime HiringDate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
