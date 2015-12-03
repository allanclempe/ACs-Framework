using Microsoft.Practices.Unity;

namespace ACs.NHibernate.Mvc
{
    public class UnityContainerHelper
    {
        public static void Configure(IUnityContainer container)
        {
            Container = container;
        }

        public static IUnityContainer Container { get; private set; }

    }
}
