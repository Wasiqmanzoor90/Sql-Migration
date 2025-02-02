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





        //Home Page
        public async Task<IActionResult> Index()
        {
            var blogs = await _dbcontext.Blogs.OrderByDescending(b => b.DateCreated).ToListAsync();
            return View(blogs);
        }

        public IActionResult Create()
        {
            return View();
        }





        //Register User
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                var finduser = await _dbcontext.User2.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (finduser != null)
                {
                    ViewBag.errorMessage = "User alredy exists";
                    return View();
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





        //Login 
        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Signin(Login login)
        {
            try 
            {

                var finduser = await _dbcontext.User2.FirstOrDefaultAsync(u => u.Email == login.Email);
                if(finduser == null)
                {
                    ViewBag.errorMessage = "User doesnt exist";
                }

                bool ispasscorrect = BCrypt.Net.BCrypt.Verify(login.Password, finduser.Password);
                if (!ispasscorrect)
                {
                    ViewBag.errorMessage = "Invalid email or password";
                    return View();
                }

                ViewBag.successMessage = "Login successful!";
                return View(); // Simply stays on the same page with success message

             
            } 
            
            catch (Exception ex) 
            {
                ViewBag.errorMessage = ex.Message;
                Console.WriteLine(ex.Message);
                return View();
            }
        }

    
        //Create Blog
        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.DateCreated = DateTime.Now;
                blog.DateUpdated = DateTime.Now;
                _dbcontext.Blogs.Add(blog);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(blog);
        }




        //Details
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _dbcontext.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            return View(blog);
        }




    }
}
