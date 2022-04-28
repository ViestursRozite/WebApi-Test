namespace WebApi.Models
{
    public class Country : ICountryReduced
    {
        public Country(string name, string nativeName, string[] tld, double area, double poppulation, bool hasEmptyFields)
        {
            Name = name;
            NativeName = nativeName;
            Tld = tld;
            Area = area;
            Poppulation = poppulation;
            HasEmptyFields = hasEmptyFields;
        }

        public string Name { get; set; }
        public string NativeName { get; set; }
        public string[] Tld { get; set; }
        public double Area { get; set; }
        public double Poppulation { get; set; }
        public bool HasEmptyFields { get; set; }
        public object? Extras { get; set; }
    }
}
