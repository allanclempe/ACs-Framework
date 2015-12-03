using System;

namespace ACs.NHibernate.Generic
{
    public interface IDatabaseRequest : IDisposable
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void Finish(bool forceRollback = false);
    }
}
