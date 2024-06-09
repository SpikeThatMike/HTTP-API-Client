using System.Collections.Specialized;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web;

namespace HTTP.APIClient
{
    public partial class Client : IDisposable
    {
        public async Task<T> ExecuteAsync<T>(Request request)
        {
            var httpRequest = GenerateRequest(request);

            //Create a new instance of HttpResponseMessage
            using HttpResponseMessage response = await HttpClient.SendAsync(httpRequest, _cancellationTokenSource?.Token ?? default);

            //Check if the response was successful
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"The request was not successful. Status code: {response.StatusCode}");
            }

            //Read the response content
            string responseContent = await response.Content.ReadAsStringAsync();

            //Deserialize the response content
            var content = JsonSerializer.Deserialize<T>(responseContent, Options.JsonSerializerOptions ?? new());

            return content;
        }

        public T Execute<T>(Request request)
        {
            var httpRequest = GenerateRequest(request);

            //Create a new instance of HttpResponseMessage
            using HttpResponseMessage response = HttpClient.Send(httpRequest, _cancellationTokenSource?.Token ?? default);

            //Check if the response was successful
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"The request was not successful. Status code: {response.StatusCode}");
            }

            //Read the response content
            string responseContent = response.Content.ReadAsStringAsync().Result;

            //Deserialize the response content
            var content = JsonSerializer.Deserialize<T>(responseContent, Options.JsonSerializerOptions ?? new());

            return content;
        }

        private HttpRequestMessage GenerateRequest(Request request)
        {
            var requestMessage = new HttpRequestMessage(request.Method, request.Url);

            //Set the default request headers
            foreach (var header in Options.DefaultRequestHeaders ?? [])
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            //Set the request headers
            foreach (var header in request.Headers ?? [])
            {
                requestMessage.Headers.Add(header.Key, header.Value);
            }

            //Set the request content
            if (request.Content != null)
            {
                requestMessage.Content = new StringContent(request.Content.ToString(), Encoding.UTF8, "application/json"); ;
            }

            //Set the request parameters
            if (request.Parameters.Count != 0)
            {
                if (request.Method == HttpMethod.Get)
                {
                    var query = ToQueryString(request.Parameters);

                    requestMessage.RequestUri = new Uri(request.Url + query);
                }
                else
                {
                    requestMessage.Content = new FormUrlEncodedContent(request.Parameters.Select(x => new KeyValuePair<string, string>(x.Name, x.Value.ToString())));
                }
            }

            return requestMessage;
        }

        public string ToQueryString(IEnumerable<Parameter> parameters)
        {
            StringBuilder sb = new("?");

            bool first = true;

            foreach (Parameter parameter in parameters)
            {
                if (!first)
                {
                    sb.Append('&');
                }

                sb.AppendFormat("{0}={1}", Uri.EscapeDataString(parameter.Name), Uri.EscapeDataString(parameter.Value.ToString()));

                first = false;
            }

            return sb.ToString();
        }
    }
}
