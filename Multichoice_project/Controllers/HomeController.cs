using Microsoft.AspNetCore.Mvc;
using Multichoice_project.Models;
using System.Diagnostics;
using Multichoice_project.Repositories;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System;


namespace Multichoice_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UnitOfWork _UnitOfWork;
        public HomeController(Multichoise_DBContext dbcontext)
        {
            _UnitOfWork =new UnitOfWork(dbcontext);
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }


        public IActionResult Index()
        {
            var data = (from test in _UnitOfWork.TestRepository.GetAll()
                        select test);
            ViewBag.Model = data.ToList();
            if (data.Any())
            {
                data.ToList();
                return View(data);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Register( User user)
        {
            user.RoleName = "Admin";
            user.PassWord = GetMD5(user.PassWord);
            _UnitOfWork.UserRepository.Insert(user);
            _UnitOfWork.SaveChange();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (user.UserName == null || user.PassWord == null)
            {
                Console.WriteLine("mull");
                ViewData["ErrorLogin"] = "erorr";
                return View();
            }
            else
            {
                var data = (from userlogin in _UnitOfWork.UserRepository.GetAll()
                            where (userlogin.UserName == user.UserName && userlogin.PassWord == GetMD5(user.PassWord))
                            select userlogin).ToList();
                if(data.Count > 0)
                {
                    HttpContext.Session.SetString("UserName", user.UserName);
                    HttpContext.Session.SetInt32("IdUser", data.First().Id);
                    Console.WriteLine("Thêm sestion thanh công:"+ HttpContext.Session.GetInt32("IdUser"));
                    //return RedirectToAction(nameof(Index));
                    return RedirectPermanent("/Test/");
                }
                else
                {
                    ViewData["ErrorLogin"] = "erorr";
                    return View();
                }

            }
        }

    }
}