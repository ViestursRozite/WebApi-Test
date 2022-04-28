namespace WebApi.Models
{
    public static class CountryHolder
    {
        public static Country[]? Countries = null;

        public static bool Store(Country[] countries)
        {
            Countries = countries;
            if (Countries != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Country[]? Get()
        {
            return Countries;
        }
    }
}
