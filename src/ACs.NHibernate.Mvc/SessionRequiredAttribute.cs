using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ACs.NHibernate.Generic;

namespace ACs.NHibernate.Mvc
{
    
    public class SessionRequiredAttribute : ActionFilterAttribute, IExceptionFilter
    {
        private readonly bool _openTransaction;
        private readonly bool _verifyModelStateError;
        private IDatabaseRequest _request;

        public SessionRequiredAttribute(bool openTransaction = true, bool verifyModelStateError = true)
        {
            _openTransaction = openTransaction;
            _verifyModelStateError = verifyModelStateError;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var factory = (IDatabaseSession)UnityContainerHelper.Container.Resolve(typeof (IDatabaseSession));

            _request = factory.BeginRequest(_openTransaction);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null || (_verifyModelStateError && filterContext.Controller.ViewData.ModelState.Values.SelectMany(x => x.Errors).Any(x => !string.IsNullOrEmpty(x.ErrorMessage))))
            {
                _request.Finish(true); 
                return;
            }

            _request.Finish();
        }

        public void OnException(ExceptionContext context)
        {
            _request.Finish(true);

            throw context.Exception;
        }
    }
}
