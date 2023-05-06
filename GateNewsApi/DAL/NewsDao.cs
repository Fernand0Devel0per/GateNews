using Dapper;
using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Data;
using GateNewsApi.Domain;
using GateNewsApi.Dtos.News;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;

namespace GateNewsApi.DAL
{
    public class NewsDao : BaseDao<News>, INewsDao
    {
        private const int PageSize = 10;
        private readonly IConfiguration _configuration;

        public NewsDao(GateNewsDbContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
        }


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

        //public async Task<(List<News> Items, int TotalPages)> GetNewsByKeywordsAsync(WordListRequest wordListRequest, int pageNumber, int pageSize = 10)
        //{
        //    var keywords = wordListRequest.Words;

        //    using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        //    var queryBuilder = new StringBuilder();
        //    queryBuilder.Append($@"SELECT n.*, a.*, c.*
        //                       FROM News n
        //                       INNER JOIN Authors a ON n.AuthorId = a.Id
        //                       INNER JOIN Categories c ON n.CategoryId = c.Id
        //                       WHERE ");

        //    for (int i = 0; i < keywords.Count; i++)
        //    {
        //        if (i > 0)
        //        {
        //            queryBuilder.Append(" OR ");
        //        }

        //        queryBuilder.Append($"n.Title LIKE @Keyword{i} OR n.Content LIKE @Keyword{i}");
        //    }

        //    queryBuilder.Append(" ORDER BY n.PublishDate OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

        //    var parameters = new DynamicParameters();
        //    for (int i = 0; i < keywords.Count; i++)
        //    {
        //        parameters.Add($"Keyword{i}", $"%{keywords[i]}%");
        //    }

        //    parameters.Add("Offset", (pageNumber - 1) * pageSize);
        //    parameters.Add("PageSize", pageSize);

        //    var news = await connection.QueryAsync<News, Author, Category, News>(
        //    queryBuilder.ToString(),
        //    (n, a, c) =>
        //    {
        //        n.Author = a;
        //        n.Category = c;
        //        return n;
        //    },
        //    parameters,
        //    splitOn: "Id,Id");



        //    var countQueryBuilder = new StringBuilder();
        //    countQueryBuilder.Append("SELECT COUNT(*) FROM News n WHERE ");

        //    for (int i = 0; i < keywords.Count; i++)
        //    {
        //        if (i > 0)
        //        {
        //            countQueryBuilder.Append(" OR ");
        //        }

        //        countQueryBuilder.Append($"n.Title LIKE @Keyword{i} OR n.Content LIKE @Keyword{i}");
        //    }

        //    var totalCount = await connection.ExecuteScalarAsync<int>(countQueryBuilder.ToString(), parameters);
        //    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        //    return (news.ToList(), totalPages);
        //}

        public async Task<(List<News> Items, int TotalPages)> GetNewsByKeywordsAsync(WordListRequest wordListRequest, int pageNumber, int pageSize = 10)
        {
            var keywords = wordListRequest.Words;

            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var query = BuildSearchQuery(keywords, pageNumber, pageSize);
            var parameters = PrepareSearchParameters(keywords, pageNumber, pageSize);

            var news = await ExecuteSearchQueryAsync(connection, query, parameters);
            var totalCount = await GetTotalCountAsync(connection, keywords);
            var totalPages = CalculateTotalPages(totalCount, pageSize);

            return (news, totalPages);
        }

        private string BuildSearchQuery(List<string> keywords, int pageNumber, int pageSize)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.Append(@"SELECT n.*, a.*, c.*
                          FROM News n
                          INNER JOIN Authors a ON n.AuthorId = a.Id
                          INNER JOIN Categories c ON n.CategoryId = c.Id
                          WHERE ");

            for (int i = 0; i < keywords.Count; i++)
            {
                if (i > 0)
                {
                    queryBuilder.Append(" OR ");
                }

                queryBuilder.Append($"n.Title LIKE @Keyword{i} OR n.Content LIKE @Keyword{i}");
            }

            queryBuilder.Append(" ORDER BY n.PublishDate OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            return queryBuilder.ToString();
        }

        private DynamicParameters PrepareSearchParameters(List<string> keywords, int pageNumber, int pageSize)
        {
            var parameters = new DynamicParameters();
            for (int i = 0; i < keywords.Count; i++)
            {
                parameters.Add($"Keyword{i}", $"%{keywords[i]}%");
            }

            parameters.Add("Offset", (pageNumber - 1) * pageSize);
            parameters.Add("PageSize", pageSize);

            return parameters;
        }

        private async Task<List<News>> ExecuteSearchQueryAsync(SqlConnection connection, string query, DynamicParameters parameters)
        {
            var news = await connection.QueryAsync<News, Author, Category, News>(
                query,
                (n, a, c) =>
                {
                    n.Author = a;
                    n.Category = c;
                    return n;
                },
                parameters,
                splitOn: "Id,Id");

            return news.ToList();
        }

        private async Task<int> GetTotalCountAsync(SqlConnection connection, List<string> keywords)
        {
            var countQueryBuilder = new StringBuilder();
            countQueryBuilder.Append("SELECT COUNT(*) FROM News n WHERE ");

            for (int i = 0; i < keywords.Count; i++)
            {
                if (i > 0)
                {
                    countQueryBuilder.Append(" OR ");
                }

                countQueryBuilder.Append($"n.Title LIKE @Keyword{i} OR n.Content LIKE @Keyword{i}");
            }

            var totalCount = await connection.ExecuteScalarAsync<int>(countQueryBuilder.ToString(), PrepareCountParameters(keywords));

            return totalCount;
        }

        private DynamicParameters PrepareCountParameters(List<string> keywords)
        {
            var parameters = new DynamicParameters();
            for (int i = 0; i < keywords.Count; i++)
            {
                parameters.Add($"Keyword{i}", $"%{keywords[i]}%");
            }

            return parameters;
        }

        private int CalculateTotalPages(int totalCount, int pageSize)
        {
            return (int)Math.Ceiling((double)totalCount / pageSize);
        }
    }
}
