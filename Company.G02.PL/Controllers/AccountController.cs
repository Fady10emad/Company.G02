using Company.G02.DAL.Models;
using Company.G02.PL.Dots;
using Company.G02.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
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


        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(FrogetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var url = Url.Action("ResetPassword","Account",new {email=model.Email,token},Request.Scheme);

                    //Send Email
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = url
                    };



                   var flag = EmailSettings.sendEmail(email);
                    if(flag)
                    {
                        //Redirect to Reset Password Page
                        return RedirectToAction("CheckYourInbox");

                    }
                }
            }



            ModelState.AddModelError("", "Invalid Email");
            return View("ForgetPassword");
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        #endregion

        #region Reset Password

        [HttpGet]
        public IActionResult ResetPassword(string email , string token)
        {
            TempData["Email"] = email;
            TempData["Token"] = token;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["Email"].ToString();
                var token = TempData["Token"].ToString();


                if (email is null || token is null) return BadRequest();
                var user = _userManager.FindByEmailAsync(email).Result;
                if (user is not null)
                {

                    var res = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                    if (res.Succeeded)
                    {
                        return RedirectToAction("SignIn");
                    }
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }

                    ModelState.AddModelError("", "invalid");

                }
            }

                     return View(model);
        }



        #endregion


    }
}
