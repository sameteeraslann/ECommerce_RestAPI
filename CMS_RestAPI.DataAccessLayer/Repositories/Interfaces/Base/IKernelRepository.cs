using CMS_RestAPI.EntityLayer.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS_RestAPI.DataAccessLayer.Repositories.Interfaces.Base
{
    public interface IKernelRepository<T> where T: class,IBaseEntity
    {
        Task<List<T>> GetAll();
        Task<List<T>> Get(Expression<Func<T, bool>> expression);
        Task<T> GetById(int id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> expression);
        Task<T> FindByDefault(Expression<Func<T, bool>> expression);
        Task<bool> Any(Expression<Func<T, bool>> expression);

        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);

        Task<List<T>> GetAll();

        Task<T> GetById(int id);
    }
}
