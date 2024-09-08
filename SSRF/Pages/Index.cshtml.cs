using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSRF.Services;

namespace SSRF.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public string url { get; set; }
        public string FetchResult { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClient)
        {
            _logger = logger;
            _httpClientFactory = httpClient;
        }

        
       

        
        public async void OnPost()
        {
            NetworkUtils networkUtils = new NetworkUtils();
            if (!string.IsNullOrEmpty(url))
            {

                if (networkUtils.isInternalIp(url))
                {
                    FetchResult = "Access to Internal IP is blocked ";
                }
                else
                {
                    string internalResponse = networkUtils.SimulateInternalService(url);
                    if (internalResponse != null)
                    {
                        FetchResult = internalResponse;
                    }
                    else
                    {
                        try
                        {
                            var httpClient = _httpClientFactory.CreateClient();
                            var response = await httpClient.GetStringAsync(url);
                            FetchResult = response.Length > 500 ? response.Substring(0, 500) + "..." : response;
                        }
                        catch (HttpRequestException reqEx)
                        {
                            FetchResult = $"Error fetching URL: {reqEx.Message}";
                        }catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }

                }
            }
        }

    }
}
