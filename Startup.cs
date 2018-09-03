using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using historianproductionservice.Data;
using historianproductionservice.Service;
using historianproductionservice.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using securityfilter.Services;
using securityfilter.Services.Interfaces;

namespace historianproductionservice {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddTransient<IOrderService, OrderService> ();
            services.AddTransient<IProductsService, ProductService> ();
            services.AddTransient<IGenealogyService, GenealogyService> ();
            services.AddSingleton<IProductionOrderService, ProductionOrderService> ();           
            services.AddCors (o => o.AddPolicy ("CorsPolicy", builder => {
                builder.AllowAnyOrigin ()
                    .AllowAnyMethod ()
                    .AllowAnyHeader ();
            }));

            services.AddSingleton<IConfiguration> (Configuration);
            services.AddTransient<IEncryptService, EncryptService> ();

            //if (!String.IsNullOrEmpty (Configuration["KeyFolder"]))
                services.AddDataProtection ()
                .SetApplicationName ("Lorien")
                //.PersistKeysToFileSystem (new DirectoryInfo (Configuration["KeyFolder"]));

            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseNpgsql (Configuration.GetConnectionString ("HistorianProductionDB")));
            services.AddMvc ();
        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            app.UseCors ("CorsPolicy");
            app.UseForwardedHeaders (new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseMvc ();
        }
    }
}
