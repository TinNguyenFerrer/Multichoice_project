using Microsoft.AspNetCore.Mvc;
using Multichoice_project.Models;
using Multichoice_project.Persistence;
using System.Linq;

namespace Multichoice_project.Controllers
{
    public class TestController : Controller
    {
        UnitOfWork _UnitOfwork;
        
        public TestController(Multichoise_DBContext dbcontext)
        {
            _UnitOfwork = new UnitOfWork(dbcontext);
            
        }
        public IActionResult Index()
        {
            var data = (from test in _UnitOfwork.TestRepository.GetAll()
                        select test);
            ViewBag.Model = data.ToList();
            if (data.Any())
            {
                data.ToList();
                ViewBag.UserNameDisplay = HttpContext.Session.GetString("UserName") != null ? "Hi!.." + HttpContext.Session.GetString("UserName") : "Đăng nhập!";
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
                ViewBag.UserNameDisplay = HttpContext.Session.GetString("UserName") != null ? "Hi!.." + HttpContext.Session.GetString("UserName") : "Đăng nhập!";
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
        public IActionResult Subject(string SubjectName)
        {
            var IdSub = (from Sub in _UnitOfwork.SubjectRepository.GetAll()
                         where  Equals(Sub.Name.Trim(),SubjectName.Trim())
                         select Sub.Id);
            var id = IdSub.FirstOrDefault();
            var ListTest = _UnitOfwork.TestRepository.GetListByIdSubjectId(id).ToList();
            ViewBag.UserNameDisplay = HttpContext.Session.GetString("UserName") != null ? "Hi!.." + HttpContext.Session.GetString("UserName") : "Đăng nhập!";
            return View(ListTest);
        }
        public IActionResult EducationalField(string EduFieldName)
        {
            var IdEdu = (from Edu in _UnitOfwork.EducationalFieldRepository.GetAll()
                         where Equals(Edu.Name.Trim(), EduFieldName.Trim())
                         select Edu.Id);
            var IdEducationalField = IdEdu.FirstOrDefault();

            var ListSubject = _UnitOfwork.SubjectRepository.GetSubjectByEducationFieldId(IdEducationalField).ToList();
            List<Test> ListTest = new List<Test>();
            foreach (var Item in ListSubject)
            {
                ListTest.AddRange(_UnitOfwork.TestRepository.GetListByIdSubjectId(Item.Id).ToList());
            }
            ViewBag.IdEduc = IdEducationalField;
            ViewBag.UserNameDisplay = HttpContext.Session.GetString("UserName") != null ? "Hi!.." + HttpContext.Session.GetString("UserName") : "Đăng nhập!";
            return View(ListTest);
        }

    }
}
