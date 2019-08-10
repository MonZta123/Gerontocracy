using AutoMapper;
using Gerontocracy.Core.Exceptions;
using Gerontocracy.Core.Exceptions.Account;
using Gerontocracy.Core.Exceptions.Affair;
using Gerontocracy.Core.Exceptions.Party;
using Gerontocracy.Core.Exceptions.User;
using Gerontocracy.Data;
using Morphius;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net;
using Gerontocracy.Core.Config;
using Gerontocracy.Core.Exceptions.Board;
using Gerontocracy.Core.Exceptions.News;
using Gerontocracy.Core.HostedServices;

namespace Gerontocracy.Core
{
    public static class GerontocracyBuilder
    {
        public static IServiceCollection AddGerontocracy(this IServiceCollection services, Action<GerontocracyOptions> action)
        {
            var config = new GerontocracyOptions();

            action(config);

            // ===== Null Checks =====
            if (string.IsNullOrEmpty(config.ConnectionString))
                throw new StartupException($"{nameof(config.ConnectionString)} not set!");

            if (config.GerontocracyConfig == null)
                throw new StartupException($"{nameof(config.GerontocracyConfig)} not set!");

            // ===== Add Automapper =====
            services.AddAutoMapper();

            // ===== Add Transients =====
            services.AddTransient<Interfaces.IAccountService, Providers.AccountService>();
            services.AddTransient<Interfaces.IPartyService, Providers.PartyService>();
            services.AddTransient<Interfaces.IAffairService, Providers.AffairService>();
            services.AddTransient<Interfaces.IBoardService, Providers.BoardService>();
            services.AddTransient<Interfaces.INewsService, Providers.NewsService>();
            services.AddTransient<Interfaces.IUserService, Providers.UserService>();

            // ===== Add Scopeds =====
            services.AddScoped<Interfaces.ISyncService, Providers.SyncService>();

            // ===== Add Singletons =====
            services.AddSingleton<Interfaces.IMailService, Providers.MailService>();
            services.AddSingleton<SendGrid.ISendGridClient>(n =>
                new SendGrid.SendGridClient(new SendGrid.SendGridClientOptions() { ApiKey = config.GerontocracyConfig.SendGridApiKey }));
            services.AddSingleton<ContextFactory>();
            services.AddSingleton<GerontocracySettings>(config.GerontocracyConfig);

            // ===== Add Entity Framework =====
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<GerontocracyContext>(options => options.UseNpgsql(config.ConnectionString));

            // ===== Add Identity =====
            services.AddIdentity<Data.Entities.Account.User, Data.Entities.Account.Role>()
                .AddEntityFrameworkStores<GerontocracyContext>()
                .AddDefaultTokenProviders();

            // ===== Add HttpClient =====
            services.AddHttpClient();

            // ===== Configure IdentityOptions =====
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                options.User.RequireUniqueEmail = false;
            });

            // ==== Add Hosted Services =====
            if (config.GerontocracyConfig.SyncActive)
                services.AddHostedService<SyncHostedService>();

            return services;
        }

        public static IApplicationBuilder UseGerontocracy(this IApplicationBuilder app)
        {
            app.Use(async (httpContext, next) =>
            {
                await next();
                if (httpContext.Response.StatusCode == 404 &&
                    !Path.HasExtension(httpContext.Request.Path.Value) &&
                    !httpContext.Request.Path.Value.StartsWith("/api/") &&
                    !httpContext.Request.Path.Value.StartsWith("/swagger/"))
                {
                    httpContext.Request.Path = "/";
                    await next();
                }
            });

            return app;
        }

        public static MorphiusOptions GetGerontocracyEntries(this MorphiusOptions cfg)
        {
            return cfg
                .AddException<EmailAlreadyConfirmedException>(HttpStatusCode.BadRequest)
                .AddException<AccountNotFoundException>(HttpStatusCode.BadRequest)
                .AddException<CredentialException>(HttpStatusCode.BadRequest)
                .AddException<EmailNotConfirmedException>(HttpStatusCode.BadRequest)
                .AddException<PoliticianNotFoundException>(HttpStatusCode.NotFound)
                .AddException<AffairNotFoundException>(HttpStatusCode.NotFound)
                .AddException<PartyNotFoundException>(HttpStatusCode.NotFound)
                .AddException<ThreadNotFoundException>(HttpStatusCode.NotFound)
                .AddException<PostNotFoundException>(HttpStatusCode.NotFound)
                .AddException<NewsNotFoundException>(HttpStatusCode.NotFound)
                .AddException<AffairAlreadyAttachedToNewsException>(HttpStatusCode.BadRequest)
                .AddException<UserNotFoundException>(HttpStatusCode.NotFound);
        }
    }
}
