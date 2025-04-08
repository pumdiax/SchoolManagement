using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Encrypter;
namespace SchoolManagement.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult UserValidation(LoginDetails loginDetails)
    {
        try
        {   if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "All Feilds are Required !");
                return View("Login", loginDetails);
            }
            var user = _context.UserDetail.FirstOrDefault(u => u.UserEmail == loginDetails.UserEmail && u.UserPassword == loginDetails.UserPassword);
            if (user != null)
            {
                string role = _context.Roles.FirstOrDefault(r => r.RoleId == user.RoleId)?.RoleName;
                if (role != null)
                {
                    // Set the claims for the user
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.UserEmail),
                        new Claim(ClaimTypes.Role, role)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "Login");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    // Sign in the user with the claims
                    HttpContext.SignInAsync(claimsPrincipal).Wait();
                    if (role == "SuperAdmin")
                    {
                        HttpContext.Session.SetString("UserName", user.UserName);
                        HttpContext.Session.SetString("UserEmail", user.UserEmail);
                        HttpContext.Session.SetString("Key", EncrypterDecrypter.EncryptPassword(loginDetails.UserPassword));
                        return RedirectToAction("Dashboard", "SuperAdmin");
                    }
                    else if (role == "SeniorAdmin")
                    {

                        return RedirectToAction("Dashboard", "SeniorAdmin");
                    }
                    else if (role == "UserAdmin")
                    {

                        return RedirectToAction("Dashboard", "SeniorAdmin");
                    }
                    else if (role == "HeadTeacher")
                    {

                        return RedirectToAction("Dashboard", "HeadTeacher");
                    }
                    else if (role == "Teacher")
                    {

                        return RedirectToAction("Dashboard", "Teacher");
                    }
                    else if (role == "Student")
                    {

                        return RedirectToAction("Dashboard", "Student");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Something went Wrong!");
                        return View("Login", loginDetails);
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View("Login", loginDetails);
                }
            }

            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("Login", loginDetails);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An error occurred while processing your request.");
            return Content("Error Occured" + ex.Message);
        }
    }



    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }


    [AllowAnonymous]
    [HttpPost]
    public IActionResult RegisterValidation()
    {
        try
        {
            var user = new UserDetail
            {
                UserId = Guid.NewGuid().ToString(),
                UserName = Request.Form["UserName"],
                UserEmail = Request.Form["UserEmail"],
                UserPhone = Request.Form["UserPhone"],
                UserAddress = Request.Form["UserAddress"],
                UserGender = Request.Form["UserGender"],
                UserDOB = Request.Form["UserDOB"],
                UserPassword = Request.Form["UserPassword"],
                UserSchoolName = Request.Form["UserSchoolName"],
                RoleId = Convert.ToInt32(Request.Form["RoleId"])
            };
            _context.UserDetail.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An error occurred while processing your request.");
            return Content("Error Occured" + ex.Message);
        }
    }
}
