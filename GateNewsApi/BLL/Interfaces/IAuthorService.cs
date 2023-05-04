using GateNewsApi.Dtos.Authors;

namespace GateNewsApi.BLL.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorResponse> GetByIdAsync(Guid id);
        Task<AuthorResponse> GetByFullNameAsync(string fullName);
        Task<bool> DeleteAuthorAsync(Guid id);
        Task<bool> UpdateAuthorAsync(Guid id, AuthorUpdateRequest request);
        
    }
}
