using Microsoft.EntityFrameworkCore;
using Wl_labb2.Data;
using Wl_labb2.Models;

namespace Wl_labb2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));


            var app = builder.Build();

            //CRUD

            //Hämta alla snusprodukter
            app.MapGet("/snus", async (AppDbContext db) =>
            await db.SnusItems.ToListAsync());

            //Hämta en specefik snus
            app.MapGet("/snus/{id}", async (int id, AppDbContext db) =>
            {
                var snus = await db.SnusItems.FindAsync(id);

                if (snus == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(snus);
            });

            //Skapa en specefik Snus
            app.MapPost("/snus", async (Snus snus, AppDbContext db) =>
            {
                db.SnusItems.Add(snus);
                await db.SaveChangesAsync();
                return Results.Created($"/snus/{snus.Id}", snus);
            });

            //Uppdatera en snus
            app.MapPut("/snus/{id}", async (int id, Snus updatedSnus, AppDbContext db) =>
            {
                var snus = await db.SnusItems.FindAsync(id);

                if (snus == null)
                {
                    return Results.NotFound();
                }

                snus.Name = updatedSnus.Name;
                snus.Description = updatedSnus.Description;
                snus.Type = updatedSnus.Type;

                await db.SaveChangesAsync();
                return Results.NoContent(); 
            });

            //Deleta en snus
            app.MapDelete("/snus/{id}", async (int id, AppDbContext db) =>
            {
                var snus = await db.SnusItems.FindAsync(id);

                if (snus == null)
                {
                    return Results.NotFound();
                }

                db.SnusItems.Remove(snus);
                await db.SaveChangesAsync();
                return Results.NoContent();

            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.Run();
        }
    }
}
