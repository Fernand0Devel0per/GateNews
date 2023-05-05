using AutoMapper;
using GateNewsApi.BLL.Interfaces;
using GateNewsApi.DAL.Interfaces;
using GateNewsApi.Domain;
using GateNewsApi.Dtos.Authors;
using Microsoft.AspNetCore.Identity;

namespace GateNewsApi.BLL
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorDao _authorDao;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorDao authorDao, UserManager<User> userManager, IMapper mapper)
        {
            _authorDao = authorDao;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<AuthorResponse> GetByIdAsync(Guid id)
        {
            var author = await _authorDao.GetByIdAsync(id);
            return _mapper.Map<AuthorResponse>(author);
        }

        public async Task<AuthorResponse> GetByFullNameAsync(string fullName)
        {
            var author = await _authorDao.GetByFullNameAsync(fullName);
            return _mapper.Map<AuthorResponse>(author);
        }


        public async Task<bool> DeleteAuthorAsync(Guid userId)
        {
            var author = await _authorDao.GetByUserIdAsync(userId);

            if (author is null)
            {
                return false;
            }

            bool authorDeleted = await _authorDao.DeleteAsync(author);
     
            if (authorDeleted)
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user is not null)
                {
                    var result = await _userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<bool> UpdateAuthorAsync(Guid userId, AuthorUpdateRequest request)
        {
            var author = await _authorDao.GetByUserIdAsync(userId);

            if (author is null)
            {
                return false;
            }

            _mapper.Map(request, author);

            var result = _authorDao.UpdateAsync(author);
            return result is not null;
        }

        
    }
}
