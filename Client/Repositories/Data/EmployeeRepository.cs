using Client.Base;
using HumanResourceAPI.Models;
using HumanResourceAPI.ModelView;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;


        public EmployeeRepository(Address address, string request = "employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };

        }

        //remove employee based on nik to backend
        public HttpStatusCode Remove(string NIK)
        {
            var result = httpClient.DeleteAsync(request + NIK).Result;
            return result.StatusCode;
        }

        //detail employee based on nik  to backend
        public async Task<Register> Detail(string NIK)
        {
            Register entity = null;

            using (var response = await httpClient.GetAsync(request+ "detail/" + NIK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<Register>(apiResponse);
            }
            return entity;
        }

        //detailreport  to backend
        public object ReportAll()
        {

            Object entities = new object();

            using (var response = httpClient.GetAsync(address.link + request + "register/report").Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }

            return entities;
        }

        //register employee to backend
        public Object Register(Register entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");

            Object entities = new object();

            using (var response = httpClient.PostAsync(address.link + request + "register", content).Result)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                entities = JsonConvert.DeserializeObject<Object>(apiResponse);
            }
            
            return entities;
        }

        //update employee to backend
        public HttpStatusCode Update(Register entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request + "register/update/", content).Result;
            return result.StatusCode;
        }

    }
}
