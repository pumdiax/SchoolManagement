using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Encrypter;
using SchoolManagement.Models;


namespace SchoolManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SuperAdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Email")!=null && HttpContext.Session.GetString("Key") !=null)
            {   string userName = HttpContext.Session.GetString("UserName");
                string userEmail = HttpContext.Session.GetString("UserEmail");
                string key = HttpContext.Session.GetString("Key");
                // Decrypt the password
                string decryptedPassword = EncrypterDecrypter.DecryptPassword(key);
                // Use the decrypted password as needed
                var user = _context.UserDetail.FirstOrDefault(u => u.UserPassword ==decryptedPassword);
                if (user == null)
                {
                    ViewData["UserName"] = userName;
                    return View();
                }
                else { return RedirectToAction("Login", "Home"); }
            }
          else
            {  return RedirectToAction("Login", "Home");    }

        }
    }
}
