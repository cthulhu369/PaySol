using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperPayments.Models;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddEntityFrameworkSqlite().AddDbContext<ApplicationDbContext>();
        // Add your other services here
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // Configure production-specific middleware or error handling here
        }

        // Add HTTPS redirection middleware before other middleware
        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
