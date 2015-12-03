using System.Linq;
using System.Web.Mvc;

namespace ACs.NHibernate.Mvc
{
    /// <summary>
    /// Filter for rollback transaction in nhibernate session when not exist exception but exist erros in ModelState values (For ASP.Net MVC only)
    /// </summary>
    public class ForceTransactionRollBackWhenExistErrorInModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Controller.ViewData.ModelState.Values.SelectMany(x => x.Errors).Any(x=>!string.IsNullOrEmpty(x.ErrorMessage)))
                DatabaseFactory.GetRequest().Finish(true);
        }

    }
}
