#Useful classes

This is a small repository where I will upload pieces of code useful for me and my daily work.

##BasicAuthentication.cs

Class made with C# and useful for a MVC .NET C# Project. Use it when you need Basic Authentication in your application. 

HTTP Basic authentication (BA) implementation is the simplest technique for enforcing access controls to web resources because it doesn't require cookies, session identifiers, or login pages; rather, HTTP Basic authentication uses standard fields in the HTTP header, obviating the need for handshakes. [+info](https://en.wikipedia.org/wiki/Basic_access_authentication)

- How to use it? 

1. Import the class into your project 
2. Add these lines on your Web.config:

```xml
  <appSettings>    
    <add key="api_username" value="your-username"/>
    <add key="api_password" value="your-password"/>
    ...    
  </appSettings>
```

3. Add [BasicAuthenticationAttribute] before your function.

```c#
[BasicAuthenticationAttribute]
[HttpGet]
public JsonResult Ping()
{
    return Json(new { IsSuccess = true }, JsonRequestBehavior.AllowGet);
}
```

##ReCaptchaClass.cs

Class made with C# and useful for a MVC .NET C# Project. Use it when you need to use Google Recaptcha in your application. 

[+info Recaptcha](https://www.google.com/recaptcha/intro/index.html)

- How to use it? 

1. Import the class into your project 
2. Add these lines on your Web.config:

```xml
  <appSettings>    
    <add key="recaptcha_privatekey" value="your-privatekey"/>
    <add key="recaptcha_publickey" value="your-publickey"/>
    ...    
  </appSettings>
```
3. Add these lines on your Controller function

```c#
[HttpPost]
public ActionResult Dummy(FormCollection form)
    string EncodedResponse = Request.Form["g-Recaptcha-Response"];
    bool IsCaptchaValid = (ReCaptchaClass.Validate(EncodedResponse) == "True" ? true : false);

    if (IsCaptchaValid)
    {
        //Recaptcha is valid
    }
}
```
4. Add these line in your view

```html
<script src='https://www.google.com/recaptcha/api.js'></script>
<div class="g-recaptcha" data-sitekey="@WebConfigurationManager.AppSettings["recaptcha_publickey"]"></div>
```

## Licenses

Distributed under the [MIT](http://en.wikipedia.org/wiki/MIT_License)

###MIT - Licence

Copyright (c) 2017 Eric Risco de la Torre

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.