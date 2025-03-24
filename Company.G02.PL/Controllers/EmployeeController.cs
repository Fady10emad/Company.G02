using AutoMapper;
using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G02.DAL.Models;
using Company.G02.PL.Dots;
using Company.G02.PL.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IunitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository employeeRepository,
            //IDepartmentRepository departmentRepository,
            IunitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var emps = await _unitOfWork.EmployeeRepository.GetAllAsync();
                return View(emps);

            }
            else
            {
            var emps2 = await _unitOfWork.EmployeeRepository.GetByNameAsync(name);
            return View(emps2);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var depts = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewBag.Departments = depts;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(model.Image is not null)
            {
              model.ImageName = DocumentSettings.UploadFile(model.Image, "images");
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
           await  _unitOfWork.EmployeeRepository.AddAsync(emp);
            var res =await _unitOfWork.CompleteAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return BadRequest("invalid id");
            }
            var Emp = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (Emp is null)
            {
                return NotFound(new { statusCode = 404, message = $"Department with Id : {id} is not found" });
            }
            return View(Emp);
        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var depts = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewBag.Departments = depts;

            var emp = await _unitOfWork.EmployeeRepository.GetAsync(id);

            //EmployeeDto model = new EmployeeDto()
            //{
            //    Name=emp.Name,
            //    Age = emp.Age,
            //    Email = emp.Email,
            //    CreatedAt = emp.CreatedAt,
            //    HiringDate = emp.HiringDate,
            //    IsActive = emp.IsActive,
            //    IsDeleted = emp.IsDeleted,
            //    Phone = emp.Phone,
            //    Salary=emp.Salary,
            //    Address = emp.Address
            //};

           var model = _mapper.Map<EmployeeDto>(emp);


            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id,EmployeeDto employee)
        {
            if (ModelState.IsValid)
            {
                //var emp = new Employee()
                //{
                //    Id = id,
                //    Name = employee.Name,
                //    Age = employee.Age,
                //    Address = employee.Address,
                //    Phone = employee.Phone,
                //    Salary = employee.Salary,
                //    IsActive = employee.IsActive,
                //    CreatedAt = employee.CreatedAt,
                //    Email = employee.Email,
                //    HiringDate = employee.HiringDate,
                //    IsDeleted = employee.IsDeleted,
                //    DepartmentId = employee.DepartmentId

                //};


                if(employee.ImageName is not null && employee.Image is not null)
                {
                    DocumentSettings.DeleteFile(employee.ImageName, "images");
                }
                if (employee.Image is not null)
                {
                   employee.ImageName = DocumentSettings.UploadFile(employee.Image, "images");
                }

               var emp = _mapper.Map<Employee>(employee);

              _unitOfWork.EmployeeRepository.Update(emp);
                int res = await _unitOfWork.CompleteAsync();
                if (res == 0) return View(employee);
                return RedirectToAction("Index");
            }
            return View(employee);

        }


        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _unitOfWork.EmployeeRepository.GetAsync(id);
            if (emp != null)
            {
                _unitOfWork.EmployeeRepository.Delete(emp);
               await _unitOfWork.CompleteAsync();
            }
            return RedirectToAction("Index");
        }


    }
}
