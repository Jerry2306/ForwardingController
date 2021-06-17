using ForwardingUtils.Contract;
using ForwardingUtils.Helper;
using ForwardingUtils.SharedItems.JsonModel.Request;
using ForwardingUtils.SharedItems.JsonModel.Response;
using Newtonsoft.Json;
using SettingsUtils.Contract;
using SettingsUtils.SharedItems.ConfigurationEntities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ForwardingUtils
{
    public class ForwardManager : IForwardManager, IDisposable
    {
        private readonly AppSettings _config;

        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;
        public ForwardManager(ISettingsManager settingsManager)
        {
            _config = settingsManager.Get<AppSettings>();

            _httpClientHandler = new HttpClientHandler();
            _httpClient = new HttpClient(_httpClientHandler);
        }

        public TunnelEntity StartTunnel(StartTunnelEntity entity)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), CombineUrl(_config.ForwardingConfiguration.APIUrl, "tunnels")))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(entity));
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                var response = TaskAwaiter.WaitForResult(_httpClient.SendAsync(request));

                HandleHttpResponseMessage(response, "StartTunnel");
                return JsonConvert.DeserializeObject<TunnelEntity>(TaskAwaiter.WaitForResult(response.Content.ReadAsStringAsync()));
            }
        }

        public void StopTunnel(string name)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), CombineUrl(_config.ForwardingConfiguration.APIUrl, $"tunnels/{name}")))
            {
                var response = TaskAwaiter.WaitForResult(_httpClient.SendAsync(request));

                HandleHttpResponseMessage(response, "StopTunnel");
                TaskAwaiter.WaitForResult(response.Content.ReadAsStringAsync());
            }
        }

        public TunnelListEntity ListTunnels()
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), CombineUrl(_config.ForwardingConfiguration.APIUrl, "tunnels")))
            {
                var response = TaskAwaiter.WaitForResult(_httpClient.SendAsync(request));

                HandleHttpResponseMessage(response, "ListTunnel");
                return JsonConvert.DeserializeObject<TunnelListEntity>(TaskAwaiter.WaitForResult(response.Content.ReadAsStringAsync()));
            }
        }

        public TunnelEntity GetTunnel(string name)
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), CombineUrl(_config.ForwardingConfiguration.APIUrl, $"tunnels/{name}")))
            {
                var response = TaskAwaiter.WaitForResult(_httpClient.SendAsync(request));

                HandleHttpResponseMessage(response, "GetTunnel");
                return JsonConvert.DeserializeObject<TunnelEntity>(TaskAwaiter.WaitForResult(response.Content.ReadAsStringAsync()));
            }
        }

        private string CombineUrl(string url, string suffix)
        {
            if (!url.EndsWith("/"))
                url += "/";

            if (suffix.StartsWith("/"))
                suffix = suffix.Substring(1);

            return url + suffix;
        }

        private void HandleHttpResponseMessage(HttpResponseMessage response, string source)
        {
            if (!((int)response.StatusCode).ToString().StartsWith("2"))
            {
                var errorMessageBuilder = new StringBuilder();
                errorMessageBuilder.AppendLine("At invoking Ngrok api an error occured!");
                errorMessageBuilder.AppendLine($"StatusCode: {(int)response.StatusCode} - {response.StatusCode}");
                errorMessageBuilder.AppendLine($"Source: {source}");

                throw new HttpRequestException(errorMessageBuilder.ToString());
            }
        }

        public void Dispose()
        {
            _httpClientHandler?.Dispose();
            _httpClient?.Dispose();

            _httpClientHandler = null;
            _httpClient = null;
        }
    }
}