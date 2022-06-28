using Microsoft.AspNetCore.Mvc;
using Multichoice_project.Models;
using Multichoice_project.Persistence;
using System.Text.Json.Nodes;

namespace Multichoice_project.Controllers
{
    public class ExamController : Controller
    {
        UnitOfWork _UnitOfwork;
        private int idUser;
        public ExamController(Multichoise_DBContext dbcontext)
        {
            _UnitOfwork = new UnitOfWork(dbcontext);
        }

        public IActionResult Index(int id)
        {
            //if (HttpContext.Session.GetInt32("IdUser") == null)
            //{
            //    Console.WriteLine("lỗi đăng nhập"+ HttpContext.Session.GetInt32("IdUser"));
            //    //return RedirectPermanent("/Home/Login");
            //}
            //else { 
            //    this.idUser = (int)HttpContext.Session.GetInt32("IdUser");
            //}
            
            var datatest = _UnitOfwork.TestRepository.GetByID(id);
            TempData["IdTest"] = id;

            var dataquestion = (from ques in _UnitOfwork.QuestionRepository.GetAll()
                                where ques.TestId == datatest.Id
                                select ques).ToList();
            if (dataquestion.Any() && datatest != null)
            {
                ViewBag.Model = datatest;
                ViewBag.question = dataquestion;
                return View();
            }

            return RedirectPermanent("/Home/Index");

        }
        public IActionResult TakeExam()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<int> ResultExam()
        {
            string body = "";
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                body = await stream.ReadToEndAsync();
            }
            Console.WriteLine(body);
            Console.WriteLine(body.GetType());
            JsonObject t = JsonNode.Parse(body).AsObject();
            var point = 0;
            foreach (var ans in t["listAnswer"].AsArray())
            {
                if (_UnitOfwork.AnswerRepository.IsRightAnswer(Int32.Parse(ans.ToString())))
                {
                    point++;
                }
            }
            Result result = new Result();
            int idTest = Int32.Parse(TempData["IdTest"].ToString());
            result.TestId = idTest;
            //sửa lại user
            Console.WriteLine("-------------id user---------:"+ this.idUser);
            result.UserId = this.idUser;

            result.DateTime = DateTime.Now;
            result.NumberOfCorrectAnswers = point;
            result.NumberQuestionOfTest = _UnitOfwork.QuestionRepository.GetQuestionsAnswersByIdTest(idTest).ToList().Count;
            _UnitOfwork.ResultRepository.Insert(result);
            _UnitOfwork.SaveChange();
            TempData.Keep("IdTest");
            return point;
        }
    }
}
