using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Application.DatabaseContext;
using PromoCodeFactory.Domain.Abstractions;
using PromoCodeFactory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.Application.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        public readonly ApplicationContext _context;
        private readonly DbSet<T> _dbSet;

        public EfRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<bool> IsExistAsync(Guid id)
        {
            return await Task.Run(() => {
                return IsExist(id, out _);
            });
        }
        private bool IsExist(Guid id, out T entity)
        {
            entity = _dbSet.Find(id);
            return entity != null;
        }
    }
}
