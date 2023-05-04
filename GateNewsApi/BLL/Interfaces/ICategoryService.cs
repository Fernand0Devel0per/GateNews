using GateNewsApi.Dtos.Categories;

namespace GateNewsApi.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllAsync();
    }
}
