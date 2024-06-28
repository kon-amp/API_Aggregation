using ApiAggregation.Models.Github;
using ApiAggregation.Models.News;
using ApiAggregation.Models.Spotify;
using ApiAggregation.Models.Weather;
using ApiAggregation.Services;
using ApiAggregation.Services.Interfaces;
using Microsoft.OpenApi.Models;

namespace ApiAggregation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();

            // Configure HttpClient for each service
            services.AddHttpClient<WeatherService>();
            services.AddHttpClient<NewsService>();
            services.AddHttpClient<SpotifyService>();
            services.AddHttpClient<GithubService>();

            // Register services with scoped lifetime
            services.AddScoped<IApiService<SpotifyArtistsRequest, SpotifyResponse>, SpotifyService>();
            services.AddScoped<IApiService<WeatherRequest, WeatherResponse>, WeatherService>();
            services.AddScoped<IApiService<NewsRequest, NewsResponse>, NewsService>();
            services.AddScoped<IApiService<GithubRequest, GithubResponse>, GithubService>();
            services.AddScoped<AggregationService>();

            // Add Swagger 
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Fun Page", Version = "v1" });
                c.EnableAnnotations();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Fun Page"));
            }

            app.UseHttpsRedirection();
            //app.UseAuthorization();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}