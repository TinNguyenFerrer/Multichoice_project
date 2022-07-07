using Microsoft.AspNetCore.Mvc;
using Multichoice_project.Models;
using Multichoice_project.Persistence;

namespace Multichoice_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubjectController : Controller
    {
        private UnitOfWork _unitOfWork;
        public SubjectController(Multichoise_DBContext dbcontext)
        {
            _unitOfWork = new UnitOfWork(dbcontext);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllSubject()
        {
            if (HttpContext.Session.GetInt32("IdUserAd") == null)
            {
                Console.WriteLine("lỗi đăng nhập" + HttpContext.Session.GetInt32("IdUser"));
                return RedirectPermanent("/Admin/Home/Login");
            }
            ViewBag.UserLogin = _unitOfWork.UserRepository.GetByID((int)HttpContext.Session.GetInt32("IdUserAd")).UserName;
            ViewBag.Subject = (from subj in _unitOfWork.SubjectRepository.GetAll()
                               join edu in _unitOfWork.EducationalFieldRepository.GetAll() on subj.EducationalFieldId equals edu.Id
                               select subj).Distinct().ToList();
            return View();
            ;
        }

        public IActionResult DeleteSubject(int id)
        {
            _unitOfWork.SubjectRepository.Delete(id);
            _unitOfWork.SaveChange();
            return RedirectToAction(nameof(AllSubject));
        }
        public IActionResult CreateSubject()
        {
            if (HttpContext.Session.GetInt32("IdUserAd") == null)
            {
                Console.WriteLine("lỗi đăng nhập" + HttpContext.Session.GetInt32("IdUser"));
                return RedirectPermanent("/Admin/Home/Login");
            }
            ViewBag.UserLogin = _unitOfWork.UserRepository.GetByID((int)HttpContext.Session.GetInt32("IdUserAd")).UserName;
            ViewBag.Educational_field = (from edu in _unitOfWork.EducationalFieldRepository.GetAll()
                                         select edu).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateSubject(Subject subject)
        {
            _unitOfWork.SubjectRepository.Insert(subject);
            _unitOfWork.SaveChange();
            return RedirectToAction(nameof(CreateSubject));
        }
        public IActionResult EditSubject(int id)
        {
            if (HttpContext.Session.GetInt32("IdUserAd") == null)
            {
                Console.WriteLine("lỗi đăng nhập" + HttpContext.Session.GetInt32("IdUser"));
                return RedirectPermanent("/Admin/Home/Login");
            }
            ViewBag.UserLogin = _unitOfWork.UserRepository.GetByID((int)HttpContext.Session.GetInt32("IdUserAd")).UserName;
            ViewBag.Subject = (from subj in _unitOfWork.SubjectRepository.GetAll()
                               join edu in _unitOfWork.EducationalFieldRepository.GetAll() on subj.EducationalFieldId equals edu.Id
                               where subj.Id == id
                               select subj).Distinct().ToList().First();
            ViewBag.Educational_field = (from edu in _unitOfWork.EducationalFieldRepository.GetAll()
                                         select edu).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult EditSubject(Subject subject)
        {
            _unitOfWork.SubjectRepository.Update(subject);
            _unitOfWork.SaveChange();
            return RedirectToAction(nameof(AllSubject));
        }
    }
}
