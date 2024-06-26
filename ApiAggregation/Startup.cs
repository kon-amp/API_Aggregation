using ApiAggregation.Models;
using ApiAggregation.Services;

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
            services.AddScoped<IApiService<WeatherData>, WeatherService>();
            services.AddScoped<IApiService<NewsData>, NewsService>();
            services.AddScoped<IApiService<SpotifyData>, SpotifyService>();
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