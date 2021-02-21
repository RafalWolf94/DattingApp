using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces {
    public interface ILikesRespository {
        Task<UserLike> GetUserLike (int sourceUserId, int likeUserId);
        Task<AppUser> GetUserWithLike (int userId);
        Task<PagedList<LikeDto>> GetUserLikes (LikesParams likesParams);
    }
}