﻿using DataAccess.MShop.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MShop.UnitOfWork
{
    public interface IMShopUnitOfWork
    {
        public IPostRepository _postRepository { get; set; }
        int SaveChange();
    }
}
