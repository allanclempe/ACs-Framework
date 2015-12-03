
namespace ACs.NHibernate.Generic
{
    public interface IDatabaseFactory 
    {
        IDatabaseRequest BeginRequest(bool beginTransaction = true);
    }
}
