using GateNewsApi.Domain;

namespace GateNewsApi.DAL.Interfaces
{
    public interface ICategoryDao : IBaseDao<Category>
    {
        Task<List<Category>> GetAllAsync();
    }
}
