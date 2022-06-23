﻿using Microsoft.EntityFrameworkCore;
using Multichoice_project.Core.Repositories;
using Multichoice_project.Models;
using Multichoice_project.Repositories;

namespace Multichoice_project.Persistence.Repositories
{
    public class QuestionRepositories : Repositories<Question>, IQuestionRepositories
    {
        public QuestionRepositories(Multichoise_DBContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Question> GetQuestionsByIdTest(int id)
        {
            return ((from ques in this.GetAll()
                     join answer in this.DbContext.Answers on ques.Id equals answer.QuestionId
                     where ques.TestId == id
                     select ques).ToList());
        }
    }
}
