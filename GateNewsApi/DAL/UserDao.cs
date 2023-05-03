using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Data;
using GateNewsApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace GateNewsApi.DAL
{
    public class UserDao : BaseDao<User>, IUserDao
    {
        public UserDao(GateNewsDbContext context) : base(context)
        {
            
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByFullNameAsync(string fullName)
        {
            var names = fullName.Split(' ');
            var lastName = names.Last();
            var firstName = string.Join(' ', names.Take(names.Length - 1));

            return await _context.Users
                .FirstOrDefaultAsync(u => u.FirstName == firstName && u.LastName == lastName);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
