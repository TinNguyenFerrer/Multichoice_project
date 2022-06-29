using Microsoft.AspNetCore.Mvc;
using Multichoice_project.Models;
using Multichoice_project.Persistence;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Web;

namespace Multichoice_project.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult SwitchRole()
        {
            //Console.WriteLine("_-------------SwitchRole--------------------");
            //_unitOfWork.UserRepository.SwitchRole(id);
            //_unitOfWork.SaveChange();
            //return RedirectPermanent("/Admin/Home/AllUsers");


            return Json(new { code = 200 });
        }
    }
}
