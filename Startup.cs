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
        public void ConfigureServices(IServiceCollection services) {}

        private static void carList(IApplicationBuilder app, List<Car> theCars)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Strona carList "+ "\n");
                foreach (Car theCar in theCars)
                {
                    await context.Response.WriteAsync(theCar.Name + "  " + theCar.Color + "\n");
                }

            });
        }

        //adding new car with a name and color
        private  static void addNewCar(IApplicationBuilder app)
        {
            List<Car> theCars = new List<Car> { };


            app.Run(async context =>
            {
                var nameOfNewCar = context.Request.Query["name"];
                var colorOfNewCar = context.Request.Query["color"];

                if (nameOfNewCar != "" | colorOfNewCar != "") //moja walidacja nie dzia³a :(
                { 
                    theCars.Add(new Car() { Name = nameOfNewCar, Color = colorOfNewCar });
                }
                await context.Response.WriteAsync("strona dodawania cars" + "\n");

                foreach (Car theCar in theCars)
                {                    
                    await context.Response.WriteAsync(theCar.Name + "  " + theCar.Color + "\n");
                }
                 context.Response.Redirect("/CarList"); //nie dziala

            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


           // List<Car> theCars = new List<Car> { };
           // app.Map("/addNewcar", addNewCar(app, theCars);

            app.Map("/addNewcar", addNewCar);
            
            
            app.Run(async context =>
            {                
                await context.Response.WriteAsync("cos");
            });







        }

        
    }
    }

