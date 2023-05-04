using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Data;
using GateNewsApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace GateNewsApi.DAL
{
    public class NewsDao : BaseDao<News>, INewsDao
    {
        private const int PageSize = 10;

        public NewsDao(GateNewsDbContext context) : base(context) { }


        public async Task<News> GetByIdAsync(Guid id)
        {
            return await _context.News
                .Include(n => n.Category)
                .SingleOrDefaultAsync(n => n.Id == id);
        }

        public async Task<(List<News> Items, int TotalPages)> GetByTitleAsync(string title, int pageNumber)
        {
            var query = GetNewsWithAuthorAndCategory().Where(n => n.Title.Contains(title));
            return await GetPagedResults(query, pageNumber);
        }

        public async Task<(List<News> Items, int TotalPages)> GetByAuthorAsync(string authorFullName, int pageNumber)
        {
            var names = authorFullName.Split(' ');
            var firstName = names[0];
            var lastName = names[1];

            var query = GetNewsWithAuthorAndCategory().Where(n => n.Author.FirstName == firstName && n.Author.LastName == lastName);
            return await GetPagedResults(query, pageNumber);
        }

        public async Task<(List<News> Items, int TotalPages)> GetByCategoryAsync(Guid categoryId, int pageNumber)
        {
            var query = GetNewsWithAuthorAndCategory().Where(n => n.CategoryId == categoryId);
            return await GetPagedResults(query, pageNumber);
        }

        public async Task<(List<News> Items, int TotalPages)> GetByCategoryAndAuthorAsync(Guid categoryId, string authorFullName, int pageNumber)
        {
            var names = authorFullName.Split(' ');
            var firstName = names[0];
            var lastName = names[1];

            var query = GetNewsWithAuthorAndCategory().Where(n => n.CategoryId == categoryId && n.Author.FirstName == firstName && n.Author.LastName == lastName);
            return await GetPagedResults(query, pageNumber);
        }

        public async Task<(List<News> Items, int TotalPages)> GetByDateIntervalAsync(DateTime startDate, DateTime endDate, int pageNumber)
        {
            var query = GetNewsWithAuthorAndCategory().Where(n => n.PublishDate >= startDate && n.PublishDate <= endDate);
            return await GetPagedResults(query, pageNumber);
        }

        public async Task<(List<News> Items, int TotalPages)> GetByDateAsync(int pageNumber)
        {
            var query = GetNewsWithAuthorAndCategory().OrderByDescending(n => n.PublishDate);
            return await GetPagedResults(query, pageNumber);
        }

        public async Task<(List<News> Items, int TotalPages)> GetByWordsAsync(List<string> words, int pageNumber)
        {
            var query = GetNewsWithAuthorAndCategory().Where(n => words.Any(w => n.Content.Contains(w)));
            return await GetPagedResults(query, pageNumber);
        }

        private async Task<(List<News> Items, int TotalPages)> GetPagedResults(IQueryable<News> query, int pageNumber)
        {
            int totalCount = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);


            var items = await query.Skip((pageNumber - 1) * PageSize)
                                   .Take(PageSize)
                                   .ToListAsync();

            return (items, totalPages);

        }

        private IQueryable<News> GetNewsWithAuthorAndCategory()
        {
            return _context.News
                .Include(n => n.Author)
                .Include(n => n.Category);
        }
    }
}
