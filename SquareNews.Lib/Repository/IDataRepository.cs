﻿using SquareNews.Lib.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareNews.Lib.Repository
{
    public interface IDataRepository<T>
    {
        DbFactory DatabaseFactory { get; set; }
        string Create(T obj);
        T GetByKey(string key);
        List<T> GetAll();
        void Update(T obj);
        void Delete(string key);
    }
}
