using GateNewsApi.Dtos.News;

namespace GateNewsApi.BLL.Interfaces
{
    public interface INewsService
    {
        Task<(List<NewsResponse> Items, int TotalPages)> GetByTitleAsync(string title, int pageNumber);
        Task<(List<NewsResponse> Items, int TotalPages)> GetByAuthorAsync(string authorFullName, int pageNumber);
        Task<(List<NewsResponse> Items, int TotalPages)> GetByCategoryAsync(int categoryCode, int pageNumber);
        Task<(List<NewsResponse> Items, int TotalPages)> GetByCategoryAndAuthorAsync(int categoryCode, string authorFullName, int pageNumber);
        Task<(List<NewsResponse> Items, int TotalPages)> GetByDateIntervalAsync(DateTime startDate, DateTime endDate, int pageNumber);
        Task<(List<NewsResponse> Items, int TotalPages)> GetByDateAsync(int pageNumber);
        Task<(List<NewsResponse> Items, int TotalPages)> GetNewsByKeywordsAsync(WordListRequest wordListRequest, int pageNumber, int pageSize = 10);
        Task<NewsResponse> CreateNews(NewsCreateRequest request, Guid userId);
        Task<bool> UpdateNews(Guid id, NewsUpdateRequest request, Guid userId);
        Task<bool> DeleteNews(Guid id, Guid userId);
    }
}
