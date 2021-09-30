using evona_hackathon.Services.Auth;
using evona_hackathon.Services.IRepos;
using evona_hackathon.Services.Logging;
using evona_hackathon.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace evona_hackathon.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //communication with database services
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IGameRepo, GameRepo>();
            services.AddTransient<ILogger, Logger>();

            //auth services
            services.AddTransient<IAuthRepo, AuthRepo>();

            return services;
        }
    }
}
