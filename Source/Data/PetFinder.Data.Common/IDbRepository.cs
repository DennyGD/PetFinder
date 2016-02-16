namespace PetFinder.Data.Common
{
    using System.Linq;

    using PetFinder.Data.Common.Models;

    //public interface IDbRepository<T> : IDbRepository<T>
    //    where T : class, IDeletableEntity
    //{
    //}

    public interface IDbRepository<T>
        where T : class, IDeletableEntity
    {
        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        T GetById(object id);

        void Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);

        void Save();
    }
}
