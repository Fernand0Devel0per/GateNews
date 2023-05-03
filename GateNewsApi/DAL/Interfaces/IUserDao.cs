using GateNewsApi.Domain;

namespace GateNewsApi.DAL.Interfaces
{
    public interface IUserDao : IBaseDao<User>
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByFullNameAsync(string fullName);
        Task<User> GetByEmailAsync(string email);
    }
}
