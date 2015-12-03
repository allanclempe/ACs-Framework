using Microsoft.AspNet.Mvc.Filters;
using ACs.NHibernate.Generic;


namespace ACs.NHibernate.Next
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

            var factory = (IDatabaseSession)filterContext.HttpContext.ApplicationServices.GetService(typeof (IDatabaseSession));

            _request = factory.BeginRequest(OpenTransaction);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null || (ModelStateErrorChecker && filterContext.ModelState.ErrorCount > 0))
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
