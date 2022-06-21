using System.Linq;
using Microsoft.EntityFrameworkCore;
using Multichoice_project.Models;
using Multichoice_project.Models;

namespace Multichoice_project.f
{
    public class SAnswerRepositories : SIAnswerRepositories
    {
        public readonly Multichoise_DBContext _DBContext;
        public SAnswerRepositories(Multichoise_DBContext _DB)
        {
            _DBContext = _DB;
        }
        public void Delete(int answerid)
        {
            var ans = _DBContext.Answers.Find(answerid);
            if (ans != null)
            {
                _DBContext.Answers.Remove(ans);
            }
        }

        public IEnumerable<Answer> GetAll()
        {
            return _DBContext.Answers.ToList();
        }

        public Answer GetById(int answerid)
        {
            throw new NotImplementedException();
        }

        public void Insert(Answer answer)
        {
            throw new NotImplementedException();
        }

        public void Save(Answer answer)
        {
            throw new NotImplementedException();
        }

        public void Update(Answer answer)
        {
            throw new NotImplementedException();
        }
    }
}
