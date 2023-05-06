using GateNewsApi.Domain;
using GateNewsApi.Dtos.News;

namespace GateNewsApi.DAL.Interfaces
{
    public interface INewsDao : IBaseDao<News>
    {
        Task<News> GetByIdAsync(Guid id);
        Task<(List<News> Items, int TotalPages)> GetByTitleAsync(string title, int pageNumber);
        Task<(List<News> Items, int TotalPages)> GetByAuthorAsync(string authorFullName, int pageNumber);
        Task<(List<News> Items, int TotalPages)> GetByCategoryAsync(Guid categoryId, int pageNumber);
        Task<(List<News> Items, int TotalPages)> GetByCategoryAndAuthorAsync(Guid categoryId, string authorFullName, int pageNumber);
        Task<(List<News> Items, int TotalPages)> GetByDateIntervalAsync(DateTime startDate, DateTime endDate, int pageNumber);
        Task<(List<News> Items, int TotalPages)> GetByDateAsync(int pageNumber);
        Task<(List<News> Items, int TotalPages)> GetNewsByKeywordsAsync(WordListRequest wordListRequest, int pageNumber, int pageSize = 10);
    }
}
