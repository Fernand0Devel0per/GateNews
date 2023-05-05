using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Data;

namespace GateNewsApi.DAL
{
    public class BaseDao<T> : IBaseDao<T> where T : class
    {
        protected readonly GateNewsDbContext _context;

        public BaseDao(GateNewsDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Update(entity);
            var rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected > 0 ? entity : null;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
