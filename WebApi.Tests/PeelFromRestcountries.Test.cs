using Xunit;
using WebApi.Services;
using WebApi.Controllers;
using WebApi.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System;

namespace WebApi.Tests
{
    public class PeelFromRestcountries
    {
        [Theory]
        [ClassData(typeof(PeelNationsEuropeTestData))]
        public void PeelNationsEurope_Returns_Countries_Object_Array(JArray param1, Country[] expected)
        {
            //Arrange

            //Act
            var actual = Services.PeelFromRestcountries.ExpectV3Structure.PeelNationsEurope(param1);
            //Assert
            Assert.Equal(expected.Length, actual.Length);
        }

        [Theory]
        [ClassData(typeof(PeelMatchingJTokenNationTestData))]
        public void PeelMatchingJTokenNation_Peels_JToken(Country param1, JArray param2, JToken expected)
        {
            //Arrange

            //Act
            JToken? actual = Services.PeelFromRestcountries.ExpectV3Structure.PeelMatchingJTokenNation(param1, param2);
            //Assert
            Assert.Equal(expected.GetType(), actual.GetType());
        }

        [Theory]
        [ClassData(typeof(PeelMatchingJTokenNationTestData))]
        public void PeelMatchingJTokenNation_Peels_Correct_JToken(Country param1, JArray param2, JToken expected)
        {
            //Arrange

            //Act
            JToken? actual = Services.PeelFromRestcountries.ExpectV3Structure.PeelMatchingJTokenNation(param1, param2);
            //Assert
            Assert.Equal(expected.SelectToken("name.common").ToString(), actual.SelectToken("name.common").ToString());
        }

        [Theory]
        [ClassData(typeof(SelectToEmptyFieldInCountryTestData))]
        public void SelectToEmptyFieldInCountry_Selects_Any_Field_To_Empty(
            Func<JToken, JToken> Param1LinqExpr, Country[] Param2Countries,
                JArray Param3JArr, Country[] expected)
        {
            //Arrange
            expected[0].Extras = "Republic of Latvia";
            expected[1].Extras = "Federal Republic of Germany";

            var actual = Param2Countries;
            //Act
            Services.PeelFromRestcountries.ExpectV3Structure.SelectToEmptyFieldInCountry( 
                Param1LinqExpr, actual, Param3JArr);
            //Assert
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i].Extras, actual[i].Extras);
            }
        }

        internal class SelectToEmptyFieldInCountryTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    (Func<JToken, JToken>)((x) => x.SelectToken("name.official")),
                    new Country[] 
                    {
                        new Country("Latvia", "Latvija", new string[] { ".lv" }, 64559, 1901548, false),
                        new Country("Germany", "Deutschland", new string[] { ".de" }, 357114, 83240525, false)
                    },
                    DataPrep.MakeTestData("BackupData.json"),
                    new Country[]
                    {
                        new Country("Latvia", "Latvija", new string[] { ".lv" }, 64559, 1901548, false),
                        new Country("Germany", "Deutschland", new string[] { ".de" }, 357114, 83240525, false)
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        internal class PeelMatchingJTokenNationTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new Country("Latvia", "Latvija", new string[] { ".lv" }, 64559, 1901548, false),
                    DataPrep.MakeTestData("LatviaData.json"),
                    DataPrep.MakeTestData("LatviaData.json")[0]
                };
                yield return new object[]
                {
                    new Country("Latvia", "Latvija", new string[] { ".lv" }, 64559, 1901548, false),
                    DataPrep.MakeTestData("BackupData.json"),
                    DataPrep.MakeTestData("LatviaData.json")[0]
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class PeelNationsEuropeTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    DataPrep.MakeTestData("LatviaData.json"),
                    new Country[] 
                    {
                        new Country("Latvia", "Latvija", new string[] { ".lv" }, 64559, 1901548, false)
                    }
                };
                yield return new object[]
                {
                    DataPrep.MakeTestData("BackupData.json"),
                    new Country[53]
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }

    public static class DataPrep
    {
        public static JArray MakeTestData(string filename)
        {
            string path = Path.Combine(Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location), @"..\..\..\", $"JArrayTestData\\{filename}");

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                JArray testData = JArray.Parse(json);

                return testData;
            }
        }
    }

}
