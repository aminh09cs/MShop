using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MShop.Entities
{
    public class Customer
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string refresh_token { get; set; }
        public DateTime refresh_token_expired { get; set; }


    }
    public class CustomerLogin_RequestData
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class UpdateRefreshTokenExpired_RequestData
    {
        public int id { get; set; }
        public string refresh_token { get; set; }
        public DateTime refresh_token_expired { get; set; }
    }
}
