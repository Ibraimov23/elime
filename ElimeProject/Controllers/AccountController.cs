using ElimeProject.Data;
using ElimeProject.Models;
using ElimeProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ElimeProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _applicationContext;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext applicationContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationContext = applicationContext;
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            User customUser = await _userManager.FindByEmailAsync(email);
            if (customUser != null)
                return Json(true);
            else
                return Json(false);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["Layout"] = "_Layout2";
            ViewData["color"] = "background-color: #FFFFFF;";
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.Users.FirstOrDefaultAsync(p => p.UserName == model.Login);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
                else
                {
                    Microsoft.AspNetCore.Identity.SignInResult result =
                            await _signInManager.PasswordSignInAsync(user,
                                model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }

                    else
                    {
                        ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    }
                }
            }
            ViewData["Layout"] = "_Layout2";
            ViewData["color"] = "background-color: #FFFFFF;";
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> UserProfiles(int? pageNumber, string searchString, int pageSize = 2)
        {
            User user = await _userManager.GetUserAsync(User);
            IQueryable<User> users = _applicationContext.Users.Where(p => p.Id != user.Id);

            if (!String.IsNullOrEmpty(searchString) && searchString.Length > 2)
            {
                users = users.Where(u =>
                  u.UserName.Contains(searchString) && u.Id != user.Id ||
                  u.Email.Contains(searchString) && u.Id != user.Id);
            }
            UsersListViewModel viewModel = new UsersListViewModel
            {
                Users = users,
                PageViewModel = await PaginatedListViewModel<User>.CreateAsync(users.OrderBy(p => p.Registered_At), pageNumber ?? 1, pageSize)
            };

            if (!String.IsNullOrEmpty(searchString)) return PartialView(viewModel);
            else return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddingUsers()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddingUsers(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                int index = model.Email.IndexOf('@');
                string Login = model.Email.Remove(index);
                User user = new User
                {
                    FullName = model.FullName,
                    CompanyName = model.CompanyName,
                    PhoneNumber = model.Phone,
                    Inn = model.Inn,
                    Ogrn = model.Ogrn,
                    Email = model.Email,
                    UserName = Login
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserProfiles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        /*[ValidateAntiForgeryToken]*/
        [Authorize]
        public async Task<IActionResult> DeleteUsers(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                _applicationContext.Remove(user);
                await _applicationContext.SaveChangesAsync();
            }
            return RedirectToAction("UserProfiles");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}