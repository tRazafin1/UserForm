using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UserForm.Models;
using UserForm.Infrastructure;

namespace UserForm.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private XMLGenerator _XML = new XMLGenerator();

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult List(Users obj)

        {
            List<Users> USERS = _XML.GetChildren();
            return View(USERS);
        }
        public ActionResult Delete(string id)
        {
            _XML.RemoveChild(id);
            List<Users> USERS = _XML.GetChildren();
            return View("List", USERS);
        }

        [HttpPost]  
        public ActionResult Index(Users obj)  
  
        {  
            if (ModelState.IsValid) {
                _XML.AddChild(obj);
            }
            List<Users> USERS = _XML.GetChildren();
            return View("List", USERS);
        }  
    }
}
