using Mango.Services.CouponApi.Data;
using Mango.Services.CouponApi.DataAccess.IRepos;

namespace Mango.Services.CouponApi.DataAccess.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
