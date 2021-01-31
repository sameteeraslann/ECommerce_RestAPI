using CMS_RestAPI.DataAccessLayer.Context;
using CMS_RestAPI.DataAccessLayer.Repositories.Interfaces.Base;
using CMS_RestAPI.EntityLayer.Entities.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS_RestAPI.DataAccessLayer.Repositories.Concrete.Base
{
    public class KernelRepository<T> : IKernelRepository<T> where T : class, IBaseEntity
    {

        private readonly ApplicationDbContext _applicationDbContext;
        protected DbSet<T> _table;


        public KernelRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task Add(T entity)
        {
            await _table.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _table.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll() => await _table.ToListAsync();


        public async Task<T> GetById(int id) => await _table.FindAsync(id);

        public async Task Update(T entity)
        {
            _applicationDbContext.Entry<T>(entity).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression) => await _table.AnyAsync(expression);

        public async Task<T> FindByDefault(Expression<Func<T, bool>> expression) => await _table.FirstAsync(expression);

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> expression) => await _table.Where(expression).FirstOrDefaultAsync();

        public async Task<List<T>> Get(Expression<Func<T, bool>> expression) => await _table.Where(expression).ToListAsync();

      

    }
}
