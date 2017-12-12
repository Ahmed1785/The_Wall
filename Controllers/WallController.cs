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
    public class WallController : Controller
    {

        private MainContext _context;

        public WallController(MainContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("createmessage")]
        public IActionResult Add(Message messages)
        {
            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            if(ModelState.IsValid)
            {
                Console.WriteLine("step1");
                Message NewMessage = new Message
                {
                    message = messages.message,
                    userid = (int)loggedperson
                };
                Console.WriteLine("step2");
                _context.Add(NewMessage);
                _context.SaveChanges();
                System.Console.WriteLine("NEW Message", messages.message);
                ViewBag.Success = "You have been added to the database! Please log in now!";

                return RedirectToAction("LandingPage", "Home");
            }
            
            return RedirectToAction("LandingPage", "Home");
        }

        [HttpPost]
        [Route("createcomment/{id}")]
        public IActionResult AddComment(Comment comments, int id)
        {
            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            if(ModelState.IsValid)
            {
                Console.WriteLine("step1");
                Comment NewComment = new Comment
                {
                    comment = comments.comment,
                    userid = (int)loggedperson,
                    messageid = id

                };
                Console.WriteLine("step2");
                _context.Add(NewComment);
                _context.SaveChanges();
                System.Console.WriteLine("NEW Message", comments.comment);
                ViewBag.Success = "You have been added to the database! Please log in now!";

                return RedirectToAction("LandingPage", "Home");
            }
            
            return RedirectToAction("LandingPage", "Home");
        }
    }
}
