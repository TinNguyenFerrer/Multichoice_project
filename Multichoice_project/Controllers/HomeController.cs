﻿using Microsoft.AspNetCore.Mvc;
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
        private UnitOfWork _unitOfWork;
        public HomeController(Multichoise_DBContext dbcontext)
        {
            _unitOfWork =new UnitOfWork(dbcontext);
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
            _unitOfWork.UserRepository.Insert(user);
            _unitOfWork.SaveChange();
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
                var data = (from userlogin in _unitOfWork.UserRepository.GetAll()
                            where (userlogin.UserName == user.UserName && userlogin.PassWord == GetMD5(user.PassWord))
                            select userlogin).ToList();
                if(data.Count > 0)
                {
                    HttpContext.Session.SetString("username", user.UserName);
                    HttpContext.Session.SetString("password", user.PassWord);
                    Console.WriteLine("Thêm sestion thanh công");
                    return RedirectToAction(nameof(Index));
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