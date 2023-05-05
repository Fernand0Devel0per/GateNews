using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Data;
using GateNewsApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace GateNewsApi.DAL
{
    public class AuthorDao : BaseDao<Author>, IAuthorDao
    {
        public AuthorDao(GateNewsDbContext context) : base(context)
        {
            
        }

        public async Task<Author> GetByUserIdAsync(Guid userId)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<Author> GetByIdAsync(Guid id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<Author> GetByFullNameAsync(string fullName)
        {
            var names = fullName.ToLowerInvariant().Split(' ');
            var lastName = names.Last();
            var firstName = string.Join(' ', names.Take(names.Length - 1));

            return await _context.Authors
                .FirstOrDefaultAsync(u => u.FirstName.ToLowerInvariant() == firstName && u.LastName.ToLowerInvariant() == lastName);
        }

    }
}
