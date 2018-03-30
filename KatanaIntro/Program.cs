using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;

namespace KatanaIntro
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        string uri = "http://localhost:8080";
    //        using (WebApp.Start<Startup>(uri))
    //        {
    //            Console.WriteLine("Started!");
    //            Console.ReadKey();
    //            Console.WriteLine("Stopped.");
    //        }
    //    }
    //}

    public class Startup
    {
        /* IAppBuilder is very important. */
        /* It helps determine how this app is going to behave and respond to ... */
        /* ... Http requests */
        /* Adding additional packages/katana components, you will see more ... */
        /* ... more methods that app will have */
        public void Configuration(IAppBuilder app)
        {
            //app.Use<HelloWorldComponent>();
            //SetUpServer(app);
            //app.Use(async(environment, next) =>
            //{
            //    foreach (var pair in environment.Environment)
            //    {
            //        Console.WriteLine("{0}:{1}", pair.Key, pair.Value);
            //    }
                

            //    await next();
            //});
            app.Use(async (environment, next) =>
            {
                Console.WriteLine("requesting : " + environment.Request.Path);
                await next();
                Console.WriteLine("response : " + environment.Response.StatusCode);
            });
            ConfigureWebApi(app);
            app.UseHelloWorld();
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultApi","api/{controller}/{id}", new { id = RouteParameter.Optional});
            app.UseWebApi(config);
        }

        private static void SetUpServer(IAppBuilder app)
        {
            app.UseWelcomePage();
            app.Run(ctx => { return ctx.Response.WriteAsync("Hello World"); });
        }
    }

    public static class AppBuilderExtensions
    {
        public static void UseHelloWorld(this IAppBuilder app)
        {
            app.Use<HelloWorldComponent>();
        }
    }

    public class HelloWorldComponent
    {
        AppFunc _next;
        public HelloWorldComponent(AppFunc next)
        {
            _next = next;
        }
        public Task Invoke(IDictionary<string, object> environment)
        {
            var response = environment["owin.ResponseBody"] as Stream;
            using (var writer = new StreamWriter(response))
            {
                return writer.WriteAsync("Hello!!");
            }
        }
    }
}
