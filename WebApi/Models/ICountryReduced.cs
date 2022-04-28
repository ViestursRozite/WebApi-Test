namespace WebApi.Models
{
    public interface ICountryReduced
    {
        public string NativeName { get; set; }
        public string[] Tld { get; set; }
        public double Area { get; set; }
        public double Poppulation { get; set; }
        public bool HasEmptyFields { get; set; }
        public object? Extras { get; set; }
    }
}
