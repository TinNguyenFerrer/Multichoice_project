using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Multichoice_project.Models;
namespace Multichoice_project.GenericRepositories
{
    public interface IRepositories<T> where T : class
    {
        DbSet<T> Dbset { get; }
        Multichoise_DBContext DbContext { get; set; }

        IEnumerable<T> GetAll();
        T? GetByID(int id);
        void Insert(T TEntity);
        void Update(T TEntity);
        void Delete(int ID);
        void Save();
    }
}
