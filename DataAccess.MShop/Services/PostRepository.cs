using DataAccess.MShop.Entities;
using DataAccess.MShop.EntitiesFramework;
using DataAccess.MShop.IServices;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MShop.Services
{
    public class PostRepository : IPostRepository
    {
        private MShopDBContext _dbContext;

        public PostRepository(MShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<Post>> GetPosts()
        {

            var list = new List<Post>();
            try
            {
                list = _dbContext.Post.ToList();

            }
            catch(Exception ex)
            {
                throw;
            }
            return list;
        }

        public async Task<int> CreatePost(Post post)
        {
            try
            {
                _dbContext.Post.Add(post);
                 return 1;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
