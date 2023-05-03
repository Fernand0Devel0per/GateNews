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

        public async Task<Author> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<Author> GetByFullNameAsync(string fullName)
        {
            var names = fullName.Split(' ');
            var lastName = names.Last();
            var firstName = string.Join(' ', names.Take(names.Length - 1));

            return await _context.Users
                .FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);
        }

    }
}
