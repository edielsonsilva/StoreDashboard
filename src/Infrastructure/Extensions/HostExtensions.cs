﻿using Microsoft.Extensions.Hosting;
using StoreDashboard.Blazor.Infrastructure.Persistence;

namespace StoreDashboard.Blazor.Infrastructure.Extensions;

public static class HostExtensions
{
    public static async Task InitializeDatabaseAsync(this IHost host)
    {
            using (var scope = host.Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
                await initializer.InitialiseAsync().ConfigureAwait(false);

                var env = host.Services.GetRequiredService<IHostEnvironment>();
                if (env.IsDevelopment())
                {
                    await initializer.SeedAsync().ConfigureAwait(false);
                }
            }
        }
}