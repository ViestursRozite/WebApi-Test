using WebApi.Models;
using Newtonsoft;

namespace WebApi.Services
{
    public static class RestcountriesService
    {
        public static async Task<string> AskForJson(string queryToAPI = @"region/europe")
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(queryToAPI))
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    //var jsonCSharp = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
                    return json;
                }
                else
                {
                    throw new Exception($"{response.StatusCode.ToString()}");
                }
            }
        }
    }
}
