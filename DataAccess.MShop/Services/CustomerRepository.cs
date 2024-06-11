using DataAccess.MShop.Entities;
using DataAccess.MShop.EntitiesFramework;
using DataAccess.MShop.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MShop.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private MShopDBContext _dbContext;

        public CustomerRepository(MShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Customer> Login(CustomerLogin_RequestData requestData)
        {
            var customer = new Customer();
            try
            {
               
                customer = _dbContext.Customer
                    .Where(s => s.username == requestData.username && s.password == requestData.password)
                    .FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

            return customer;
        }

        public async Task<int> UpdateRefreshTokenExpired(UpdateRefreshTokenExpired_RequestData requestData)
        {
            var result = 0;
            try
            {
                var customer = _dbContext.Customer
                    .Where(s => s.id == requestData.id)
                    .FirstOrDefault();
                if(customer != null && customer.id > 0)
                {
                    customer.refresh_token = requestData.refresh_token;
                    customer.refresh_token_expired = requestData.refresh_token_expired;

                    _dbContext.Update(customer);
                }
                return result = 1;
            }
            catch (Exception)
            {

                return result = -99;
            }
        }
    }
}
