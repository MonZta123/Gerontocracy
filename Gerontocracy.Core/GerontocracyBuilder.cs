﻿using AutoMapper;

using Gerontocracy.Core.Config;
using Gerontocracy.Core.Exceptions;
using Gerontocracy.Core.Exceptions.Account;
using Gerontocracy.Core.Exceptions.Affair;
using Gerontocracy.Core.Exceptions.Board;
using Gerontocracy.Core.Exceptions.News;
using Gerontocracy.Core.Exceptions.Party;
using Gerontocracy.Core.Exceptions.User;
using Gerontocracy.Core.HostedServices;
using Gerontocracy.Core.Middlewares;
using Gerontocracy.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Morphius;

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Gerontocracy.Core
{
    public static class GerontocracyBuilder
    {
        #region Methods

        private static GerontocracySettings _settings;

        public static IServiceCollection AddGerontocracy(this IServiceCollection services, Action<GerontocracyOptions> action)
        {
            var config = new GerontocracyOptions();

            action(config);
            _settings = config.GerontocracyConfig;
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
            services.AddTransient<Interfaces.ITaskService, Providers.TaskService>();

            // ===== Add Scopeds =====
            services.AddScoped<Interfaces.ISyncService, Providers.SyncService>();

            // ===== Add Singletons =====
            services.AddSingleton<Interfaces.IMailService, Providers.MailService>();
            services.AddSingleton<SendGrid.ISendGridClient>(n =>
                new SendGrid.SendGridClient(new SendGrid.SendGridClientOptions() { ApiKey = config.GerontocracyConfig.SendGridApiKey }));
            services.AddSingleton<ContextFactory>();
            services.AddSingleton<ImporterRepository>();
            services.AddSingleton(config.GerontocracyConfig);

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
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                }
            );

            // ==== Add Hosted Services =====
            if (config.GerontocracyConfig.SyncActive)
                services.AddHostedService<SyncHostedService>();
            
            return services;
        }

        public static MorphiusOptions GetGerontocracyEntries(this MorphiusOptions cfg)
        {
            return cfg
                .AddException<EmailAlreadyConfirmedException>(HttpStatusCode.OK)
                .AddException<AccountAlreadyBannedException>(HttpStatusCode.OK)
                .AddException<AccountIsBannedException>(HttpStatusCode.OK)
                .AddException<AccountNotBannedException>(HttpStatusCode.OK)
                .AddException<AccountCannotBeBannedException>(HttpStatusCode.OK)
                .AddException<CannotChangeAdminPermissionException>(HttpStatusCode.OK)
                .AddException<CredentialException>(HttpStatusCode.OK)
                .AddException<AffairAlreadyAttachedToNewsException>(HttpStatusCode.OK)
                .AddException<EmailNotConfirmedException>(HttpStatusCode.OK)
                .AddException<AccountNotFoundException>(HttpStatusCode.NotFound)
                .AddException<PoliticianNotFoundException>(HttpStatusCode.NotFound)
                .AddException<AffairNotFoundException>(HttpStatusCode.NotFound)
                .AddException<PartyNotFoundException>(HttpStatusCode.NotFound)
                .AddException<ThreadNotFoundException>(HttpStatusCode.NotFound)
                .AddException<PostNotFoundException>(HttpStatusCode.NotFound)
                .AddException<NewsNotFoundException>(HttpStatusCode.NotFound)
                .AddException<UserNotFoundException>(HttpStatusCode.NotFound)
                .AddException<TaskNotFoundException>(HttpStatusCode.NotFound);
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
            
            app.EnsureSeed();

            app.UseMiddleware<UserDestroyerMiddleware>();

            return app;
        }

        #endregion Methods
    }
}