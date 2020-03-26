using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarList
{
    public class Car
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<List<Car>>();
        }

        private static void carList(IApplicationBuilder app)
        {

            app.Run(async context =>
            {

                var theCars = context.RequestServices.GetService<List<Car>>();
                await context.Response.WriteAsync("Strona carList " + "\n");
                foreach (Car theCar in theCars)
                {
                    await context.Response.WriteAsync(theCar.Name + "  " + theCar.Color + "\n");
                }

            });
        }

        //adding new car with a name and color
        private static void addNewCar(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var theCars = context.RequestServices.GetService<List<Car>>();
                var nameOfNewCar = context.Request.Query["name"].ToString();
                var colorOfNewCar = context.Request.Query["color"].ToString();
                
                if (nameOfNewCar != "" || colorOfNewCar != "") 
                {
                    theCars.Add(new Car() { Name = nameOfNewCar, Color = colorOfNewCar });
                }
              
                context.Response.Redirect("/CarList"); 

            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


           
            app.MapWhen(app => app.Request.Path.StartsWithSegments("/addNewCar") & app.Request.Query.ContainsKey("name"), addNewCar);
          
            app.Map("/CarList", carList);

           
            app.Run(async context =>
            {
                await context.Response.WriteAsync("cos");
            });







        }


    }
}

