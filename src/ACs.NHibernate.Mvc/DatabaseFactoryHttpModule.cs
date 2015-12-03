using System;
using System.Web;
using Microsoft.Practices.Unity;
using ACs.NHibernate.Generic;

namespace ACs.NHibernate.Mvc
{
    public class DatabaseFactoryHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
           
        }

        public void Dispose()
        {
        }


        private static void BeginRequest(object sender, EventArgs e)
        {
            UnityContainerHelper.Container.Resolve<IDatabaseFactory>().BeginRequest();
        }

        private static void EndRequest(object sender, EventArgs e)
        {
            DatabaseFactory.GetRequest().Finish((HttpContext.Current != null && ((HttpContext.Current.AllErrors != null && HttpContext.Current.AllErrors.Length > 0) || HttpContext.Current.Error != null)));
        }


    }
}


