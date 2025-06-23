using FlexConMan_Monolith.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlexConMan_Monolith;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //builder.Services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        //builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        //    {
        //        options.SignIn.RequireConfirmedAccount = false;
        //    })
        //    .AddEntityFrameworkStores<ApplicationDbContext>();

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

        builder.Services.AddRazorPages(options =>
            options.Conventions.AuthorizeFolder("/"));

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManger = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            string[] roles = { "USER", "ADMIN" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var identityRole = new IdentityRole(role);
                    await roleManager.CreateAsync(identityRole);
                }
            }

            var users = userManger.Users.ToList();
            if (users.Any())
            {
                var firstUser = users.First();
                if (!await userManger.IsInRoleAsync(firstUser, "ADMIN"))
                {
                    await userManger.AddToRoleAsync(firstUser, "ADMIN");
                }
            }
        }

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}