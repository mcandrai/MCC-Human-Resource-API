using System;
using HumanResourceAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Base;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using HumanResourceAPI.ModelView;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories.Data
{

    public class AccountRepository : GeneralRepository<Account, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public AccountRepository(Address address, string request = "accounts/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };

        }

        //login direct to backend
        public Object Login(Login login)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

            Object entities = new Object();

            using (var response = httpClient.PostAsync(request + "login/", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }

        //login employee
        public async Task<JwtToken> Auth(Login login)
        {
            JwtToken token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "v1.0/login/", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JwtToken>(apiResponse);

            return token;
        }

    }
}
