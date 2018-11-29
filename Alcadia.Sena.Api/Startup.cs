using Alcadia.Sena.Api.Binders;
using Alcadia.Sena.Api.Constraints;
using Alcadia.Sena.Api.Extensions;
using Alcadia.Sena.Api.Filters;
using Alcadia.Sena.Api.Handlers;
using Alcadia.Sena.Api.Selectors;
using LightInject;
using LightInject.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Alcadia.Sena.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public IHostingEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddJsonFormatters()
                .AddAuthorization();

            //Register Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Isain API", Version = "v1" });
            });

            services.Configure<MvcOptions>(options =>
            {
                //Enforcing SSL
                options.Filters.Add(new RequireHttpsAttribute());
                options.Filters.Add(new ValidateModelAttribute());
            });

            //register mvc
            services.AddMvc(config =>
            {              
                config.ModelBinderProviders.Insert(0, new OrderByBinderProvider());
            })
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opt.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            })
            .AddControllersAsServices();

            //Register settings
            services.RegisterSettings(Configuration);

            //Add application insights telemetry
            services.AddApplicationInsightsTelemetry();

            //Constraints
            services.Configure<RouteOptions>(options => options.ConstraintMap.Add("email", typeof(EmailConstraint)));

            //Add cors
            services.AddCors();

            var container = new ServiceContainer()
            {
                ScopeManagerProvider = new PerLogicalCallContextScopeManagerProvider(),
                PropertyDependencySelector = new PropertyInjectionDisabler(),
            };

            container.RegisterInstance(Configuration);
            //container.RegisterAspects();
            return container.CreateServiceProvider(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Add exception handling
            app.UseExceptionHandler(new ExceptionHandlerOptions { ExceptionHandler = new JsonExceptionHandler().Invoke });
            //MiddleWare relyes on forwarded headers
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //Enforcing SSL
            var reqriteOptions = new RewriteOptions().AddRedirectToHttps();
            app.UseRewriter(reqriteOptions);

            //Swagger routing
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Isain API V1");
            });

            //Enable CORs
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
                builder.WithExposedHeaders("WWW-Authenticate", "X-Pagination");
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
