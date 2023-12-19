using InfinityStoreAdmin.Api.Shared.Configurations;
using InfinityStoreAdmin.BlazorApp.Configurations;
using InfinityStoreAdmin.BlazorApp.Data;
using InfinityStoreAdmin.BlazorApp.Services;
using InfinityStoreAdmin.BlazorApp.Shared.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace InfinityStoreAdmin.BlazorApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            //todo: move to separate extension
            builder.Services.AddSingleton<GameService>();
            builder.Services.AddSingleton<IFileUploadService, FileUploadService>();

            // configure app (settings, dependencies, etc)
            IConfigurationSetup appSettingSetup = new AppConfigurationSetup();
            appSettingSetup.ConfigureAll(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
