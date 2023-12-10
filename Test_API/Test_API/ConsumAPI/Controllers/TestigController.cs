using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Test_API.Model;

namespace ConsumAPI.Controllers
{
    public class TestigController : Controller
    {
        public IActionResult Index()
        {
           List<Customer>data=new List<Customer>();
            try
            {
                using(HttpClient client=new HttpClient())
                {
                    client.BaseAddress = new Uri("");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.GetAsync("/Customer/GetAllCustomer").Result;
                    client.Dispose();
                }
            }
        }
    }
}
