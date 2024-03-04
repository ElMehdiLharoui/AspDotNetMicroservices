using Application.Students.Handler;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Students.Options;
namespace Students
{
    public class Startup
    {
        private  IConfiguration Configuration;
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }
        // IOC
        public  void ConfigureServices(IServiceCollection services)
        {
            
              // services.ConfigureOptions<ConfigureSwagerOptions>();
            // services.AddMediatR(typeof(GetStudentByIdQueryHandler).GetTypeInfo().Assembly);
            // services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddConfigureHandler();

            services.AddMongoDb(Configuration);

            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            Console.WriteLine("Le gestionnaire de requête est dans les assemblies chargées.");
            services.AddLogging(configure =>
            {
                configure.AddConsole(); // Ajoute la sortie console
            });
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSingleton(Configuration);
            services.AddControllers();
            services.AddSwaggerGen(
           /*   c =>
                {
                    c.SwaggerDoc(
                        "V1",
                        new OpenApiInfo
                            {
                            Title = "mon titre",
                            Version = "v1",

                        });
                    c.CustomOperationIds(
                        api =>
                        {
                            var  description = api.ActionDescriptor as ControllerActionDescriptor;// convertire tout les paramtere qui existe dans le controller
                            return $"{description?.ControllerName}_{description?.ActionName}"; // formter une chaine de caractere en c# le svariable avec les chaines 

                        });
                    c.CustomSchemaIds
                }*/
                );
            CheckAssemblyTypes();

        }
        // Midlwears
        public  void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints=> {
                endpoints.MapControllers();
                });
            app.UseSwagger();
            app.UseSwaggerUI(op =>
            {
            });
            CheckAssemblyTypes();
        }
        private void CheckAssemblyTypes()
        {
            // Obtenez l'assembly de Startup
            var startupAssembly = typeof(Startup).Assembly;

            // Obtenez toutes les assemblies chargées dans le domaine actuel
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Obtenez l'assembly de GetStudentByIdQueryHandler
            var handlerAssembly = typeof(Application.Students.Handler.GetStudentByIdQueryHandler).Assembly;

            // Vérifiez si l'assembly du gestionnaire est parmi les assemblies chargées
            var handlerAssemblyLoaded = loadedAssemblies.Any(loadedAssembly => loadedAssembly == handlerAssembly);

            // Ajoutez un log explicite pour voir s'il est affiché dans la console
            if (handlerAssemblyLoaded)
            {
                Console.WriteLine("Le gestionnaire de requête est dans les assemblies chargées.");
                // Ajoutez un log explicite
               _logger.LogInformation("Le gestionnaire de requête est dans les assemblies chargéeshfhgfhfhgfhg.");
            }
            else
            {
                Console.WriteLine("Le gestionnaire de requête n'est pas dans les assemblies chargées.");
                // Ajoutez un log explicite
                 _logger.LogWarning("Le gestionnaire de requête n'est pas dans les assemblies chargéeshghghghghg.");
            }
        }
    }


}