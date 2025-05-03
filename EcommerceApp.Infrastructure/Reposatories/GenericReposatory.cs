using EcommerceApp.Domain.Interfaces;
using EcommerceApp.Infrastructure.Data;
using EcommerceApp.Application.Exsceptions;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Infrastructure.Reposatories;
public class GenericReposatory<TEntity>(AppDbContext context) : IGeneric<TEntity> where TEntity : class
{
    public async Task<int> AddAsync(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        if (entity is null) 
            return 0;

        context.Set<TEntity>().Remove(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        var result = await context.Set<TEntity>().FindAsync(id) ??
            throw new ItemNotFoundException($"item with {id} not found");

        return result;
    }

    public async Task<int> UpdateAsync(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
        return await context.SaveChangesAsync();
    }
}
