using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

public class BasicAuthentication : ActionFilterAttribute
{
 
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {

        var Username = WebConfigurationManager.AppSettings["username"]; //WEB.CONFIG
        var Password = WebConfigurationManager.AppSettings["password"];
        
        var req = filterContext.HttpContext.Request;
        var auth = req.Headers["Authorization"];
        if (!String.IsNullOrEmpty(auth))
        {
            var cred = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
            var user = new { Name = cred[0], Pass = cred[1] };
            if (user.Name == Username && user.Pass == Password) return;
        }
        filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", String.Format("Basic realm=\"{0}\"", "HotelIglu"));
        filterContext.Result = new HttpUnauthorizedResult();
        var response = HttpContext.Current.Response;
        response.StatusCode = (int)HttpStatusCode.Unauthorized;
        response.End();

        // Prevent the action from actually being executed

        filterContext.Result = new EmptyResult();
        
    }

}