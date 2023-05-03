using GateNewsApi.Domain;

namespace GateNewsApi.DAL.Interfaces
{
    public interface INewsDao : IBaseDao<News>
    {
        Task<(List<News> Items, int TotalPages)> GetByTitleAsync(string title, int pageNumber);
        Task<(List<News> Items, int TotalPages)> GetByAuthorAsync(string authorFullName, int pageNumber);
        Task<(List<News> Items, int TotalPages)> GetByCategoryAsync(Guid categoryId, int pageNumber);
        Task<(List<News> Items, int TotalPages)> GetByCategoryAndAuthorAsync(Guid categoryId, string authorFullName, int pageNumber);
        Task<(List<News> Items, int TotalPages)> GetByDateIntervalAsync(DateTime startDate, DateTime endDate, int pageNumber);
        Task<(List<News> Items, int TotalPages)> GetByDateAsync(int pageNumber);
        Task<(List<News> Items, int TotalPages)> GetByWordsAsync(List<string> words, int pageNumber);
    }
}
