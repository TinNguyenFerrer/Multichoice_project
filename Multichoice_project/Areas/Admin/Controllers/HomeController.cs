using Microsoft.AspNetCore.Mvc;
using Multichoice_project.Core;
using Multichoice_project.Models;
using Multichoice_project.Persistence;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Web;

namespace Multichoice_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //bỏ dấu tiếng việt
        public string RemoveVietnameseTone(string text)
        {
            string result = text.ToLower();
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            return result;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<JsonResult> DeleteListIdQuestion()
        {
            Task<string> body;
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                body = stream.ReadToEndAsync();
            }

            JsonObject t = JsonNode.Parse(await body).AsObject();
            Console.WriteLine("-------------------------list question delete---------------");
            Console.WriteLine(t);
            foreach (var idQ in t["listidques"].AsArray())
            {
                Console.WriteLine(Int32.Parse(idQ.ToString()));
                _unitOfWork.QuestionRepository.Delete(Int32.Parse(idQ.ToString()));
            }
            _unitOfWork.SaveChange();
            return Json(new { code = 200 });

        }
        public async Task<JsonResult> DeleteListIdAnswer()
        {
            Task<string> body;
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                body = stream.ReadToEndAsync();
            }

            JsonObject t = JsonNode.Parse(await body).AsObject();
            Console.WriteLine("-------------------------list answer delete---------------");
            Console.WriteLine(t);
            foreach (var idQ in t["listidans"].AsArray())
            {
                Console.WriteLine(Int32.Parse(idQ.ToString()));
                _unitOfWork.AnswerRepository.Delete(Int32.Parse(idQ.ToString()));
            }
            _unitOfWork.SaveChange();
            return Json(new { code = 200 });

        }
        
        public IActionResult Point()
        {
            if (HttpContext.Session.GetInt32("IdUserAd") == null)
            {
                Console.WriteLine("lỗi đăng nhập" + HttpContext.Session.GetInt32("IdUser"));
                return RedirectPermanent("/Admin/Home/Login");
            }
            ViewBag.UserLogin = _unitOfWork.UserRepository.GetByID((int)HttpContext.Session.GetInt32("IdUserAd")).UserName;
            ViewBag.Result = (from result in _unitOfWork.ResultRepository.GetAll()
                              join test in _unitOfWork.TestRepository.GetAll() on result.TestId equals test.Id
                              select test).Distinct().ToList();
            return View();
        }
        public IActionResult PointDetail(int Id)
        {
            if (HttpContext.Session.GetInt32("IdUserAd") == null)
            {
                Console.WriteLine("lỗi đăng nhập" + HttpContext.Session.GetInt32("IdUser"));
                return RedirectPermanent("/Admin/Home/Login");
            }
            ViewBag.UserLogin = _unitOfWork.UserRepository.GetByID((int)HttpContext.Session.GetInt32("IdUserAd")).UserName;
            ViewBag.Result = (from result in _unitOfWork.ResultRepository.GetAll()
                              join test in _unitOfWork.TestRepository.GetAll() on result.TestId equals test.Id
                              join user in _unitOfWork.UserRepository.GetAll() on result.UserId equals user.Id
                              where result.TestId == Id
                              select result).Distinct().ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserNameAd");
            HttpContext.Session.Remove("IdUserAd");
            return RedirectPermanent("/Admin/Home/Login");
        }
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
                            where (userlogin.UserName == user.UserName && userlogin.PassWord == Multichoice_project.Controllers.HomeController.GetMD5(user.PassWord))
                            select userlogin).ToList();
                if((data.Count > 0)&& String.Compare(data.First().RoleName,"Admin",true)==0)
                {
                    HttpContext.Session.SetString("UserNameAd", data.First().UserName);
                    HttpContext.Session.SetInt32("IdUserAd", data.First().Id);
                    Console.WriteLine("Thêm sestion thanh công:"+ HttpContext.Session.GetInt32("IdUser"));
                    //return RedirectToAction(nameof(Index));
                    return RedirectPermanent("/Admin/Test/AllTest");
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
