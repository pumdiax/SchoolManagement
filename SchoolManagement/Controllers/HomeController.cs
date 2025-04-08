using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Data;
using SchoolManagement.Models;

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
    public IActionResult UserVerification(LoginDetails loginDetails)
    {
        if (ModelState.IsValid)
        {
            var user = _context.UserDetail.FirstOrDefault(u => u.UserEmail == loginDetails.UserEmail && u.UserPassword == loginDetails.UserPassword);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.UserId);
                HttpContext.Session.SetString("UserName", user.UserName);
                return RedirectToAction("Index", "Dashboard");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
        }
        return View(loginDetails);
    }


}
