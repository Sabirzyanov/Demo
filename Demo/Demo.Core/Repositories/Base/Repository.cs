using Demo.Core.Context;
using Demo.Core.Entites.Base;

namespace Demo.Core.Repositories.Base;

public class Repository<TEntity> where TEntity : Entity
{
    private protected readonly DBContext Context;

    public Repository(DBContext context)
    {
        Context = context;
    }

    
    public void Create(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
        Context.SaveChanges();
    }

    public void CreateRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().AddRange(entities);
        Context.SaveChanges();
    }

    public TEntity? Get(int id)
    {
        return Context.Set<TEntity>().Find(id);
    }

    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
        Context.SaveChanges();
    }
    
    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().UpdateRange(entities);
        Context.SaveChanges();
    }

    public void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        Context.SaveChanges();
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
        Context.SaveChanges();
    }

    public List<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList();
    }

    public List<TEntity> Find(Func<TEntity, bool> predicate)
    {
        return Context.Set<TEntity>().Where(predicate).ToList();
    }

    public TEntity? FindFirst(Func<TEntity, bool> predicate)
    {
        return Context.Set<TEntity>().FirstOrDefault(predicate);
    }

}