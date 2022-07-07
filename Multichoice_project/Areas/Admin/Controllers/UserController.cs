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

            return Redirect("/Admin/User/AllUser");
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
            return Redirect("/Admin/User/AllUser");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult AllUser()
        {
            if (HttpContext.Session.GetInt32("IdUserAd") == null)
            {
                Console.WriteLine("lỗi đăng nhập" + HttpContext.Session.GetInt32("IdUser"));
                return RedirectPermanent("/Admin/Home/Login");
            }
            ViewBag.UserLogin = _unitOfWork.UserRepository.GetByID((int)HttpContext.Session.GetInt32("IdUserAd")).UserName;
            ViewBag.User = _unitOfWork.UserRepository.GetAll();
            return View();
        }
        public IActionResult CreateUser()
        {
            if (HttpContext.Session.GetInt32("IdUserAd") == null)
            {
                Console.WriteLine("lỗi đăng nhập" + HttpContext.Session.GetInt32("IdUser"));
                return RedirectPermanent("/Admin/Home/Login");
            }
            ViewBag.UserLogin = _unitOfWork.UserRepository.GetByID((int)HttpContext.Session.GetInt32("IdUserAd")).UserName;
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            Console.WriteLine("-----CreateUser-----");
            if (user != null)
            {
                if (user.RoleName == null)
                {
                    user.RoleName = "User";
                }
                user.PassWord = Multichoice_project.Controllers.HomeController.GetMD5(user.PassWord);
                _unitOfWork.UserRepository.Insert(user);
                _unitOfWork.SaveChange();
                
            }
                return RedirectPermanent("/Admin/User/AllUser");

        }
        public IActionResult DeleteUser(int id)
        {
            Console.WriteLine("------DeleteUser----------");
            _unitOfWork.UserRepository.Delete(id);
            _unitOfWork.SaveChange();
            return RedirectPermanent("/Admin/User/AllUser");
        }
        public IActionResult EditUser(int Id)
        {
            if (HttpContext.Session.GetInt32("IdUserAd") == null)
            {
                Console.WriteLine("lỗi đăng nhập" + HttpContext.Session.GetInt32("IdUser"));
                return RedirectPermanent("/Admin/Home/Login");
            }
            ViewBag.UserLogin = _unitOfWork.UserRepository.GetByID((int)HttpContext.Session.GetInt32("IdUserAd")).UserName;
            ViewBag.User = (from user in _unitOfWork.UserRepository.GetAll()
                            where user.Id == Id
                            select user).ToList().First();
            return View();
        }
    }
}
