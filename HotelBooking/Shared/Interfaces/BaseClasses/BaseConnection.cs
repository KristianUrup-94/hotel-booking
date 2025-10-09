using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Interfaces.BaseClasses
{
    public class BaseConnection
    {
        private RestClient Client { get; }
        public BaseConnection(string connectionString)
        {
            Client = new RestClient(new RestClientOptions(connectionString));
        }

        /// <summary>
        /// Gets specific data given by the url, and the expected type (T)
        /// </summary>
        /// <typeparam name="T">Specific type</typeparam>
        /// <param name="url">url for getting data</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Exception thrown if received no response</exception>
        public async Task<T> GetAsync<T>(string url) 
        {
            RestRequest request = new RestRequest(url);
            T? response = await Client.GetAsync<T>(request);
            if (response == null) 
            {
                throw new InvalidDataException(url + " Resulted in no response");
            }
            return response;
        }

        /// <summary>
        /// Sends a post request given the object as data
        /// </summary>
        /// <typeparam name="T">Specific type</typeparam>
        /// <param name="url">url for getting data</param>
        /// <param name="data">specific data to post</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Exception thrown if received no response</exception>
        public async Task<T> PostAsync<T>(string url, object data)
        {
            RestRequest request = new RestRequest(url).AddJsonBody(data);
            T? response = await Client.PostAsync<T>(request);
            if (response == null)
            {
                throw new InvalidDataException(url + " Resulted in no response");
            }
            return response;
        }
    }
}
