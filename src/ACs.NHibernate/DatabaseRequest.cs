using System;
using NHibernate;
using NHibernate.Context;
using ACs.NHibernate.Generic;

namespace ACs.NHibernate
{
    public class DatabaseRequest : IDatabaseRequest
    {
        private readonly ISession _session;

        public DatabaseRequest(ISession session)
        {
            _session = session;
        }

        internal virtual IDatabaseRequest Open(bool beginTransaction = true)
        {
            if (beginTransaction) _session.BeginTransaction();
            
            return this;

        }

        public void BeginTransaction()
        {
            _session.Transaction.Begin();
        }

        public virtual void CommitTransaction()
        {
            _session.Transaction.Commit();
        }

        public virtual void RollbackTransaction()
        {
            _session.Transaction.Rollback();
        }

        public virtual void Finish(bool forceRollback = false)
        {
            if (_session == null) return;

            try
            {
                if (!_session.Transaction.IsActive) return;

                if (forceRollback)
                {
                    _session.Transaction.Rollback();
                    return;
                }

                _session.Transaction.Commit();

            }
            catch (Exception)
            {
                if (_session.IsOpen && _session.Transaction.IsActive)
                    _session.Transaction.Rollback();

                throw;
            }
        }


        public void Dispose()
        {
            Finish();
        }

    }
}
