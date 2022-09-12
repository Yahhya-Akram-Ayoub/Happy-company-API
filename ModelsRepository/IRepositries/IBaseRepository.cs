﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModelsRepository.IRepositries
{
    public interface IBaseRepository<T> where T : class
    {
        T Add(T entity);
        void Update(T entity);

        int Count(Expression<Func<T, bool>> exp);
        int Count();
        IEnumerable<T> SaveRange(IEnumerable<T> list);
        IQueryable<T> GetPaged(int page, int pageSize, out int count, Expression<Func<T, bool>> exp);
        IQueryable<T> GetPaged(int page, int pageSize, out int count);
        IQueryable<T> GetOrderdPaged(int page, int pageSize, out int count, Expression<Func<T, object>> exp);
        IQueryable<T> GetOrderdDesPaged(int page, int pageSize, out int count, Expression<Func<T, object>> exp);
        void DeleteEntity(Expression<Func<T, bool>> exp);
        T GetEntity(Expression<Func<T, bool>> exp);
    }
}
