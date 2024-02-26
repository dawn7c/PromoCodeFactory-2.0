using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Application.DatabaseContext;
using PromoCodeFactory.Domain.Abstractions;
using PromoCodeFactory.Domain.Models;
using PromoCodeFactory.Domain.Models.Administration;
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

        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false); ;
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

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            return await Task.Run(() =>
            {
                if (!IsExist(entity.Id, out _))
                {
                    return false;
                }
                _dbSet.Update(entity);
                _context.SaveChanges();
                return true;
            });
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid employeeId)
        {
            return await _context.Employees
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Id == employeeId);
        }


    }
}
