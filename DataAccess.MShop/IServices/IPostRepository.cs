using DataAccess.MShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MShop.IServices
{
    public interface IPostRepository
    {
        Task<List<Post>> GetPosts();
        Task<int> CreatePost(Post post);

    }
}
