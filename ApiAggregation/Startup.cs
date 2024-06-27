using ApiAggregation.Models;
using ApiAggregation.Models.News;
using ApiAggregation.Models.Spotify;
using ApiAggregation.Models.Weather;
using ApiAggregation.Services;
using ApiAggregation.Services.Interfaces;

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
            //services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));
            services.AddHttpClient<WeatherService>();
            services.AddHttpClient<NewsService>();
            services.AddHttpClient<SpotifyService>();
            services.AddScoped<IApiService<SpotifyArtistsRequest, SpotifyResponse>, SpotifyService>();
            services.AddScoped<IApiService<WeatherRequest, WeatherResponse>, WeatherService>();
            services.AddScoped<IApiService<NewsRequest, NewsResponse>, NewsService>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}