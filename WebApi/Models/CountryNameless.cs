namespace WebApi.Models
{
    public class ReducedNamelessCountry : ICountryReduced
    {
        public ReducedNamelessCountry(string nativeName, string[] tld, double area, double poppulation)
        {
            NativeName = nativeName;
            Tld = tld;
            Area = area;
            Poppulation = poppulation;
        }


        public string NativeName { get; set; }
        public string[] Tld { get ; set ; }
        public double Area { get; set; }
        public double Poppulation { get; set; }
        public bool HasEmptyFields { get; set; }
        public object? Extras { get; set; }
    }
}
