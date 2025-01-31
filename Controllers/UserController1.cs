using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sql_Migration.Data;
using Sql_Migration.Models;

namespace Sql_Migration.Controllers
{
    public class UserController1(SqlDbContext dbContext) : Controller
    {
        private readonly SqlDbContext _dbcontext = dbContext;

        public string errorMessage = "";
        public string successMessage = "";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            try {
                var finduser = await _dbcontext.User2.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (finduser!=null)
            {
                ViewBag.errorMessage = "User alredy exists";
            }
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                if (ModelState.IsValid)
                {
                    await _dbcontext.User2.AddAsync(user);
                    await _dbcontext.SaveChangesAsync();
                    ViewBag.successMessage = "User created suceefully";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;

                Console.WriteLine(ex.Message);
                return View();
            }
            return View(user);
        }


    }
}
