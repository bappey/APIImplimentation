using Microsoft.AspNetCore.Mvc;
using InterviewApplication.Data;
using InterviewApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public DiseaseController(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }

        [HttpGet]
        public List<DiseaseInfo> Get()
        {
            return dbContext.DiseaseInfos.ToList();
        }
        [HttpGet("{id}")]
        public DiseaseInfo? GetDisease(int id)
        {
            var disease = dbContext.DiseaseInfos.Where(m => m.Id == id).SingleOrDefault();
            return disease;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            var disease = await dbContext.DiseaseInfos.FindAsync(id);
            if (disease == null)
            {
                return NotFound();
            }
            dbContext.DiseaseInfos.Remove(disease);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public IActionResult PostDisease([FromBody] DiseaseInfo diseaseObj)
        {
            if (ModelState.IsValid)
            {
                dbContext.DiseaseInfos.Add(diseaseObj);
                dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest("Not a Valid Data");
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
