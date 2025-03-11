using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
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
    }
}
