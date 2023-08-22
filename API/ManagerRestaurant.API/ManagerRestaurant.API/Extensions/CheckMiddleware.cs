using auth.Helpers;
using Infratructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks; 
namespace ManagerRestaurant.API.Extensions
{
    public class CheckMiddleware
    {
        private RequestDelegate _next;
        private JwtService _jwtService = new JwtService(); 
         
        public CheckMiddleware(DataContext context,RequestDelegate next)
        {
            _next = next; 
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            //StringValues tokent;
            //httpContext.Request.Headers.TryGetValue("token", out tokent);

            //if (tokent.Count > 0)
            //{ 
            //    var user = HttpContext.Session.GetString(tokent[0]);
            //    if (user != "")
            //    {
            //        await _next(httpContext);
            //    }
            //}
 
            await _next(httpContext);
        }
    }
}
