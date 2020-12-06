﻿using Newtonsoft.Json;
using BuiltCodeWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCodeWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public Repository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<bool> CreateAsync(string url, T objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if(objToCreate != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _httpClientFactory.CreateClient();
            if(client != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
            }

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string url, int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url + Id);

            var client = _httpClientFactory.CreateClient();
            if (client != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
            }
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _httpClientFactory.CreateClient();
            if (client != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
            }
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            }
            else
            {
                return null;
            }
        }

        public async Task<T> GetAsync(string url, int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url+Id);
            var client = _httpClientFactory.CreateClient();
            if (client != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
            }
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(string url, T objToUpdate)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url);
            if (objToUpdate != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(objToUpdate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _httpClientFactory.CreateClient();
            if (client != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
            }
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
