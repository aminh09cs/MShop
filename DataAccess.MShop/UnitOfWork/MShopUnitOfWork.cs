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
        public ICustomerRepository _customerRepository { get; set; }

        public MShopUnitOfWork(MShopDBContext dbContext, ICustomerRepository customerRepository)
        {
            _dbContext = dbContext;
            _customerRepository = customerRepository;
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
