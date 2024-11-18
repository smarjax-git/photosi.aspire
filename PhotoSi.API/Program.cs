
using System.Net.Http;

using PhotoSi.API.WS.Ordini;

using PhotoSi.API.Users;
using PhotoSi.API.WS.Prodotti;
namespace PhotoSi.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();

        builder.Services.AddScoped<OrdiniWS>(s => new OrdiniWS(
                                                        builder.Configuration.GetValue<string>("WS:OrdiniWS"),
                                                        new HttpClient()));

        builder.Services.AddScoped<UsersWS>(s => new UsersWS(
                                                builder.Configuration.GetValue<string>("WS:UsersWS"),
                                                new HttpClient()));

        builder.Services.AddScoped<ProdottiWS>(s => new ProdottiWS(
                                        builder.Configuration.GetValue<string>("WS:ProdottiWS"),
                                        new HttpClient()));

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
