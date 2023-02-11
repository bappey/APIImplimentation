using System.Diagnostics;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FrontEndApplication.Helper;
using FrontEndApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using InterviewApplication.Models;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace FrontEndApplication.Controllers
{
    public class HomeController : Controller
    {
        PatientAPI api = new PatientAPI();

        public async Task<IActionResult> Index()
        {
            List<DiseaseInfo> diseaseInfos = new List<DiseaseInfo>();            

            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Disease");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                diseaseInfos = JsonConvert.DeserializeObject<List<DiseaseInfo>>(result);                
            }            
            return View(diseaseInfos);
        }        

        public async Task<IActionResult> Details(int id)
        {
            var disease = new DiseaseInfo();

            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/Patient/{id}");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                disease = JsonConvert.DeserializeObject<DiseaseInfo>(result);
            }
            return View(disease);
        }
        
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult create(DiseaseInfo disease)
        {
            HttpClient client = api.Initial();
            var diseases = client.PostAsJsonAsync<DiseaseInfo>("api/Patient", disease);
            diseases.Wait();

            var result = diseases.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task< IActionResult> Delete(int Id)
        {
            var disease = new DiseaseInfo();
            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/patient/{Id}");
        
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> NCDIndex()
        {
            List<NCD> ncds = new List<NCD>();
            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/NCD");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                ncds = JsonConvert.DeserializeObject<List<NCD>>(result);
            }
            return View(ncds);
        }

        public ActionResult createNCD()
        {
            return View();
        }
        [HttpPost]
        public IActionResult createNCD(NCD nCD)
        {
            HttpClient client = api.Initial();
            var ncd = client.PostAsJsonAsync<NCD>("api/NCD", nCD);
            ncd.Wait();

            var result = ncd.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("NCDIndex");
            }
            return View();
        }
        public async Task<IActionResult> AllergiesIndex()
        {
            List<Allergies> allergies = new List<Allergies>();
            HttpClient client = api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Allergies");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                allergies = JsonConvert.DeserializeObject<List<Allergies>>(result);
            }
            return View(allergies);
        }
        
        public ActionResult createAllergies()
        {
            return View();
        }
        [HttpPost]
        public IActionResult createAllergies(Allergies allergies)
        {
            HttpClient client = api.Initial();
            var allergiess = client.PostAsJsonAsync<Allergies>("api/Allergies", allergies);
            allergiess.Wait();

            var result = allergiess.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("AllergiesIndex");
            }
            return View();
        }

        public async Task<IActionResult> PatientIndex()
        {
            Common commonData = new Common();
            try
            {
                HttpClient client = api.Initial();
                HttpResponseMessage res = await client.GetAsync("api/Patient");

                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                     commonData = JsonConvert.DeserializeObject<Common>(result);            
                    }
                TempData["Message"] = TempData["Message"];
                return View(commonData);
            }
            catch
            {
                throw;
            }            
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPatient( Common allData)
        {
            if (allData == null)
            {
                return BadRequest("No data found");
            }

            HttpClient client = api.Initial();
            var registration = await client.PostAsJsonAsync<Common>("api/Patient", allData);
            
            if (registration.IsSuccessStatusCode)
            {                
                return RedirectToAction("PatientIndex");
            }

            return View();
        }


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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