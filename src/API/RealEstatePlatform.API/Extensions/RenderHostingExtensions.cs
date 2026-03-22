using Microsoft.Extensions.Configuration;

namespace RealEstatePlatform.API.Extensions;

public static class RenderHostingExtensions
{
    public static string GetRedisConfiguration(this IConfiguration configuration)
    {
        var explicitRedis = configuration.GetConnectionString("Redis");
        if (!string.IsNullOrWhiteSpace(explicitRedis))
        {
            return explicitRedis;
        }

        var redisHost = configuration["REDIS_HOST"];
        var redisPort = configuration["REDIS_PORT"];

        if (!string.IsNullOrWhiteSpace(redisHost) && !string.IsNullOrWhiteSpace(redisPort))
        {
            return $"{redisHost}:{redisPort}";
        }

        return "localhost:6379";
    }

    public static int GetPort(this IConfiguration configuration)
    {
        var portValue = configuration["PORT"];
        return int.TryParse(portValue, out var port) ? port : 10000;
    }
}
