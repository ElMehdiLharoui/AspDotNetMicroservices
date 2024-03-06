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
            var mongoDbConfig = configuration.GetSection("ConnectionStrings").Get<MongoDbConfiguration>();

            services.AddSingleton(sp => new MongoClient(mongoDbConfig.ConnectionString));
            services.AddScoped(sp => sp.GetRequiredService<MongoClient>().GetDatabase(mongoDbConfig.DatabaseName));
        }
    }
}
