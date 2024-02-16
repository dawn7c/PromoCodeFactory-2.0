using PromoCodeFactory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.Domain.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> IsExistAsync(Guid id);
    }
}
