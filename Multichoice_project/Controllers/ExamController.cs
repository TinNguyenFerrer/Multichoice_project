using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Multichoice_project.Core;
using Multichoice_project.Models;
using Multichoice_project.Persistence;
using Multichoice_project.Persistence.CachePattern;
using System.Text.Json.Nodes;

namespace Multichoice_project.Controllers
{
    public class ExamController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;
        //private int idUser;
        private readonly IMemoryCache _memoryCache;
        public ExamController(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _unitOfwork = unitOfWork;
        }

        public IActionResult Index(int id)
        {
            int IdUser;
            if (HttpContext.Session.GetInt32("IdUser") == null)
            {
                Console.WriteLine("lỗi đăng nhập" + HttpContext.Session.GetInt32("IdUser"));
                return RedirectToAction("Login", "Home");
            }
            else
            {
                IdUser = (int)HttpContext.Session.GetInt32("IdUser");
                TempData["IdUser"] = (int)HttpContext.Session.GetInt32("IdUser");
            }
            var datatest = _unitOfwork.TestRepository.GetByID(id);
            TempData["IdTest"] = id;

            
            var dataquestion = (from ques in _unitOfwork.QuestionRepository.GetAll()
                                where ques.TestId == datatest.Id
                                select ques).ToList();

            //========--------Dao cua hoi--------============
            List<Question> Listdataquestions = new List<Question>();
            var k = dataquestion.Count;
            for (int i = 0;i< k; i++)
            {
                Random rnd = new Random();
                int t = rnd.Next(dataquestion.Count);
                Listdataquestions.Add(dataquestion[t]);
                dataquestion.RemoveAt(t);
            }
            dataquestion.Clear();
            dataquestion.AddRange(Listdataquestions);


            if (dataquestion.Any() && datatest != null)
            {
                //==============-----Caching Memory----=====================
                var TimeToDoTest = datatest.Time;
                bool ExamIsExisting = false;
                IList<ExamCache> ExamCacheList = new List<ExamCache>();
                if (_memoryCache.TryGetValue("ExamCache", out ExamCacheList))
                {
                    ExamCacheList = _memoryCache.Get("ExamCache") as List<ExamCache>;
                    foreach (var Item in ExamCacheList.ToList())
                    {
                        if (DateTime.Compare(Item.TimeEndTest, DateTime.Now) < 0)
                        {
                            ExamCacheList.Remove(Item);
                        }
                        if ((Item.IdTest == datatest.Id) && (Item.IdUser == IdUser))
                        {
                            TimeSpan TimeDidTest = DateTime.Now - Item.TimeStartTest;
                            TimeToDoTest = TimeToDoTest - Math.Abs((int)TimeDidTest.TotalMinutes);
                            ExamIsExisting = true;
                        }
                    }
                }
                else
                {
                    ExamCacheList = new List<ExamCache>();
                }
                if (!ExamIsExisting)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(100));
                    ExamCache t = new ExamCache(DateTime.Now, DateTime.Now.AddMinutes(datatest.Time), datatest.Id, IdUser);
                    ExamCacheList.Add(t);
                    _memoryCache.Set("ExamCache", ExamCacheList, cacheEntryOptions);
                }
                datatest.Time = TimeToDoTest;
                //==============-----------------------=====================
                ViewBag.Model = datatest;
                ViewBag.question = dataquestion;
                ViewBag.NumberQuestion = dataquestion.Count();
                ViewBag.UserNameDisplay = HttpContext.Session.GetString("UserName") != null ? "Hi!.." + HttpContext.Session.GetString("UserName") : "Đăng nhập!";
                //TempData["t"]
                return View();
            }

            return RedirectToAction("Index", "Home");

        }
        public IActionResult TakeExam()
        {
            ViewBag.UserNameDisplay = HttpContext.Session.GetString("UserName") != null ? "Hi!.." + HttpContext.Session.GetString("UserName") : "Đăng nhập!";
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
                if (_unitOfwork.AnswerRepository.IsRightAnswer(Int32.Parse(ans.ToString())))
                {
                    point++;
                }
            }
            Result result = new Result();
            int idTest = Int32.Parse(TempData["IdTest"].ToString());
            result.TestId = idTest;
            
            result.UserId = Int32.Parse(TempData["IdUser"].ToString());

            result.DateTime = DateTime.Now;
            result.NumberOfCorrectAnswers = point;
            result.NumberQuestionOfTest = _unitOfwork.QuestionRepository.GetQuestionsAnswersByIdTest(idTest).ToList().Count;
            _unitOfwork.ResultRepository.Insert(result);
            _unitOfwork.SaveChange();

            //==============-----Caching Memory----=====================
            IList<ExamCache> ExamCacheList = new List<ExamCache>();
            if (_memoryCache.TryGetValue("ExamCache", out ExamCacheList))
            {
                var TestId = Int32.Parse(t["IdTest"].ToString());
                var UserId = (int)HttpContext.Session.GetInt32("IdUser");
                    
                ExamCacheList = _memoryCache.Get("ExamCache") as List<ExamCache>;
                foreach (var Item in ExamCacheList.ToList())
                {
                    if (DateTime.Compare(Item.TimeEndTest, DateTime.Now) < 0)
                    {
                        ExamCacheList.Remove(Item);
                    }
                    if ((Item.IdTest == TestId) && (Item.IdUser == UserId))
                    {
                        ExamCacheList.Remove(Item);
                    }
                }
            }
            //==============-----------------------=====================

            TempData.Keep("IdTest");
            TempData.Keep("IdUser");
            return point;
        }
    }
}
