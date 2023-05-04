using AutoMapper;
using GateNewsApi.BLL.Interfaces;
using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Dtos.Categories;

namespace GateNewsApi.BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDao _categoryDao;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryDao categoryDao, IMapper mapper)
        {
            _categoryDao = categoryDao;
            _mapper = mapper;
        }

        public async Task<List<CategoryResponse>> GetAllAsync()
        {
            var categories = await _categoryDao.GetAllAsync();
            return _mapper.Map<List<CategoryResponse>>(categories);
        }
    }
}
