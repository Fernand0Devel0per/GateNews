using GateNewsApi.Domain;

namespace GateNewsApi.DAL.Interfaces
{
    public interface IAuthorDao : IBaseDao<Author>
    {
        Task<Author> GetByIdAsync(Guid id);
        Task<Author> GetByFullNameAsync(string fullName);
        Task<Author> GetByUserIdAsync(Guid userId);
    }
}
