using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StoreDashboard.Blazor.Application.Common.ExceptionHandlers;
using StoreDashboard.Blazor.Application.Common.FusionCache;
using StoreDashboard.Blazor.Application.Common.PublishStrategies;
using StoreDashboard.Blazor.Application.Common.Security;
using StoreDashboard.Blazor.Application.Pipeline;
using StoreDashboard.Blazor.Application.Pipeline.PreProcessors;

namespace StoreDashboard.Blazor.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(DbExceptionHandler<,,>));
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.NotificationPublisher = new ParallelNoWaitPublisher();
                config.AddRequestPreProcessor(typeof(IRequestPreProcessor<>), typeof(ValidationPreProcessor<>));
                config.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
                config.AddOpenBehavior(typeof(FusionCacheBehaviour<,>));
                config.AddOpenBehavior(typeof(CacheInvalidationBehaviour<,>));

            });
            services.AddScoped<UserProfileStateService>();
            return services;
        }
    public static void InitializeCacheFactory(this IHost host)
    {
            FusionCacheFactory.Configure(host.Services);
        }
}