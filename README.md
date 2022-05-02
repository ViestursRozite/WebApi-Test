## Task:
1. Create a WebApi .NET 6 application that consumes data from
https://restcountries.com/ API v3+
2. We are only interested in European Union countries, our internal data model only
needs info on the country name, area, population, tld, native name (and any
extra needed).
3. Create REST API for the following:
a. Top 10 countries with the biggest population
b. Top 10 countries with the biggest population density (people / square km)
c. By passing a specific country (eg /country/latvia) return everything you
have on that country in your model except the country name
4. Add unit tests
5. Swagger for the endpoints
Deliverables:
1. Link to a GitHub repository
2. Description of how to run your program

## Result:
- Web application with 2 Api endpoints, can be run as a console app
- Opens a swagger UI where functionality can be seen

1. /[controller]/euCountries/topTen
- can be used as is OR take search parameters
- /Api/euCountries/topTen?countBy=density
- /Api/euCountries/topTen?countBy=population (defaults to this if "?countBy=" is not passed)
- Gives a json array of EU Country objects
 
2. /[controller]/euCountries/specific
- Can only be used with parameters
- Api/euCountries/specific?country={your country name}
- returns first country object where {your country name} case insensitive,  matches a section of country name field
- Api/euCountries/specific?country=eRmaN
- would return Germany country obj

## Specifics / look out for:
1. Unit tests cover 2 classes where new functionality is implemented   
- PeelFromRestcountries
- CountryFilter
2. As specified in task 2nd requirement an unexposed (acessable by editing code) fuction 
- PeelFromRestcountries. ExpectV3Structure. SelectToEmptyFieldInCountry() 
- allows writing anything to Country.extras from restcountries.com returned json 
- By default: extras field is null
3. restcountries.com is queried for data at application startup, data is processed into internal model and everything unused is dropped
- Therefore SelectToEmptyFieldInCountry() cannot be used after the application startup
- This creates an issue as new changes from restcountries can be seen only if app restarts
