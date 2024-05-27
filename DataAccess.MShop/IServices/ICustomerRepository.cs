using DataAccess.MShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MShop.IServices
{
    public interface ICustomerRepository
    {
        Task<Customer> Login(CustomerLogin_RequestData requestData);
        Task<int> UpdateRefreshTokenExpired(UpdateRefreshTokenExpired_RequestData requestData);

    }
}
