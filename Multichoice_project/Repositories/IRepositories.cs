using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Multichoice_project.Repositories
{
    public interface IRepositories<T> where T : class
    {
        DbSet<T> Dbset { get; }
        DbContext DbContext { get; set; }

        IEnumerable<T> GetAll();
        T GetByID(int id);
        void Insert(T TEntity);
        void Update(T TEntity);
        void Delete(int ID);
        void Save();
    }
}
