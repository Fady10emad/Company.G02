using Company.G02.DAL.Models;
using Company.G02.PL.Dots;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.G02.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region SignUp

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is null)
                {
                    user =await _userManager.FindByEmailAsync(model.Email);

                    if (user is null)
                    {
                        user = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree,
                        };

                        var res = await _userManager.CreateAsync(user, model.Password);

                        if (res.Succeeded)
                        {
                            return RedirectToAction("SignIn");
                        }

                        foreach (var error in res.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }



                }

                ModelState.AddModelError("", "Invalid SignUp");


            }


            return View(model);
        }

        #endregion

        #region SignIn


        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto Model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(Model.Email);

                if (User != null)
                {
                    var res = await _userManager.CheckPasswordAsync(User, Model.Password);
                    if (res)
                    {
                        var res2 = await _signInManager.PasswordSignInAsync(User,Model.Password,Model.RememberMe,false);

                        if (res2.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                            
                        }


                    }
                }

                ModelState.AddModelError("", "Invalid SignIn");

            }

            return View(Model);
        }


        #endregion

        #region SignOut

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }



        #endregion

    }
}
