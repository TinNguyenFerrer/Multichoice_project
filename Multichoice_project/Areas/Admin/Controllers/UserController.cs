using Microsoft.AspNetCore.Mvc;
using Multichoice_project.Models;
using Multichoice_project.Persistence;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Web;

namespace Multichoice_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private UnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public UserController(Multichoise_DBContext dbcontext)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _unitOfWork = new UnitOfWork(dbcontext);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SwitchRoleName(int id)
        {
            Console.WriteLine("--------------------Switch_Role------------");
            User user = _unitOfWork.UserRepository.GetByID(id);
            if (String.Compare(user.RoleName, "Admin", true) == 0)
            {
                user.RoleName = "User";
            }
            else
            {
                user.RoleName = "Admin";
            }
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChange();

            return Redirect("/Admin/Home/AllUser");
        }
        public IActionResult EditUser(User user)
        {
            if (user.RoleName == null)
            {
                user.RoleName = "User";
            }
            user.PassWord = Multichoice_project.Controllers.HomeController.GetMD5(user.PassWord);
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.SaveChange();
            return Redirect("/Admin/Home/AllUser");
        }
    }
}
