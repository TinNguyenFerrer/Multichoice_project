﻿using Multichoice_project.Models;
using Multichoice_project.GenericRepositories;

namespace Multichoice_project.Core.Repositories
{
    public interface IAnswerRepositories : IRepositories<Answer>
    {
        public bool IsRightAnswer(int Id);
    }
}
