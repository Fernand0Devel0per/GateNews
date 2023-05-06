using AutoMapper;
using GateNewsApi.BLL.Interfaces;
using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Domain;
using GateNewsApi.Dtos.News;
using GateNewsApi.Enums;
using GateNewsApi.Helpers.Exceptions;
using GateNewsApi.Helpers.OpenIA;

namespace GateNewsApi.BLL
{
    public class NewsService : INewsService
    {
        private readonly INewsDao _newsDao;
        private readonly IMapper _mapper;
        private readonly ICategoryDao _categoryDao;
        private readonly IAuthorDao _authorDao;

        private readonly ContentModerationService _contentModerationService;

        public NewsService(INewsDao newsDao, 
                           IMapper mapper,
                           ICategoryDao categoryDao, 
                           IAuthorDao authorDao,
                           IConfiguration configuration)
        {
            _newsDao = newsDao;
            _mapper = mapper;
            _categoryDao = categoryDao;
            _authorDao = authorDao;
            _contentModerationService = new ContentModerationService(configuration);
        }

        public async Task<(List<NewsResponse> Items, int TotalPages)> GetByTitleAsync(string title, int pageNumber)
        {
            var result = await _newsDao.GetByTitleAsync(title, pageNumber);
            var mappedItems = _mapper.Map<List<NewsResponse>>(result.Items);
            return (mappedItems, result.TotalPages);
        }

        public async Task<(List<NewsResponse> Items, int TotalPages)> GetByAuthorAsync(string authorFullName, int pageNumber)
        {
            var result = await _newsDao.GetByAuthorAsync(authorFullName, pageNumber);
            var mappedItems = _mapper.Map<List<NewsResponse>>(result.Items);
            return (mappedItems, result.TotalPages);
        }

        public async Task<(List<NewsResponse> Items, int TotalPages)> GetByCategoryAsync(int categoryCode, int pageNumber)
        {
            var category = await _categoryDao.GetByCodeAsync((CategoryEnum)categoryCode);
            if (category == null)
            {
                throw new CategoryNotFoundException(categoryCode);
            }

            var result = await _newsDao.GetByCategoryAsync(category.Id, pageNumber);
            var mappedItems = _mapper.Map<List<NewsResponse>>(result.Items);
            return (mappedItems, result.TotalPages);
        }

        public async Task<(List<NewsResponse> Items, int TotalPages)> GetByCategoryAndAuthorAsync(int categoryCode, string authorFullName, int pageNumber)
        {
            var category = await _categoryDao.GetByCodeAsync((CategoryEnum)categoryCode);
            if (category == null)
            {
                throw new CategoryNotFoundException(categoryCode);
            }

            var result = await _newsDao.GetByCategoryAndAuthorAsync(category.Id, authorFullName, pageNumber);
            var mappedItems = _mapper.Map<List<NewsResponse>>(result.Items);
            return (mappedItems, result.TotalPages);
        }

        public async Task<(List<NewsResponse> Items, int TotalPages)> GetByDateIntervalAsync(DateTime startDate, DateTime endDate, int pageNumber)
        {
            var result = await _newsDao.GetByDateIntervalAsync(startDate, endDate, pageNumber);
            var mappedItems = _mapper.Map<List<NewsResponse>>(result.Items);
            return (mappedItems, result.TotalPages);
        }

        public async Task<(List<NewsResponse> Items, int TotalPages)> GetByDateAsync(int pageNumber)
        {
            var result = await _newsDao.GetByDateAsync(pageNumber);
            var mappedItems = _mapper.Map<List<NewsResponse>>(result.Items);
            return (mappedItems, result.TotalPages);
        }

        public async Task<(List<NewsResponse> Items, int TotalPages)> GetNewsByKeywordsAsync(WordListRequest wordListRequest, int pageNumber, int pageSize = 10)
        {
            var result = await _newsDao.GetNewsByKeywordsAsync(wordListRequest, pageNumber, pageSize);
            var newsResponseList = _mapper.Map<List<NewsResponse>>(result.Items);
            return (newsResponseList, result.TotalPages);
        }

        public async Task<NewsResponse> CreateNews(NewsCreateRequest request, Guid userId)
        {

            bool IsInappropriateContent = await _contentModerationService.CheckForInappropriateContent(request.Content);
            if (IsInappropriateContent)
            {
                throw new InvalidOperationException("The provided text contains inappropriate content.");
            }

            var author = await _authorDao.GetByUserIdAsync(userId);
            if (author is null)
            {
                throw new AuthorNotFoundException("Author not found for the given user ID.");
            }

            var category = await _categoryDao.GetByCodeAsync(request.Category);
            if (category is null)
            {
                throw new CategoryNotFoundException((int)request.Category);
            }

            var news = _mapper.Map<News>(request);

            news.Author = author;
            news.Category = category;

            var createdNews = await _newsDao.AddAsync(news);
            try
            {
                return _mapper.Map<NewsResponse>(createdNews);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<bool> UpdateNews(Guid id, NewsUpdateRequest request, Guid userId)
        {

            bool IsInappropriateContent = await _contentModerationService.CheckForInappropriateContent(request.Content);
            if (IsInappropriateContent)
            {
                throw new InvalidOperationException("The provided text contains inappropriate content.");
            }

            var category = await _categoryDao.GetByCodeAsync(request.Category);
            if (category is null)
            {
                throw new CategoryNotFoundException((int)request.Category);
            }

            var existingNews = await _newsDao.GetByIdAsync(id);
            if (existingNews is null)
            {
                return false;
            }

            if (existingNews.Author.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this news item.");
            }

            _mapper.Map(request, existingNews);
            var result = await _newsDao.UpdateAsync(existingNews);
            return result is not null;
        }

        public async Task<bool> DeleteNews(Guid id, Guid userId)
        {
            var news = await _newsDao.GetByIdAsync(id);
            if (news is null)
            {
                return false;
            }

            if (news.Author.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this news item.");
            }

            return await _newsDao.DeleteAsync(news);
        }

    }
}