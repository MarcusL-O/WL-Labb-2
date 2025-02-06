using Microsoft.EntityFrameworkCore;
using Wl_labb2.Data;
using Wl_labb2.Models;
using Wl_labb2.Services;

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
            builder.Services.AddSingleton<MongoDbService>();

            var app = builder.Build();

            //Startsida för mitt API
            app.MapGet("/", () => "Välkommen till mitt API, Använd /swagger för API-dokumentation.");

            //CRUD
            var mongoService = app.Services.GetRequiredService<MongoDbService>();

            //Hämta alla snusprodukter
            app.MapGet("/snus", async () =>
            {
                var snusList = await mongoService.GetAllAsync();
                return Results.Ok(snusList);
            });

            //Hämta en specefik snus
            app.MapGet("/snus/{id}", async (string id) =>
            {
                var snus = await mongoService.GetByIdAsync(id);
                if (snus == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(snus);

            });

            //Skapa en ny Snus
            app.MapPost("/snus", async (Snus snus) =>
            {
                await mongoService.CreateAsync(snus);
                return Results.Created($"/snus/{snus.Id}", snus);
            });

            //Uppdatera en snus
            app.MapPut("/snus/{id}", async (string id, Snus updatedSnus) =>
            {
                var existingSnus = await mongoService.GetByIdAsync(id);
                if (existingSnus == null)
                {
                    return Results.NotFound();
                }

                updatedSnus.Id = id;
                await mongoService.UpdateAsync(id, updatedSnus);
                return Results.Ok();
            });

            //Deleta en snus
            app.MapDelete("/snus/{id}", async (string id) =>
            {
                var existingSnus = await mongoService.GetByIdAsync(id);
                if (existingSnus == null)
                {
                    return Results.NotFound();
                }

                await mongoService.DeleteAsync(id);
                return Results.NoContent();

            });

            //tog bort if så att den alltid körs
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.Run();
        }
    }
}
