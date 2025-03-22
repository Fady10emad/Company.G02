using AutoMapper;
using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G02.DAL.Models;
using Company.G02.PL.Dots;
using Microsoft.AspNetCore.Mvc;

namespace Company.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public IActionResult Index(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var emps = _employeeRepository.GetAll();
                return View(emps);

            }
            else
            {
            var emps2 = _employeeRepository.GetByName(name);
            return View(emps2);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var depts = _departmentRepository.GetAll();
            ViewBag.Departments = depts;

            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            //var emp = new Employee()
            //{
            //    Name = model.Name,
            //    Age = model.Age,
            //    Email = model.Email,
            //    Address = model.Address,
            //    Phone = model.Phone,
            //    Salary = model.Salary,
            //    IsActive = model.IsActive,
            //    IsDeleted = model.IsDeleted,
            //    HiringDate = model.HiringDate,
            //    CreatedAt = model.CreatedAt,
            //    DepartmentId = model.DepartmentId

            //};
           var emp = _mapper.Map<Employee>(model);
           var res = _employeeRepository.Add(emp);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest("invalid id");
            }
            var Emp = _employeeRepository.Get(id.Value);
            if (Emp is null)
            {
                return NotFound(new { statusCode = 404, message = $"Department with Id : {id} is not found" });
            }
            return View(Emp);
        }


        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            var depts = _departmentRepository.GetAll();
            ViewBag.Departments = depts;

            var emp = _employeeRepository.Get(id);

            EmployeeDto model = new EmployeeDto()
            {
                Name=emp.Name,
                Age = emp.Age,
                Email = emp.Email,
                CreatedAt = emp.CreatedAt,
                HiringDate = emp.HiringDate,
                IsActive = emp.IsActive,
                IsDeleted = emp.IsDeleted,
                Phone = emp.Phone,
                Salary=emp.Salary,
                Address = emp.Address
            };


            return View(model);

        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id,EmployeeDto employee)
        {
            if (ModelState.IsValid)
            {
                var emp = new Employee()
                {
                    Id = id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Phone = employee.Phone,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    CreatedAt = employee.CreatedAt,
                    Email = employee.Email,
                    HiringDate = employee.HiringDate,
                    IsDeleted = employee.IsDeleted,
                    DepartmentId = employee.DepartmentId

                };
              int res = _employeeRepository.Update(emp);
                if (res == 0) return View(employee);
                return RedirectToAction("Index");
            }
            return View(employee);

        }


        public IActionResult Delete(int id)
        {
            var emp = _employeeRepository.Get(id);
            if (emp != null)
            {
                _employeeRepository.Delete(emp);
            }
            return RedirectToAction("Index");
        }


    }
}
