﻿using ActionCommandGame.Api.Installers.Abstractions;
using ActionCommandGame.Repository;
using Microsoft.EntityFrameworkCore;

namespace ActionCommandGame.Api.Installers;

public class DbInstaller : IInstaller
{
    private static readonly ILoggerFactory ConsoleLoggerFactory
        = LoggerFactory.Create(builder => { builder.AddConsole(); });

    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ActionCommandGameDbContext>(options =>
        {
            options.UseLoggerFactory(ConsoleLoggerFactory);
            options.EnableSensitiveDataLogging();
            options.UseSqlServer(
                @"server = LAPTOP-ID44330L\VIVES; database=RickAndMortyActionCommandGame; Trusted_Connection=True; TrustServerCertificate=True; Encrypt=False",
                b => b.MigrationsAssembly("ActionCommandGame.Api"));
        });
    }
}