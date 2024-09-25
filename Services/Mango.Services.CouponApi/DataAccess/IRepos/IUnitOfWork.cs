namespace Mango.Services.CouponApi.DataAccess.IRepos
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
