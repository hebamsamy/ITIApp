using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ITIApp
{
    public class GenaralExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //send Error
            StringBuilder sb = new StringBuilder();
            sb.Append($" Error : {context.Exception.Message}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($" Details : {context.Exception.StackTrace}");

            MailHelper mail = new MailHelper(
                "Admin@company.com",
                "ITI App Exception",
                sb.ToString());
            mail.Send();

            //return View (error)
            context.ExceptionHandled = true;
            context.Result = new ViewResult()
            {
                ViewName = "Error",
            };
        }
    }
}
