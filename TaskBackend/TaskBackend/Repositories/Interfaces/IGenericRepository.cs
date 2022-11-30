#pragma warning disable
using System.Linq.Expressions;

namespace Task.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    TEntity GetById(int id);
    ValueTask<TEntity> AddAsync(TEntity entity);
    ValueTask<TEntity> Update(TEntity entity);
}