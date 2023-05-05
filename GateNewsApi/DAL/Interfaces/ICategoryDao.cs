using GateNewsApi.Domain;
using GateNewsApi.Enums;

namespace GateNewsApi.DAL.Interfaces
{
    public interface ICategoryDao : IBaseDao<Category>
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByCodeAsync(CategoryEnum code);
        Task SeedCategories();
    }
}
