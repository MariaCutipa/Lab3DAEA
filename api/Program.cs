using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add HttpClient and GifService
builder.Services.AddHttpClient<IGifService, GifService>();
builder.Services.AddTransient<IGifService, GifService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.MapGet("/gifs/{searchTerm}", async (string searchTerm, IGifService gifService) =>
{
    var gifs = await gifService.GetGifsAsync(searchTerm);
    return Results.Ok(gifs);
})
.WithName("GetGifs")
.WithOpenApi();

app.Run();

// Define the service and controller logic here
public interface IGifService
{
    Task<IEnumerable<Gif>> GetGifsAsync(string searchTerm);
}

public class GifService : IGifService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "LIVDSRZULELA"; // Your Tenor API Key
    private const int Limit = 8; // Number of results

    public GifService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Gif>> GetGifsAsync(string searchTerm)
    {
        var response = await _httpClient.GetStringAsync(
            $"https://g.tenor.com/v1/search?q={searchTerm}&key={ApiKey}&limit={Limit}");
        var data = JObject.Parse(response);

        var gifs = new List<Gif>();

        foreach (var result in data["results"])
        {
            var mediaArray = result["media"] as JArray;
            var gifUrl = mediaArray?
                .Select(media => media["gif"]?["url"]?.ToString())
                .FirstOrDefault(url => !string.IsNullOrEmpty(url));

            var gif = new Gif
            {
                Id = result["id"]?.ToString(),
                Url = result["url"]?.ToString(),
                WebpUrl = gifUrl
            };

            gifs.Add(gif);
        }

        return gifs;
    }

}

// Define the Gif model
public class Gif
{
    public string Id { get; set; }
    public string Url { get; set; }
    public string WebpUrl { get; set; }
}
