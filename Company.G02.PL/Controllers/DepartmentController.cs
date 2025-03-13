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


    }
}
