using Microsoft.AspNetCore.Mvc;
using InterviewApplication.Data;
using InterviewApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NCDController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public NCDController(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public List<NCD> GetNCD()
        {
            return dbContext.NCDs.ToList();
        }
        [HttpGet("{id}")]
        public NCD? GetNCDs(int id)
        {
            var ncds = dbContext.NCDs.Where(m => m.Id == id).SingleOrDefault();
            return ncds;
        }
        [HttpPost]
        public IActionResult PostNCD([FromBody] NCD ncdObj)
        {
            if (ModelState.IsValid)
            {
                dbContext.NCDs.Add(ncdObj);
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
