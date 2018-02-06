﻿using System.Collections.Generic;

namespace JqD.Data.Repository
{
    public interface IRepositoryBase<T>
    {
        int Add(T model);

        int AddBatch(IList<T> items);

        int Delete(int id);

        int Update(T model);

        T Get(int id);

        IEnumerable<T> GetAll();
    }
}