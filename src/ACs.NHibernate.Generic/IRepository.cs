
namespace ACs.NHibernate.Generic
{
    public interface IRepository<T> where T : IEntityRoot, IEntityId
    {
        T GetById(long id);
        void Save(T entity);
    }
}
