using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Multichoice_project.Models;

namespace Multichoice_project.f
{
    public interface SIAnswerRepositories
    {
        IEnumerable<Answer> GetAll();
        Answer GetById(int answerid);
        void Insert(Answer answer);
        void Update(Answer answer);
        void Delete(int answerid);
        void Save(Answer answer);
    }
}
