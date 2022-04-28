using System.Reflection;
using WebApi.Services;
using WebApi.Models;
using Newtonsoft.Json.Linq;

Task<bool> loadingData = AsyncLoadData();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "WebApi on .NET 6",
        Description = @"Api that pulls specified data from restcountries.com api and outputs the results to Swagger UI",
        Version = "v1"
    });

    var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
    options.IncludeXmlComments(filePath);
    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    //var configObj = options.ConfigObject;
    //configObj.
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi on .NET 6");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await loadingData;

app.Run();

async Task<bool> AsyncLoadData()
{
    //Singleton HttpClient
    ApiHelper.InitializeClient();

    Task<string> jsonEurContinent = RestcountriesService.AskForJson("region/europe");

    JArray jArrEuropeObject = JArray.Parse(await jsonEurContinent);

    Country[] europeCountries = PeelFromRestcountries
        .ExpectV3Structure.PeelNationsEurope(jArrEuropeObject);

    Country[] eUNations = CountryFilter
        .DropNotEncluded(europeCountries, CountryFilter.MembersOfEUNames);

    eUNations = CountryFilter.OrderByPoppulation(eUNations);

    //Hold countries in Singleton Europe obj
    CountryHolder.Store(eUNations);

    return true;
}
