using PromoCodeFactory.Domain.Models;


namespace PromoCodeFactory.Domain.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<bool> IsExistAsync(Guid id);
        Task AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        
    }
}
