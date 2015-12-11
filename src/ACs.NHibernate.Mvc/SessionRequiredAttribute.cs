using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ACs.NHibernate.Generic;

namespace ACs.NHibernate.Mvc
{
    
    public class SessionRequiredAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public bool OpenTransaction { get; set; }
        public bool ModelStateErrorChecker { get; set; }
        private IDatabaseRequest _request;

        public SessionRequiredAttribute()
        {
            OpenTransaction = true;
            ModelStateErrorChecker = true;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var factory = (IDatabaseFactory)UnityContainerHelper.Container.Resolve(typeof (IDatabaseFactory));

            _request = factory.BeginRequest(OpenTransaction);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null || (ModelStateErrorChecker && filterContext.Controller.ViewData.ModelState.Values.SelectMany(x => x.Errors).Any(x => !string.IsNullOrEmpty(x.ErrorMessage))))
            {
                _request.Finish(true); 
                return;
            }

            _request.Finish();
        }

        public void OnException(ExceptionContext context)
        {
            if (_request != null)
                _request.Finish(true);

            throw context.Exception;
        }
    }
}
