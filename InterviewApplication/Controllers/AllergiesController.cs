using Microsoft.AspNetCore.Mvc;
using InterviewApplication.Data;
using InterviewApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AllergiesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public AllergiesController(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public List<Allergies> GetAllergies()
        {
            return dbContext.Allergies.ToList();
        }
        [HttpGet("{id}")]
        public Allergies? GetAllergies(int id)
        {
            var allergies = dbContext.Allergies.Where(m => m.Id == id).SingleOrDefault();
            return allergies;
        }
        [HttpPost]
        public IActionResult PostAllergies([FromBody] Allergies allergiesObj)
        {
            if (ModelState.IsValid)
            {
                dbContext.Allergies.Add(allergiesObj);
                dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest("Not a Valid Data");
            }
        }
    }
}
