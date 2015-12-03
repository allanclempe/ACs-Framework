using NHibernate;
using ACs.NHibernate.Generic;

namespace ACs.NHibernate
{
    public interface IDatabaseSession : IDatabaseFactory
    {
        ISession Session { get; }

    }
}
