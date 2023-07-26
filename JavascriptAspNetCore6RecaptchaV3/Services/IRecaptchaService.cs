using JavascriptAspNetCore6RecaptchaV3.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Net;

namespace JavascriptAspNetCore6RecaptchaV3.Services
{
    public interface IRecaptchaService
    {
        Task<bool> ValidateRecaptcha(string recaptchaResponse, bool useProxy = false);
    }

    public class RecaptchaService : IRecaptchaService
    {
        private readonly IOptions<GoogleReCaptcha> _options;

        public RecaptchaService(IOptions<GoogleReCaptcha> options)
        {
            _options = options;
        }

        public async Task<bool> ValidateRecaptcha(string recaptchaResponse, bool useProxy)
        {
            bool isSuccess = false;

            // Create Handler
            var handler = new HttpClientHandler();
            // Proxy 
            WebProxy proxy = new WebProxy();
            proxy.Address = new Uri(_options.Value.WebProxyUri);
            handler.Proxy = proxy;
            handler = useProxy == true ? handler : new HttpClientHandler();


            using (var httpClient = new HttpClient(handler))
            {
                var response = await httpClient.PostAsync(_options.Value.ReCaptchaServiceBaseUrl, new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("secret", _options.Value.Secretkey),
                    new KeyValuePair<string, string>("response", recaptchaResponse)
                }));

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    // Parse the response JSON and check if 'success' is true
                    isSuccess = JObject.Parse(responseBody)["success"].ToObject<bool>();
                }

                return isSuccess;
            }
        }
    }
}
