using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Data;
using GateNewsApi.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
                .Include(n => n.Author)
                .SingleOrDefaultAsync(n => n.Id == id);
        }

        public async Task<(List<News> Items, int TotalPages)> GetByTitleAsync(string title, int pageNumber)
        {
            var lowerTitle = title.ToLower();
            var query = GetNewsWithAuthorAndCategory().Where(n => n.Title.ToLower().Contains(lowerTitle));
            return await GetPagedResults(query, pageNumber);
        }

        public async Task<(List<News> Items, int TotalPages)> GetByAuthorAsync(string authorFullName, int pageNumber)
        {
            var names = authorFullName.ToLower().Split(' ');
            var lastName = names.Last();
            var firstName = string.Join(' ', names.Take(names.Length - 1));

            var query = GetNewsWithAuthorAndCategory().Where(n => n.Author.FirstName.ToLower() == firstName && n.Author.LastName.ToLower() == lastName);
            return await GetPagedResults(query, pageNumber);
        }

        public async Task<(List<News> Items, int TotalPages)> GetByCategoryAsync(Guid categoryId, int pageNumber)
        {
            var query = GetNewsWithAuthorAndCategory().Where(n => n.CategoryId == categoryId);
            return await GetPagedResults(query, pageNumber);
        }

        public async Task<(List<News> Items, int TotalPages)> GetByCategoryAndAuthorAsync(Guid categoryId, string authorFullName, int pageNumber)
        {
            var names = authorFullName.ToLower().Split(' ');
            var lastName = names.Last();
            var firstName = string.Join(' ', names.Take(names.Length - 1));

            var query = GetNewsWithAuthorAndCategory().Where(n => n.CategoryId == categoryId && n.Author.FirstName.ToLower() == firstName && n.Author.LastName.ToLower() == lastName);
            return await GetPagedResults(query, pageNumber);
        }

        public async Task<(List<News> Items, int TotalPages)> GetByDateIntervalAsync(DateTime startDate, DateTime endDate, int pageNumber)
        {
            var query = GetNewsWithAuthorAndCategory().Where(n => n.PublishDate.Date >= startDate.Date && n.PublishDate.Date <= endDate.Date);
            return await GetPagedResults(query, pageNumber);
        }

        public async Task<(List<News> Items, int TotalPages)> GetByDateAsync(int pageNumber)
        {
            var query = GetNewsWithAuthorAndCategory().OrderByDescending(n => n.PublishDate);
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
