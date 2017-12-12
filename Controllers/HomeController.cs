using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Wall.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
// using Wall.Models;

namespace Wall.Controllers
{
    public class HomeController : Controller
    {

        private MainContext _context;

        public HomeController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // wrap the session validation around all of the methods in all the pages!!
            // for example if(user not in session{
            // return redirectToAvtion("Index")}else{
            // ALL THE OTHER SHIT ON THE PAGEEEE
            //}
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // System.Console.WriteLine(registeredcheck);
                // System.Console.WriteLine("EMAILLL" + user.Email);
                // System.Console.WriteLine("THISSSSS****"+returnedid.id);

                User registeredcheck = _context.users.SingleOrDefault(str => str.email == user.email);


                // System.Console.WriteLine("THE EMAILLL", registeredcheck.Email);
                // string email = registeredcheck.Email;
                if (registeredcheck == null)
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.password = Hasher.HashPassword(user, user.password);
                    User NewPerson = new User

                    {
                        first_name = user.first_name,
                        last_name = user.last_name,
                        email = user.email,
                        password = user.password,

                    };

                    _context.Add(NewPerson);
                    _context.SaveChanges();
                    System.Console.WriteLine("NEW PERSON", NewPerson.first_name);
                    ViewBag.Success = "You have been added to the database! Please log in now!";
                    return View("Index");

                }
                else
                {
                    System.Console.WriteLine("ALREADY IN THE DATABASE");
                    return View("Index");
                }
            }
            return View("Index");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginUser user)
        {
            User userfound = new User
            {
                first_name = "",
                last_name = "",
                email = user.LogEmail,
                password = user.LogPassword,
            };

            User loggeduser = _context.users.SingleOrDefault(str => str.email == userfound.email);
            System.Console.WriteLine("LOGGEDUSER" + loggeduser);
            if (loggeduser == null)
            {
                ViewBag.loginerror = "Login failed, email and password did not match the information in the database. If you haven't registered please register first!";
                return View("Index");
            }
            else{
                PasswordHasher<User> Hasher = new PasswordHasher<User>();

                if (0 != Hasher.VerifyHashedPassword(loggeduser, loggeduser.password, userfound.password))
                {

                    HttpContext.Session.SetInt32("loggedperson", (int)loggeduser.userid);

                    return RedirectToAction("LandingPage");
                }
                else
                {

                    ViewBag.loginerror = "Login failed, email and password did not match the information in the database. If you haven't registered please register first!";
                    return View("Index");
                }
            }
        }

        [HttpGet]
        [Route("home")]

        public IActionResult LandingPage()
        {

            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            if (loggedperson == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                System.Console.WriteLine("HEYYY" + loggedperson);
                User findtheperson = _context.users.SingleOrDefault(str => str.userid == loggedperson);

                System.Console.WriteLine("FOUND PESON " + findtheperson);

                ViewBag.User = findtheperson;
                ViewBag.messages = _context.messages.Include(m => m.messagecomments);
                return View("About");
            }

        }
        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {


            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }


    }
}
