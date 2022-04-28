using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpGet("/[controller]/euCountries/topTen")]
        [Produces("application/json", new string[] { "application/json" })]
        public Country[] TopTen(string? countBy = null)
        {
            if (countBy == null)
            {
                var countries = CountryHolder.Get();
                countries = CountryFilter.OrderByPoppulation(countries);
                countries = CountryFilter.ShortenArray(countries, 10);
                //return biggest pop

                return countries;
            }
            else if(countBy.Equals("population"))
            {
                var countries = CountryHolder.Get();
                countries = CountryFilter.OrderByPoppulation(countries);
                countries = CountryFilter.ShortenArray(countries, 10);
                //return biggest pop

                return countries;
            }
            else if(countBy.Equals("density"))
            {
                var countries = CountryHolder.Get();
                countries = CountryFilter.OrderByPoppulationDensity(countries);
                countries = CountryFilter.ShortenArray(countries, 10);
                //return biggest pop

                return countries;
            }
            else
            {
                return null;//return error message
            }
        }

        [HttpGet("/[controller]/euCountries/specific")]
        public ReducedNamelessCountry? Nation(string? country = null)
        {
            if (country == null)
            {
                return null; // return how to use error page
            }
            else
            {
                country = country.ToLower();

                //preform search
                ReducedNamelessCountry? result = null;

                foreach (Country nation in CountryHolder.Countries)
                {
                    if (nation.Name.ToLower().Contains(country))
                    {
                        result = new ReducedNamelessCountry(nation.NativeName, 
                            nation.Tld, nation.Area, nation.Poppulation);
                        break;
                    }
                }

                return result is null ? null : result;
            }
        }
    }
}
