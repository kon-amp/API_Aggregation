using ApiAggregation.Models.AppSettings;
using ApiAggregation.Models.CountriesInfo;
using ApiAggregation.Models.Github;
using ApiAggregation.Models.News;
using ApiAggregation.Models.Spotify;
using ApiAggregation.Models.Weather;
using ApiAggregation.Services;
using ApiAggregation.Services.Integrations;
using ApiAggregation.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Win32;
using StackExchange.Redis;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

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
            // Bind configuration settings
            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Issuer",
                    ValidAudience = "Audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kon_amp_assessment_interview_2024"))
                };
            });

            // Configuration for Redis
            services.AddSingleton<IConnectionMultiplexer>(
                    ConnectionMultiplexer.Connect("localhost,abortConnect=false")
            );
            services.AddScoped<ICacheService, RedisCacheService>();

            // Add services to the container.
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            });
          
            // Register HTTP client services
            services.AddHttpClient<IApiService<WeatherRequest, WeatherResponse>, WeatherService>();
            services.AddHttpClient<IApiService<NewsRequest, NewsResponse>, NewsService>();
            services.AddHttpClient<IApiService<SpotifyArtistsRequest, SpotifyResponse>, SpotifyService>();
            services.AddHttpClient<IApiService<GithubRequest, GithubResponse>, GithubService>();
            services.AddHttpClient<IApiService<CountriesInfoRequest, CountriesInfoResponse>, CountriesService>();

            // Register services with scoped lifetime
            services.AddScoped<IApiService<SpotifyArtistsRequest, SpotifyResponse>, SpotifyService>();
            services.AddScoped<IApiService<WeatherRequest, WeatherResponse>, WeatherService>();
            services.AddScoped<IApiService<NewsRequest, NewsResponse>, NewsService>();
            services.AddScoped<IApiService<GithubRequest, GithubResponse>, GithubService>();
            services.AddScoped<IApiService<CountriesInfoRequest, CountriesInfoResponse>, CountriesService>();

            services.AddScoped<IAggregationService, AggregationService>();
            services.AddSingleton<AuthTokenService>(); 

            // Add Swagger 
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Fun Page", Version = "v1" });
                c.EnableAnnotations();
                c.UseInlineDefinitionsForEnums();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please enter your token with Bearer prefix (ex. Bearer token))",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });


                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}