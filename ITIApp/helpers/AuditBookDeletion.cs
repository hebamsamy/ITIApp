using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Text;

namespace ITIApp
{
    public class AuditBookDeletion :ActionFilterAttribute
    {
        private readonly string path = Directory.GetCurrentDirectory() 
            + "/Logging/" + DateTime.Today.ToString("yy-MM-dd") +".txt";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"On {DateTime.Now.ToString()}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append($"UserID {context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)} ");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append($"fullName {context.HttpContext.User.FindFirstValue("fullName")} ");
            stringBuilder.Append(Environment.NewLine);


            File.AppendAllText(path, stringBuilder.ToString());
        }
        
    }
}
