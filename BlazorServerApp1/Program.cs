using BlazorServerApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;


namespace BlazorServerApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<BooksRepository>(sp => new BooksRepository("Server=localhost;Port=3306;Database=Library;User=root;Password=Pennylane7$;"));
            builder.Services.AddScoped<AuthorsRepository>(sp => new AuthorsRepository("Server=localhost;Port=3306;Database=Library;User=root;Password=Pennylane7$;"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}