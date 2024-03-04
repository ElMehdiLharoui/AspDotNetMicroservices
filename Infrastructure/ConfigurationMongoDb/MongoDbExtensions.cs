using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class MongoDbExtensions
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbConfig = new MongoDbConfiguration
            {
                ConnectionString = configuration.GetConnectionString("MongoDbAtlas"),
                DatabaseName = "VotreNomDeBaseDeDonnees" // Remplacez par le nom de votre base de données
            };

            services.AddSingleton(sp => new MongoClient(mongoDbConfig.ConnectionString));
            services.AddScoped(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDbConfig.DatabaseName));
        }
    }
}
