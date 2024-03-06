using Application.Common;
using Application.Common.Mappings;
using Application.Students.Handler.Querys;
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
            services.AddInfrastructure();
            
            services.AddMongoDb(Configuration);

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddLogging(configure =>
            {
                configure.AddConsole(); // Ajoute la sortie console
            });

         
            services.AddEndpointsApiExplorer();
            services.AddSingleton(Configuration);
            services.AddControllers();
            services.AddSwaggerGen();
            CheckAssemblyTypes();

        }
        // Midlwears
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
;
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowOrigin");

            app.UseEndpoints(endpoints=> {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(op =>
            {
                op.SwaggerEndpoint("/swagger/v1/swagger.json", "STD API");
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
            var handlerAssembly = typeof(GetStudentByIdQueryHandler).Assembly;

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