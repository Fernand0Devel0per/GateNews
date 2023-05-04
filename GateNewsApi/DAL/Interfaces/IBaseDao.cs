namespace GateNewsApi.DAL.Interfaces
{
    public interface IBaseDao<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
