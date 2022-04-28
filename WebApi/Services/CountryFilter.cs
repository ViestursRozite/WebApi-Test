using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using System.Linq;

namespace WebApi.Services
{
    public static class CountryFilter
    {
        public static readonly string[] MembersOfEUNames = new string[] { "Austria", "Belgium", "Bulgaria",
            "Croatia", "Cyprus", "Czechia", "Denmark", "Estonia", "Finland",
            "France", "Germany", "Greece", "Hungary", "Ireland", "Italy", "Latvia",
            "Lithuania", "Luxembourg", "Malta", "Netherlands", "Poland", "Portugal",
            "Romania", "Slovakia", "Slovenia", "Spain", "Sweden"};

        /// <summary>
        /// Returns first num elements as a new array
        /// </summary>
        /// <param name="countries"></param>
        /// <param name="numOfElementsToLeave">number of elements from the start of the array</param>
        /// <returns></returns>
        public static Country[] ShortenArray(Country[] countries, int numOfElementsToLeave)
        {
            if (countries.Length < numOfElementsToLeave)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                var result = new Country[numOfElementsToLeave];

                for (int i = 0; i < numOfElementsToLeave; i++)
                {
                    result[i] = countries[i];
                }

                return result;
            }
        }

        /// <summary>
        /// Returns a coppy of the passed array where Country.Name matches any element of the passed String[]
        /// </summary>
        /// <param name="europeNations">Coppy from this</param>
        /// <param name="eUMemberstates">Leave only where name matches any of these</param>
        /// <returns></returns>
        public static Country[] DropNotEncluded(Country[] europeNations, string[] eUMemberstates)
        {
            List<Country> eUCountries = new List<Country>(eUMemberstates.Length);

            foreach (Country country in europeNations)
            {
                if (eUMemberstates.Contains(country.Name))
                {
                    eUCountries.Add(country);
                }
            }
            return eUCountries.ToArray();
        }

        /// <summary>
        /// Returns a coppy of the passed array arranged by descending poppulation
        /// </summary>
        /// <param name="countries"></param>
        /// <returns>Country[] arranged by population desc</returns>
        public static Country[] OrderByPoppulation(Country[] countries)
        {
            var result = countries.OrderByDescending(ob => ob.Poppulation).ToArray();
            return result;
        }

        /// <summary>
        /// Returns a coppy of the passed array arranged by descending poppulation density
        /// </summary>
        /// <param name="countries"></param>
        /// <returns>Country[] arranged pop density desc</returns>
        public static Country[] OrderByPoppulationDensity(Country[] countries)
        {
            var result = countries.OrderByDescending(ob => (ob.Poppulation / ob.Area)).ToArray();
            return result;
        }

        /// <summary>
        /// Sorts a Country[] and Returns first 10 elements of it
        /// </summary>
        /// <param name="byDensityInsteadOfTotalPop">true for sort by density, false for sort by total poppulation</param>
        /// <param name="countries">array</param>
        /// <returns>sorted Country[] with 10 elements</returns>
        public static Country[] Return10(bool byDensityInsteadOfTotalPop, Country[] countries)
        {
            if (byDensityInsteadOfTotalPop)
            {
                countries = OrderByPoppulationDensity(countries);
                countries = ShortenArray(countries, 10);
                return countries;
            }
            else
            {
                countries = OrderByPoppulation(countries);
                countries = ShortenArray(countries, 10);
                return countries;
            }
        }
    }
}
