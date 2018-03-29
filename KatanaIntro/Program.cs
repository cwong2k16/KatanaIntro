using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Owin;

namespace KatanaIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:8080";
            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("Started!");
                Console.ReadKey();
                Console.WriteLine("Stopped.");
            }
        }
    }

    public class Startup
    {
        /* IAppBuilder is very important. */
        /* It helps determine how this app is going to behave and respond to ... */
        /* ... Http requests */
        /* Adding additional packages/katana components, you will see more ... */
        /* ... more methods that app will have */
        public void Configuration(IAppBuilder app)
        {
            app.Run(ctx =>
            {
                return ctx.Response.WriteAsync("Hello World"); 
            });
        }
    }
}
