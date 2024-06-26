
## Overview
A .NET-based web application built with C#. It serves as an API aggregator, handling data from various APIs either through individual requests to the related API or as a single response from all of them simultaneously. The project leverages JWT for authentication, managing login and token generation. Redis caching is used specifically for the aggregated calls that handle large data sets, calling all available APIs simultaneously with parallelism. Lastly, Swagger UI is implemented for simplicity and provides an interactive way for users to make API calls.

## Functional APIs

The application currently integrates the following functional APIs:

- **Weather API**: Fetches weather data based on geographical coordinates.
- **News API**: Retrieves breaking news headlines for specified country.
- **Countries API**: Provides information about different countries or specific country if it is applied.

APIs planned for future integration include:

- **GitHub API**: To retrieve repositories for specific users.
- **Spotify API**: To gather information about artists.


## Guide to Interact with APIs

### 1) API Keys

For the **Weather** and **News** APIs, an API key is required:

- **Weather API Key**: Obtain from [OpenWeatherMap](https://openweathermap.org/current#call).
- **News API Key**: Obtain from [NewsAPI](https://newsapi.org/docs/).

##### Replace `*********` with your actual API keys at the configuration in `appsettings.json`:

```json
"Weather": {
  "BaseUrl": "https://api.openweathermap.org/data/2.5/weather",
  "ApiKey": "*********",
  "DefaultExclude": "current,minutely",
  "DefaultUnits": "metric",
  "DefaultLang": "en"
},
"NewsApi": {
  "BaseUrl": "https://newsapi.org/v2/top-headlines",
  "ApiKey": "*********",
  "User_Agent": "ApiAggregation-NewsApi",
  "DefaultCountry": "us"
}
```

### 2) **Obtain Credentials**:

- Ensure you have the **username** and **password** defined in `appsettings.json`.

### 3) **Authenticate**:

- Use the credentials to obtain a token by submitting them through the login endpoint, found under the `Auth` category. Follow these steps:
		a) Click the downward arrow on the right side of the `POST` request to reveal the login parameters.
		b) Press the **Try it out** button.
		c) Enter the required **Username** and **Password**.
		d) Finally, click the large blue **Execute** button to submit your credentials.

### 4) Authorize in Swagger UI:

- Click the **Authorize** button in the upper right corner of the page. This will open a pop-up window where you need to enter the token from the previous step under **Value**. Make sure to format it as follows:
```
Bearer <token>
```

## Project Structure

### Key Components

- **Controllers**:
    - **AuthController**: Handles user authentication and JWT token generation.
    - **IntegrationController**: Manages requests to the integrated APIs.
    
- **Services**:
    - **AggregationService**: Orchestrates data retrieval from multiple APIs simultaneously, leveraging parallel processing and caching.
    - **AuthTokenService**: Generates JWT tokens for authenticated users.
    - **RedisCacheService**: Implements caching using Redis.
    
- **Models**:
    - **Requests/Responses**: Define the structure for API requests and responses (e.g., `NewsRequest`, `NewsResponse`).
    - **Enums**: Enumerations used for specifying API parameters (e.g., `NewsApiCountryEnumOption`).


## Authentication

The application uses JWT for authentication. The `AuthController` manages login functionality, where users provide credentials (username and password) stored in `appsettings.json`. Upon successful authentication, a JWT token is issued, allowing access to protected endpoints.

##### Example:

```csharp
[HttpPost("login")]
[AllowAnonymous]
public IActionResult Login([FromQuery] LoginModel login)
{
    if (login.Username == _settings.Username && login.Password == _settings.Password)
    {
        string token = _tokenService.GenerateToken(login.Username);
        return Ok(new { token });
    }

    return Unauthorized();
}
```

## Aggregation Logic
#### Overview

The `AggregationService` is responsible for aggregating data from multiple APIs. It utilizes parallelism and Redis caching to optimize performance and response times.

### Key Methods

- **GetAggregatedDataAsync**: Retrieves data from weather, news, and country APIs simultaneously. Utilizes caching to reduce redundant API calls.

```csharp
public async Task<AggregatedData> GetAggregatedDataAsync(WeatherRequest weatherRequest, NewsRequest newsRequest, CountriesInfoRequest countriesInfoRequest)
{
    string weatherCacheKey = GenerateCacheKey("weather_data", weatherRequest);
    string newsCacheKey = GenerateCacheKey("news_data", newsRequest);
    string countriesInfoCacheKey = GenerateCacheKey("countries_data", countriesInfoRequest);

    Task<WeatherResponse> weatherTask = _cacheService.GetOrCreateAsync(weatherCacheKey, () => _weatherService.GetDataAsync(weatherRequest));
    Task<NewsResponse> newsTask = _cacheService.GetOrCreateAsync(newsCacheKey, () => _newsService.GetDataAsync(newsRequest));
    Task<CountriesInfoResponse> countriesTask = _cacheService.GetOrCreateAsync(countriesInfoCacheKey, () => _countriesService.GetDataAsync(countriesInfoRequest));

    await Task.WhenAll(weatherTask, newsTask, countriesTask);

    return new AggregatedData
    {
        Weather = await weatherTask,
        News = await newsTask,
        CountriesInfo = await countriesTask
    };
}
```

### Caching

Redis caching is implemented through the `RedisCacheService`. It checks if data exists in the cache before making an API call. Cached data is serialized using JSON.

```csharp
public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> fetchData)
{
    IDatabase db = _redisConnection.GetDatabase();
    RedisValue cachedData = await db.StringGetAsync(cacheKey);
    if (!cachedData.IsNullOrEmpty)
    {
        return JsonConvert.DeserializeObject<T>(cachedData);
    }

    T data = await fetchData();
    await db.StringSetAsync(cacheKey, JsonConvert.SerializeObject(data), TimeSpan.FromMinutes(10));
    return data;
}
```

