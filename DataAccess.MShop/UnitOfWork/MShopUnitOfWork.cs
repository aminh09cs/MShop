using DataAccess.MShop.EntitiesFramework;
using DataAccess.MShop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MShop.UnitOfWork
{
    public class MShopUnitOfWork : IMShopUnitOfWork, IDisposable
    {
        private MShopDBContext _dbContext;
        public IPostRepository _postRepository { get; set; }

        public MShopUnitOfWork(MShopDBContext dbContext, IPostRepository postRepository)
        {
            _dbContext = dbContext;
            _postRepository = postRepository;
        }

        public int SaveChange()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }

}
