using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

class AuthCookie
{
    public const string COOKIE_NAME = "AWESOMECOOKIENAME";

    public static void create(string username)
    {
        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, // version 
                                            username, // user name
                                            DateTime.Now, // create time
                                            DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),// expire time
                                            false, // persistent
                                            "1",
                                            FormsAuthentication.FormsCookiePath); // user data, such as roles

        string encryptedIdentityTicket = FormsAuthentication.Encrypt(ticket);
        var identityCookie = new HttpCookie(COOKIE_NAME, encryptedIdentityTicket);
        identityCookie.Expires = ticket.Expiration;
        HttpContext.Current.Response.Cookies.Add(identityCookie);

        FormsAuthentication.SetAuthCookie(username, false);
    }

    public static string get()
    {
        if (System.Web.HttpContext.Current.Request.Cookies[COOKIE_NAME] != null)
        {
            var cookies = System.Web.HttpContext.Current.Request.Cookies[COOKIE_NAME];
            return FormsAuthentication.Decrypt(cookies.Value).Name;
        }
        return "";
    }

    public static void destroy()
    {
        HttpContext.Current.Response.Cookies[COOKIE_NAME].Expires = DateTime.Now.AddDays(-1);
        HttpContext.Current.Session.Clear();
        HttpContext.Current.Session.Abandon();
        HttpContext.Current.User = null;
        System.Web.Security.FormsAuthentication.SignOut();
    }

}
