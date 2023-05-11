using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace Fake_Server_URL.Controllers
{
    public class TodoController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var todos = JsonConvert.DeserializeObject<List<Todo>>(content);
                    return View(todos);
                }
                return View();
            }
        }

        // Other CRUD actions
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Todo todo)
        {
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(todo);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("https://jsonplaceholder.typicode.com/todos", data);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle error
                    return View(todo);
                }
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://jsonplaceholder.typicode.com/todos/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var todo = JsonConvert.DeserializeObject<Todo>(content);
                    return View(todo);
                }
                else
                {
                    // Handle error
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Todo todo)
        {
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(todo);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync($"https://jsonplaceholder.typicode.com/todos/{todo.Id}", data);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle error
                    return View(todo);
                }
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"https://jsonplaceholder.typicode.com/todos/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle error
                    return RedirectToAction("Index");
                }
            }
        }

    }
}