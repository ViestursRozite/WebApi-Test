namespace WebApi.Services
{
    public static class ApiHelper
    { 
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("https://restcountries.com/v3.1/");
        }
    }    
}
