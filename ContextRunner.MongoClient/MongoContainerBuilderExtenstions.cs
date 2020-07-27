using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ContextRunner.MongoClient
{
    public static class MongoContainerBuilderExtenstions
    {
        public static void ConfigureMongo(this IServiceCollection services, string configName = null)
        {
            services.AddSingleton(sp =>
            {
                MongoDbConfig config = null;

                if (string.IsNullOrEmpty(configName))
                {
                    var configAccesor = sp.GetRequiredService<IOptions<MongoDbConfig>>();
                    config = configAccesor.Value;
                }
                else
                {
                    var configAccesor = sp.GetRequiredService<IOptionsSnapshot<MongoDbConfig>>();
                    config = configAccesor.Get(configName);
                }

                return config;
            });


            services.AddScoped<IMongoDocumentClient, MongoDocumentClient>();
        }
    }
}
