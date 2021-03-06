﻿using System.Linq;

namespace RecipeRepositories
{
    public interface IRepository<T>
    {
        T Add(T item);
        T Update(int id, T item);
        T Get(int id);
        IQueryable<T> All();
    }
}
