using System;
using ColaTerminal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var serviceScope = serviceProvider.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<traperto_kurtContext>();

            // auto migration
            context.Database.Migrate();

            // Seed the database.
            InitializeUserAndRoles(context);
        }
    }

    private static void InitializeUserAndRoles(traperto_kurtContext context)
    {
        // init user and roles  
    }
}