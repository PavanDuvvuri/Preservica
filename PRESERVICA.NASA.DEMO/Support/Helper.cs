using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
{
    
}

namespace PRESERVICA.NASA.DEMO.Support
{
    public class Helper
    {
        public static RestClient? client;
        public static RestRequest? restRequest;
        private readonly IDictionary<string, string> _context;

        public Helper(IDictionary<string, string> context)
        {
            _context = context;
        }

        public static RestClient CreateRestClient(string baseUrl)
        {
            client = new RestClient(baseUrl);
            return client;
        }

        public static RestRequest CreateRestRequest(string resource, Method method, bool alwaysMultipartFormData = false)
        {
            var request = new RestRequest
            {
                Method = method,
                Resource = resource,
                AlwaysMultipartFormData = alwaysMultipartFormData
            };
             return request;
        }

        public static async Task<RestResponse> ExecuteAsync(RestClient client, RestRequest request)
        {
            return await client.ExecuteAsync(request);
        }
    }
}
