﻿using Multichoice_project.Models;
using Multichoice_project.Repositories;

namespace Multichoice_project.Core.Repositories
{
    public interface IQuestionRepositories : IRepositories<Question>
    {
        public IEnumerable<Question> GetQuestionsByIdTest(int id);
    }
}
