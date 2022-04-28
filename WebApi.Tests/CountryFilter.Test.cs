using Xunit;
using WebApi.Services;
using WebApi.Controllers;
using WebApi.Models;
using System.Collections.Generic;
using System.Collections;
using System;

namespace WebApi.Tests
{
    public static class CountryFilter
    {
        [Theory]
        [ClassData(typeof(ShortenArrayTestData))]
        public static void ShortenArray_Shortens_Country_Array(Country[] param1, int param2, Country[] expected)
        {
            //Arrange

            //Act
            var actual = Services.CountryFilter.ShortenArray(param1, param2);

            //Assert
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].NativeName, actual[i].NativeName);
                Assert.Equal(expected[i].Area, actual[i].Area);
                Assert.Equal(expected[i].Poppulation, actual[i].Poppulation);
                Assert.Equal(expected[i].Tld[0], actual[i].Tld[0]);
                Assert.Equal(expected[i].HasEmptyFields, actual[i].HasEmptyFields);
            }
        }

        [Fact]
        public static void ShortenArray_Throws_Exeptions_If_Out_Of_Range()
        {
            //Arrange
            Country[] data = CreateTestArr();
            object actual;
            //Act
            try
            {
                var returned = Services.CountryFilter.ShortenArray(data, 12);
                actual = new Exception();
            }
            catch (Exception exeption)
            {
                actual = exeption;
            }

            //Assert
            Assert.IsType<IndexOutOfRangeException>(actual);
        }

        [Theory]
        [ClassData(typeof(DropNotEncludedTestData))]
        public static void DropNotEncluded_Returns_A_Coppy_Where_Only_Included_Remain(Country[] param1, string[] param2, Country[] expected)
        {
            //Arrange

            //Act
            var actual = Services.CountryFilter.DropNotEncluded(param1, param2);
            //Assert
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].NativeName, actual[i].NativeName);
                Assert.Equal(expected[i].Area, actual[i].Area);
                Assert.Equal(expected[i].Poppulation, actual[i].Poppulation);
                Assert.Equal(expected[i].Tld[0], actual[i].Tld[0]);
                Assert.Equal(expected[i].HasEmptyFields, actual[i].HasEmptyFields);
            }
        }

        [Theory]
        [ClassData(typeof(OrderByPoppulationTestData))]
        public static void OrderByPoppulation_Returns_Coppy_Of_Passed_Arranged_Population_desc(
            Country[] param1, Country[] expected)
        {
            //Arrange

            //Act
            var actual = Services.CountryFilter.OrderByPoppulation(param1);
            //Assert
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].NativeName, actual[i].NativeName);
                Assert.Equal(expected[i].Area, actual[i].Area);
                Assert.Equal(expected[i].Poppulation, actual[i].Poppulation);
                Assert.Equal(expected[i].Tld[0], actual[i].Tld[0]);
                Assert.Equal(expected[i].HasEmptyFields, actual[i].HasEmptyFields);
            }
        }

        [Theory]
        [ClassData(typeof(OrderByPoppulationDensityTestData))]
        public static void OrderByPoppulationDensity_Returns_Coppy_Of_Passed_Arranged_PopulationDensity_desc(
            Country[] param1, Country[] expected)
        {
            //Arrange

            //Act
            var actual = Services.CountryFilter.OrderByPoppulationDensity(param1);
            //Assert
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].NativeName, actual[i].NativeName);
                Assert.Equal(expected[i].Area, actual[i].Area);
                Assert.Equal(expected[i].Poppulation, actual[i].Poppulation);
                Assert.Equal(expected[i].Tld[0], actual[i].Tld[0]);
                Assert.Equal(expected[i].HasEmptyFields, actual[i].HasEmptyFields);
            }
        }
    
        [Theory]
        [ClassData(typeof(Return10TestData))]
        public static void Return10_Returns_Shortened_Sorted_Array(
            bool param1, Country[] param2, Country[] expected)
        {
            //Arrange

            //Arrange
            var actual = Services.CountryFilter.Return10(param1, param2);
            //Assert
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i].Name, actual[i].Name);
                Assert.Equal(expected[i].NativeName, actual[i].NativeName);
                Assert.Equal(expected[i].Area, actual[i].Area);
                Assert.Equal(expected[i].Poppulation, actual[i].Poppulation);
                Assert.Equal(expected[i].Tld[0], actual[i].Tld[0]);
                Assert.Equal(expected[i].HasEmptyFields, actual[i].HasEmptyFields);
            }
        }

        public static Country[] CreateTestArr()
        {
            return (new Country[] {
                new Country("aaaa", "aa", new string[] {"aaa" }, 11, 111, false),
                new Country("bbbb", "bb", new string[] {"bbb" }, 22, 222, false),
                new Country("cccc", "cc", new string[] {"ccc" }, 33, 333, false),
                new Country("dddd", "dd", new string[] {"ddd" }, 44, 444, false),
                new Country("eeee", "ee", new string[] {"eee" }, 55, 555, false),
                new Country("ffff", "ff", new string[] {"fff" }, 66, 666, false),
                new Country("gggg", "gg", new string[] {"ggg" }, 77, 777, false),
                new Country("hhhh", "hh", new string[] {"hhh" }, 88, 888, false),
                new Country("iiii", "ii", new string[] {"iii" }, 99, 999, false)
            });
        }

        internal class Return10TestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                //Country[] 18 elements
                yield return new object[] 
                { 
                    true,
                    new Country[]
                    {
                        new Country("gggg", "gg", new string[] {"ggg" }, 111, 777, false),
                        new Country("eeee", "ee", new string[] {"eee" }, 111, 555, false),
                        new Country("hhhh", "hh", new string[] {"hhh" }, 111, 888, false),
                        new Country("dddd", "dd", new string[] {"ddd" }, 111, 444, false),
                        new Country("cccc", "cc", new string[] {"ccc" }, 111, 333, false),
                        new Country("aaaa", "aa", new string[] {"aaa" }, 111, 111, false),
                        new Country("ffff", "ff", new string[] {"fff" }, 111, 666, false),
                        new Country("iiii", "ii", new string[] {"iii" }, 111, 999, false),
                        new Country("bbbb", "bb", new string[] {"bbb" }, 111, 222, false),
                        new Country("ggg1", "gg", new string[] {"ggg" }, 110, 777, false),
                        new Country("eee1", "ee", new string[] {"eee" }, 112, 555, false),
                        new Country("hhh1", "hh", new string[] {"hhh" }, 110, 888, false),
                        new Country("ddd1", "dd", new string[] {"ddd" }, 112, 444, false),
                        new Country("ccc1", "cc", new string[] {"ccc" }, 110, 333, false),
                        new Country("aaa1", "aa", new string[] {"aaa" }, 112, 111, false),
                        new Country("fff1", "ff", new string[] {"fff" }, 110, 666, false),
                        new Country("iii1", "ii", new string[] {"iii" }, 112, 999, false),
                        new Country("bbb1", "bb", new string[] {"bbb" }, 110, 222, false)
                    },
                    new Country[]
                    {
                        new Country("iiii", "ii", new string[] {"iii" }, 111, 999, false),
                        new Country("iii1", "ii", new string[] {"iii" }, 112, 999, false),
                        new Country("hhh1", "hh", new string[] {"hhh" }, 110, 888, false),
                        new Country("hhhh", "hh", new string[] {"hhh" }, 111, 888, false),
                        new Country("ggg1", "gg", new string[] {"ggg" }, 110, 777, false),
                        new Country("gggg", "gg", new string[] {"ggg" }, 111, 777, false),
                        new Country("fff1", "ff", new string[] {"fff" }, 110, 666, false),
                        new Country("ffff", "ff", new string[] {"fff" }, 111, 666, false),
                        new Country("eeee", "ee", new string[] {"eee" }, 111, 555, false),
                        new Country("eee1", "ee", new string[] {"eee" }, 112, 555, false)
                    }
                };
                yield return new object[]
                {
                    false,
                    new Country[]
                    {
                        new Country("gggg", "gg", new string[] {"ggg" }, 111, 777, false),
                        new Country("eeee", "ee", new string[] {"eee" }, 111, 555, false),
                        new Country("hhhh", "hh", new string[] {"hhh" }, 111, 888, false),
                        new Country("dddd", "dd", new string[] {"ddd" }, 111, 444, false),
                        new Country("cccc", "cc", new string[] {"ccc" }, 111, 333, false),
                        new Country("aaaa", "aa", new string[] {"aaa" }, 111, 111, false),
                        new Country("ffff", "ff", new string[] {"fff" }, 111, 666, false),
                        new Country("iiii", "ii", new string[] {"iii" }, 111, 999, false),
                        new Country("bbbb", "bb", new string[] {"bbb" }, 111, 222, false),
                        new Country("ggg1", "gg", new string[] {"ggg" }, 110, 777, false),
                        new Country("eee1", "ee", new string[] {"eee" }, 112, 555, false),
                        new Country("hhh1", "hh", new string[] {"hhh" }, 110, 888, false),
                        new Country("ddd1", "dd", new string[] {"ddd" }, 112, 444, false),
                        new Country("ccc1", "cc", new string[] {"ccc" }, 110, 333, false),
                        new Country("aaa1", "aa", new string[] {"aaa" }, 112, 111, false),
                        new Country("fff1", "ff", new string[] {"fff" }, 110, 666, false),
                        new Country("iii1", "ii", new string[] {"iii" }, 112, 999, false),
                        new Country("bbb1", "bb", new string[] {"bbb" }, 110, 222, false)
                    },
                    new Country[]
                    {
                        new Country("iiii", "ii", new string[] {"iii" }, 111, 999, false),
                        new Country("iii1", "ii", new string[] {"iii" }, 112, 999, false),
                        new Country("hhhh", "hh", new string[] {"hhh" }, 111, 888, false),
                        new Country("hhh1", "hh", new string[] {"hhh" }, 110, 888, false),
                        new Country("gggg", "gg", new string[] {"ggg" }, 111, 777, false),
                        new Country("ggg1", "gg", new string[] {"ggg" }, 110, 777, false),
                        new Country("ffff", "ff", new string[] {"fff" }, 111, 666, false),
                        new Country("fff1", "ff", new string[] {"fff" }, 110, 666, false),
                        new Country("eeee", "ee", new string[] {"eee" }, 111, 555, false),
                        new Country("eee1", "ee", new string[] {"eee" }, 112, 555, false)
                    }
                };

                //Country[] 13 elements
                yield return new object[]
                {
                    true,
                    new Country[]
                    {
                        new Country("gggg", "gg", new string[] {"ggg" }, 111, 777, false),
                        new Country("eeee", "ee", new string[] {"eee" }, 111, 555, false),
                        new Country("hhhh", "hh", new string[] {"hhh" }, 111, 888, false),
                        new Country("dddd", "dd", new string[] {"ddd" }, 111, 444, false),
                        new Country("cccc", "cc", new string[] {"ccc" }, 111, 333, false),
                        new Country("aaaa", "aa", new string[] {"aaa" }, 111, 111, false),
                        new Country("ffff", "ff", new string[] {"fff" }, 111, 666, false),
                        new Country("iiii", "ii", new string[] {"iii" }, 111, 999, false),
                        new Country("bbbb", "bb", new string[] {"bbb" }, 111, 222, false),
                        new Country("ggg1", "gg", new string[] {"ggg" }, 110, 777, false),
                        new Country("eee1", "ee", new string[] {"eee" }, 112, 555, false),
                        new Country("hhh1", "hh", new string[] {"hhh" }, 110, 888, false),
                        new Country("ddd1", "dd", new string[] {"ddd" }, 112, 444, false)
                    },
                    new Country[]
                    {
                        new Country("iiii", "ii", new string[] {"iii" }, 111, 999, false),
                        new Country("hhh1", "hh", new string[] {"hhh" }, 110, 888, false),
                        new Country("hhhh", "hh", new string[] {"hhh" }, 111, 888, false),
                        new Country("ggg1", "gg", new string[] {"ggg" }, 110, 777, false),
                        new Country("gggg", "gg", new string[] {"ggg" }, 111, 777, false),
                        new Country("ffff", "ff", new string[] {"fff" }, 111, 666, false),
                        new Country("eeee", "ee", new string[] {"eee" }, 111, 555, false),
                        new Country("eee1", "ee", new string[] {"eee" }, 112, 555, false),
                        new Country("dddd", "dd", new string[] {"ddd" }, 111, 444, false),
                        new Country("ddd1", "dd", new string[] {"ddd" }, 112, 444, false)
                    }
                };
                yield return new object[]
                {
                    false,
                    new Country[]
                    {
                        new Country("gggg", "gg", new string[] {"ggg" }, 111, 777, false),
                        new Country("eeee", "ee", new string[] {"eee" }, 111, 555, false),
                        new Country("hhhh", "hh", new string[] {"hhh" }, 111, 888, false),
                        new Country("dddd", "dd", new string[] {"ddd" }, 111, 444, false),
                        new Country("cccc", "cc", new string[] {"ccc" }, 111, 333, false),
                        new Country("aaaa", "aa", new string[] {"aaa" }, 111, 111, false),
                        new Country("ffff", "ff", new string[] {"fff" }, 111, 666, false),
                        new Country("iiii", "ii", new string[] {"iii" }, 111, 999, false),
                        new Country("bbbb", "bb", new string[] {"bbb" }, 111, 222, false),
                        new Country("ggg1", "gg", new string[] {"ggg" }, 110, 777, false),
                        new Country("eee1", "ee", new string[] {"eee" }, 112, 555, false),
                        new Country("hhh1", "hh", new string[] {"hhh" }, 110, 888, false),
                        new Country("ddd1", "dd", new string[] {"ddd" }, 112, 444, false)
                    },
                    new Country[]
                    {
                        new Country("iiii", "ii", new string[] {"iii" }, 111, 999, false),
                        new Country("hhhh", "hh", new string[] {"hhh" }, 111, 888, false),
                        new Country("hhh1", "hh", new string[] {"hhh" }, 110, 888, false),
                        new Country("gggg", "gg", new string[] {"ggg" }, 111, 777, false),
                        new Country("ggg1", "gg", new string[] {"ggg" }, 110, 777, false),
                        new Country("ffff", "ff", new string[] {"fff" }, 111, 666, false),
                        new Country("eeee", "ee", new string[] {"eee" }, 111, 555, false),
                        new Country("eee1", "ee", new string[] {"eee" }, 112, 555, false),
                        new Country("dddd", "dd", new string[] {"ddd" }, 111, 444, false),
                        new Country("ddd1", "dd", new string[] {"ddd" }, 112, 444, false)
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        internal class OrderByPoppulationDensityTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                    {
                        new Country[] {
                        new Country("gggg", "gg", new string[] {"ggg" }, 111, 777, false),
                        new Country("eeee", "ee", new string[] {"eee" }, 111, 555, false),
                        new Country("hhhh", "hh", new string[] {"hhh" }, 111, 888, false),
                        new Country("dddd", "dd", new string[] {"ddd" }, 111, 444, false),
                        new Country("cccc", "cc", new string[] {"ccc" }, 111, 333, false),
                        new Country("aaaa", "aa", new string[] {"aaa" }, 111, 111, false),
                        new Country("ffff", "ff", new string[] {"fff" }, 111, 666, false),
                        new Country("iiii", "ii", new string[] {"iii" }, 111, 999, false),
                        new Country("bbbb", "bb", new string[] {"bbb" }, 111, 222, false)
                        },
                        new Country[] {
                        new Country("iiii", "ii", new string[] {"iii" }, 111, 999, false),
                        new Country("hhhh", "hh", new string[] {"hhh" }, 111, 888, false),
                        new Country("gggg", "gg", new string[] {"ggg" }, 111, 777, false),
                        new Country("ffff", "ff", new string[] {"fff" }, 111, 666, false),
                        new Country("eeee", "ee", new string[] {"eee" }, 111, 555, false),
                        new Country("dddd", "dd", new string[] {"ddd" }, 111, 444, false),
                        new Country("cccc", "cc", new string[] {"ccc" }, 111, 333, false),
                        new Country("bbbb", "bb", new string[] {"bbb" }, 111, 222, false),
                        new Country("aaaa", "aa", new string[] {"aaa" }, 111, 111, false)
                        }
                    };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        internal class OrderByPoppulationTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                    {
                        new Country[]
                        {
                            new Country("bbbb", "bb", new string[] {"bbb" }, 22, 222, false),
                            new Country("gggg", "gg", new string[] {"ggg" }, 77, 777, false)
                        },
                        new Country[]
                        {
                            new Country("gggg", "gg", new string[] {"ggg" }, 77, 777, false),
                            new Country("bbbb", "bb", new string[] {"bbb" }, 22, 222, false)
                        }
                    };
                yield return new object[]
                    {
                        new Country[]
                        {
                            new Country("dddd", "dd", new string[] {"ddd" }, 44, 444, false),
                            new Country("hhhh", "hh", new string[] {"hhh" }, 88, 888, false),
                            new Country("iiii", "ii", new string[] {"iii" }, 99, 999, false),
                            new Country("gggg", "gg", new string[] {"ggg" }, 77, 777, false),
                            new Country("bbbb", "bb", new string[] {"bbb" }, 22, 222, false),
                            new Country("cccc", "cc", new string[] {"ccc" }, 33, 333, false),
                            new Country("eeee", "ee", new string[] {"eee" }, 55, 555, false),
                            new Country("ffff", "ff", new string[] {"fff" }, 66, 666, false),
                            new Country("aaaa", "aa", new string[] {"aaa" }, 11, 111, false)
                        },
                        new Country[] {
                            new Country("iiii", "ii", new string[] {"iii" }, 99, 999, false), 
                            new Country("hhhh", "hh", new string[] {"hhh" }, 88, 888, false),
                            new Country("gggg", "gg", new string[] {"ggg" }, 77, 777, false),
                            new Country("ffff", "ff", new string[] {"fff" }, 66, 666, false),
                            new Country("eeee", "ee", new string[] {"eee" }, 55, 555, false),
                            new Country("dddd", "dd", new string[] {"ddd" }, 44, 444, false),
                            new Country("cccc", "cc", new string[] {"ccc" }, 33, 333, false),
                            new Country("bbbb", "bb", new string[] {"bbb" }, 22, 222, false),
                            new Country("aaaa", "aa", new string[] {"aaa" }, 11, 111, false)
                        }
                    };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        internal class DropNotEncludedTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    CountryFilter.CreateTestArr(),
                    new string[] {"bbbb", "gggg" },
                    new Country[]
                    {
                        new Country("bbbb", "bb", new string[] {"bbb" }, 22, 222, false),
                        new Country("gggg", "gg", new string[] {"ggg" }, 77, 777, false),
                    }
                };
                yield return new object[]
                {
                    CountryFilter.CreateTestArr(),
                    new string[] {"bbbb"},
                    new Country[]
                    {
                        new Country("bbbb", "bb", new string[] {"bbb" }, 22, 222, false),
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ShortenArrayTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] 
                {
                    CountryFilter.CreateTestArr(),
                    2,
                    new Country[] {
                        new Country("aaaa", "aa", new string[] {"aaa" }, 11, 111, false),
                        new Country("bbbb", "bb", new string[] {"bbb" }, 22, 222, false) }
                };

                yield return new object[]
                {
                    CountryFilter.CreateTestArr(),
                    3,
                    new Country[] {
                        new Country("aaaa", "aa", new string[] {"aaa" }, 11, 111, false),
                        new Country("bbbb", "bb", new string[] {"bbb" }, 22, 222, false), 
                        new Country("cccc", "cc", new string[] {"ccc" }, 33, 333, false)}
                };
            }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}

    