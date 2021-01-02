using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    /// <summary>
    /// Summary description for NewTestHandler
    /// </summary>
    public class NewTestHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}