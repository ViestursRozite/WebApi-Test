using Newtonsoft.Json.Linq;
using WebApi.Models;
using System.Threading;

namespace WebApi.Services
{
    public static class PeelFromRestcountries
    {
        public static class ExpectV3Structure
        {
            /// <summary>
            /// Peels all European nations To Country[]
            /// </summary>
            /// <param name="jArrNationsFromRestcountriesV3">Expects RestcountriesV3.1 structure of JArray object containing countries</param>
            /// <param name="aproxExpectedNations">Number of nations in Europe</param>
            /// <returns></returns>
            public static Country[] PeelNationsEurope(JArray jArrNationsFromRestcountriesV3, int aproxExpectedNations = 50)
            {
                List<Country> listOfEuropeNations = new List<Country>(aproxExpectedNations);

                foreach (JToken jTNation in jArrNationsFromRestcountriesV3)
                {
                    //Country has
                    string name;
                    string nativeName;
                    string[] tld;
                    double area;
                    double poppulation;
                    bool hasEmptyFields = false;

                    //expect in jNation 
                    JToken? jTName, jTNameNative, jTTld, jTArea, jTPoppulation;

                    jTName = jTNation.SelectToken("name.common");
                    jTNameNative = jTNation["name"]["nativeName"].First.First["common"];  /* nation.SelectToken("name.nativeName");*/
                    jTTld = jTNation.SelectToken("tld");
                    jTArea = jTNation.SelectToken("area");
                    jTPoppulation = jTNation.SelectToken("population");

                    if (jTName is null ||
                        jTNameNative is null ||
                        jTTld is null ||
                        jTArea is null ||
                        jTPoppulation is null)
                    {
                        hasEmptyFields = true;
                    }

                    //Empty? Place empty. Otherwise cast.
                    name = jTName is null ? "" : jTName.ToString();
                    nativeName = jTNameNative is null ? "" : jTNameNative.ToString();
                    tld = jTTld is null ? new string[] { } : jTTld.ToObject<string[]>();
                    area = jTArea is null ? -1 : (double)jTArea;
                    poppulation = jTPoppulation is null ? -1 : (double)jTPoppulation;

                    //+1 nation to list
                    listOfEuropeNations.Add(
                        new Country(name, nativeName, tld, area, poppulation, hasEmptyFields));
                }
                return listOfEuropeNations.ToArray<Country>();
            }

            /// <summary>
            /// Extracts a single JToken containing full Country data From Restcountries - JArray obj
            /// </summary>
            /// <param name="filledCountry">Filled Country obj of interest</param>
            /// <param name="jArrNationsFromRestcountriesV3">Full JArray returned from restcountries</param>
            /// <returns></returns>
            public static JToken PeelMatchingJTokenNation(Country filledCountry, JArray jArrNationsFromRestcountriesV3)
            {
                var countryName = filledCountry.Name;

                var countryJson = from jTNation in jArrNationsFromRestcountriesV3
                                    where jTNation.SelectToken("name.common").ToString().Equals(countryName)
                                    select jTNation;

                return countryJson.ToArray<JToken>()[0];//Workaround for explicit cast throwing errors
            }

            /// <summary>
            /// Writes to countries.EmptyField, whatever the linq expression points to
            /// </summary>
            /// <param name="linqExprPointToDesiredField">jToken => jToken.SelectToken("name.common")</param>
            /// <param name="countries"></param>
            /// <param name="jArrNationsFromRestcountriesV3">JArray From Restcountries Api v3</param>
            public static void SelectToEmptyFieldInCountry(
                Func<JToken, JToken> linqExprPointToDesiredField, Country[] countries, 
                JArray jArrNationsFromRestcountriesV3)
            {
                foreach (Country country in countries)
                {
                    JToken jTNation = PeelMatchingJTokenNation(country, jArrNationsFromRestcountriesV3);
                    country.Extras = linqExprPointToDesiredField(jTNation).ToString();
                }
            }
        }
    }
}
