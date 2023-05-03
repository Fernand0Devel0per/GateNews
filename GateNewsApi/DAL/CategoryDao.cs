using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Data;
using GateNewsApi.Domain;
using GateNewsApi.Enums;
using Microsoft.EntityFrameworkCore;

namespace GateNewsApi.DAL
{
    public class CategoryDao : BaseDao<Category>, ICategoryDao
    {

        public CategoryDao(GateNewsDbContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByCodeAsync(CategoryEnum code)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Code == (int)code);
        }
    }
}
