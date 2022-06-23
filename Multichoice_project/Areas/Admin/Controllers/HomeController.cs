﻿using Microsoft.AspNetCore.Mvc;
using Multichoice_project.Models;
using Multichoice_project.Repositories;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Web;

namespace Multichoice_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        private UnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;
        public HomeController(Multichoise_DBContext dbcontext)
        {
            _unitOfWork = new UnitOfWork(dbcontext);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateTest()
        {
            ViewData["Subject"] = _unitOfWork.SubjectRepository.GetAll().ToList();
            return View();
        }
        [HttpPost]
        
        public async void SaveTest()
        {

            string body = "";
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                body = await stream.ReadToEndAsync();
            }


            //var t = JsonSerializer.Deserialize<dynamic>(body);
            //var t = JsonSerializer.Deserialize<object>(body) as JsonElement?;
            JsonObject t = JsonNode.Parse(body).AsObject();

            Console.WriteLine(t);
            
                Test test = new Test();

                test.Question = new List<Question>();

                test.Name = t["name"].ToString();
                test.Time = Int32.Parse(t["time"].ToString());
                test.Description = t["description"].ToString();
                test.SubjectId = Int32.Parse(t["subjectid"].ToString());
                test.Hits = 0;
                test.Link = RemoveVietnameseTone(t["name"].ToString() +"-"+ String.Format("{0:d/M/hhyymmss}", DateTime.Now).Replace("/", "")).Replace(" ", "");
                test.CreatedTime = DateTime.Now;
                test.UserId = 1;
                foreach (var Lques in t["listQues"].AsArray()) {
                    Question ques = new Question();
                    ques.Mark = 1;
                    ques.QuestionTypeID = 1;
                    ques.Content = Lques["question"].ToString();
                    ques.Answers = new List<Answer>();
                    var i = 0;
                    foreach(var answer in Lques["answer"].AsArray()){
                        Answer ans = new Answer();
                        ans.Content = answer.ToString();
                        if (i == Int32.Parse(Lques["rightAnswer"].ToString())){
                            ans.IsCorrectAnswer = true;
                        }
                        ques.Answers.Add(ans);
                        i++;
                    }
                    test.Question.Add(ques);
                }
                _unitOfWork.TestRepository.Insert(test);
                _unitOfWork.SaveChange();
                
            //Console.WriteLine(t?.GetProperty("listQues"));
            
        }
        public IActionResult AllTest()
        {
            ViewBag.Test = _unitOfWork.TestRepository.GetAll().ToList();

            return View();
        }
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

    }
}