using Microsoft.EntityFrameworkCore;
using Multichoice_project.Core.Repositories;
using Multichoice_project.Models;
using Multichoice_project.GenericRepositories;

namespace Multichoice_project.Persistence.Repositories
{
    public class TestRepositories : Repositories<Test>, ITestRepositories
    {
        public TestRepositories(Multichoise_DBContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Test> GetListByIdSubjectId(int idsubj)
        {
            return (from test in this.GetAll() 
                    where test.SubjectId == idsubj 
                    select test).ToList();

        }

        public IEnumerable<Test> GetListByIdUser(int iduser)
        {
            return (from test in this.GetAll()
                    where test.UserId == iduser
                    select test).ToList();
        }
    }
}
