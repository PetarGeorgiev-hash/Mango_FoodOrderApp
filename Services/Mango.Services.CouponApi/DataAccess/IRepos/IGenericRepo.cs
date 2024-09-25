namespace Mango.Services.CouponApi.DataAccess.IRepos
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<T> CreateAsync(T entity);

        Task<T> Delete(int id);
    }
}
