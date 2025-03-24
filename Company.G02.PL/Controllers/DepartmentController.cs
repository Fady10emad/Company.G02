using AutoMapper;
using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G02.DAL.Models;
using Company.G02.PL.Dots;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.G02.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(
            //IDepartmentRepository departmentRepository
            IunitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_departmentRepository = departmentRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var departments =await _unitOfWork.DepartmentRepository.GetAllAsync();

            //ViewData["messsage"] = "Hello from Details view data";
            ViewBag.Message = "Hello from Details view Bag";

            return View(departments);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentDots dept)
        {
            if (!ModelState.IsValid)
            {
                return View(dept);
            }

            //var department = new Department()
            //{
            //    Code = dept.Code,
            //    Name = dept.Name,
            //    CreatedeAt = dept.CreatedeAt
            //};

           var department = _mapper.Map<Department>(dept);

          await  _unitOfWork.DepartmentRepository.AddAsync(department);
           await _unitOfWork.CompleteAsync();


            TempData["Message"] = "Department added successfully";


            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {

            if (id is null)
            {
                return BadRequest("invalid id");
            }
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null)
            {
                return NotFound(new { statusCode =404 , message=$"Department with Id : {id} is not found" });
            }
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest("invalid id");


            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            return View(department);

        }

        //[HttpPost]
        //public IActionResult Edit([FromRoute]int id,Department dept)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if(id!=dept.Id) return BadRequest();
        //        int res = _departmentRepository.Update(dept);
        //        if (res > 0)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    return View(dept);

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentDots dept)
        {
            if (ModelState.IsValid)
            {
                //var department = new Department()
                //{
                //    Id = id,
                //    Code = dept.Code,
                //    Name = dept.Name,
                //    CreatedeAt = dept.CreatedeAt
                //};

               var department = _mapper.Map<Department>(dept);

                 _unitOfWork.DepartmentRepository.Update(department);
                int res = await _unitOfWork.CompleteAsync();
                if (res > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(dept);

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest("invalid id");


            var department =await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            //var dept = new DepartmentDots()
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    CreatedeAt = department.CreatedeAt
            //};

           var dept = _mapper.Map<DepartmentDots>(department);

            return View(dept);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int id,DepartmentDots dept)
        {

            //var department = new Department()
            //{
            //    Id = id,
            //    Code = dept.Code,
            //    Name = dept.Name,
            //    CreatedeAt = dept.CreatedeAt
            //};

            var department = _mapper.Map<Department>(dept);


             _unitOfWork.DepartmentRepository.Delete(department);
            int res = await _unitOfWork.CompleteAsync();
            if (res>=0) return RedirectToAction("Index");
            return View(dept);

        }

    }
}
