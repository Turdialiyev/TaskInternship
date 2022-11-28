#pragma warning disable
using System;
using System.Linq.Expressions;
using Task.Data;

namespace Task.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context) => _context = context;
    public async ValueTask<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public async ValueTask AddRange(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public IQueryable<TEntity> GetAll() => _context.Set<TEntity>();

    public TEntity GetById(int id)
    {
       return _context.Set<TEntity>().Find(id);
    }

    public async ValueTask<TEntity> Update(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();

        return entry.Entity;
    }
}