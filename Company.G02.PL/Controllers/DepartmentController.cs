using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G02.DAL.Models;
using Company.G02.PL.Dots;
using Microsoft.AspNetCore.Mvc;

namespace Company.G02.PL.Controllers
{
    public class DepartmentController : Controller
    {
       private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }


        [HttpGet]
        public IActionResult Index()
        {

            var departments = _departmentRepository.GetAll();

            return View(departments);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentDots dept)
        {
            if (!ModelState.IsValid)
            {
                return View(dept);
            }

            var department = new Department()
            {
                Code = dept.Code,
                Name = dept.Name,
                CreatedeAt = dept.CreatedeAt
            };

            _departmentRepository.Add(department);
            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest("invalid id");
            }
            var department = _departmentRepository.Get(id.Value);
            if (department is null)
            {
                return NotFound(new { statusCode =404 , message=$"Department with Id : {id} is not found" });
            }
            return View(department);
        }
    }
}
