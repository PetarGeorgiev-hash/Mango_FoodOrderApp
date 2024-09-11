using Mango.Services.CouponApi.Data;
using Mango.Services.CouponApi.DataAccess.IRepos;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponApi.DataAccess.Repos
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly DbSet<T> _set;
        private readonly AppDbContext _db;

        public GenericRepo(AppDbContext db)
        {
            _db = db;
            _set = _db.Set<T>();
        }

        
        public async Task<T> CreateAsync(T entity)
        {
            await _set.AddAsync(entity);
            return entity;
        }

        public async Task<T> Delete(int id)
        {
            var set = await _set.FindAsync(id);
            if(set == null)
            { 
                return null; 
            }
            _set.Remove(set);
            return set;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }
    }
}
