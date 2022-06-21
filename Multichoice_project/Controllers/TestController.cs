using Microsoft.AspNetCore.Mvc;
using Multichoice_project.f;
using Multichoice_project.Models;
using Multichoice_project.Repositories;
using System.Linq;

namespace Multichoice_project.Controllers
{
    public class TestController : Controller
    {
        UnitOfWork _UnitOfwork;
        SAnswerRepositories answerrepositories;
        public TestController(Multichoise_DBContext dbcontext)
        {
            _UnitOfwork = new UnitOfWork(dbcontext);
            answerrepositories = new SAnswerRepositories(dbcontext);
        }
        public IActionResult Index()
        {
            var data = (from test in _UnitOfwork.TestRepository.GetAll()
                        select test);
            ViewBag.Model = data.ToList();
            if (data.Any())
            {
                data.ToList();

                return View(data);
            }

            return RedirectPermanent("/Home/Index");
        }
        public IActionResult TestName(string id)
        {
            var datatest = (from test in _UnitOfwork.TestRepository.GetAll()
                            where test.Link == id
                            select test).ToList().First();
            var dataquestion = (from ques in _UnitOfwork.QuestionRepository.GetAll()
                                where ques.Test.Id == datatest.Id
                                select ques).ToList();
            if (dataquestion.Any() && datatest != null)
            {
                ViewBag.Model = datatest;
                ViewBag.question = dataquestion;
                return View();
            }

            return RedirectPermanent("/Home/Index");
        }
        public JsonResult GetAnswer(int idquestion)
        {
            var answers = (from answer in _UnitOfwork.AnswerRepository.GetAll() 
                           join question in _UnitOfwork.QuestionRepository.GetAll() 
                           on answer.Question.Id equals question.Id 
                           where answer.Question.Id == idquestion
                           select new
                           {
                               answer= answer.Content,
                               idanswer = answer.Id,
                               idquestion = answer.Question.Id,
                               
                           } ).ToList();
            return Json(new { code = 200, answerslist = answers });
        }
        public JsonResult GetCorrectAnswer(int idquestion)
        {
            var answers = (from answer in _UnitOfwork.AnswerRepository.GetAll()
                           join question in _UnitOfwork.QuestionRepository.GetAll()
                           on answer.Question.Id equals question.Id
                           where answer.Question.Id == idquestion && answer.IsCorrectAnswer
                           select new
                           {
                               idanswer = answer.Id,
                           }).ToList();
            return Json(new { code = 200, answerslist = answers });
        }


    }
}
