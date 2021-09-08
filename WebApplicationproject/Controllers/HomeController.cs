using CRUDApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient.Memcached;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApplicationproject.Helper;
using WebApplicationproject.Models;

namespace WebApplicationproject.Controllers
{
    public class HomeController : Controller
    {
        StudentAPI _api = new StudentAPI();
        public async Task<IActionResult> Index()
        {
            List<StudentData> Students = new List<StudentData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/student");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                Students = JsonConvert.DeserializeObject<List<StudentData>>(results);

            }

            return View(Students);
        }
        public async Task<IActionResult>Details(int Id)
        {
            var student = new StudentData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/student/{Id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                student = JsonConvert.DeserializeObject<StudentData>(results);
            }
            return View(student);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult
            Create(StudentData student)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<StudentData>("api/student", student);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var students = new StudentData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/student/{Id}");
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
} 
      

