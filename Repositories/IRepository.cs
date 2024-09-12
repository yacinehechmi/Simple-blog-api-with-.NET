namespace Tidjma.Repository;
using Tidjma.Models;
using Microsoft.EntityFrameworkCore;
using Tidjma.Contracts;
using Tidjma.Data;

interface IRepository
{
    public T Get<T>(int id) where T : class;

    public List<T> List<T>(DbSet<T> entities) where T : class;
    //public bool Create<U>(U newDTO);
    //public T Update<T>(T updatedEntity) where T : IModel;
    //public bool Delete<T>(int id) where T : IModel;
}

public class Query
{
    public TModel? Get<TModel>(int id, DbSet<TModel> entities) 
    where TModel : class
    {
        return entities.Find(id);
    }

    public List<TModel> List<TModel>(DbSet<TModel> entities)
    where TModel : class
    {
        return entities.ToList();
    }

    //public bool Create<TContract>(TContract newEntity )
    //{

    //}
}
