using Microsoft.AspNetCore.Mvc;
using InterviewApplication.Data;
using InterviewApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.CompilerServices;

namespace InterviewApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;  
        public PatientController (ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            Common AllDataList = new Common
            {
                D_Info = dbContext.DiseaseInfos.ToList(),
                ncd = dbContext.NCDs.ToList(),
                allergies = dbContext.Allergies.ToList(),
                epilepsies = new[] { Epilepsy.Yes, Epilepsy.No }
            };
            return Ok (AllDataList);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNCD(int id)
        {
            var nCD = await dbContext.NCDs.FindAsync(id);
            if (nCD == null)
            {
                return NotFound();
            }
            dbContext.NCDs.Remove(nCD);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpPost]
        public IActionResult PostPatient(Common allData)
        {
            try
            {
                if (ModelState.IsValid)
                {                   
                    PatientInfo pInfo = new PatientInfo();
                    pInfo.Name = allData.P_Info[0].Name;
                    pInfo.Epilepsy = allData.P_Info[0].Epilepsy;
                    dbContext.PatientInfos.Add(pInfo);
                    dbContext.SaveChanges();
                    var maxID = dbContext.PatientInfos.DefaultIfEmpty().Max(r => r == null ? 0 : r.Id);

                    List<NCD_Details> nCD_Details1 = allData.ncdDtails;
                    if (allData.ncdDtails == null)
                    {
                        nCD_Details1 = new List<NCD_Details>();
                    }
                    foreach (NCD_Details modeldata in nCD_Details1)
                    {
                        NCD_Details nCD_Details = new NCD_Details();
                        nCD_Details.PatientID = maxID;
                        nCD_Details.NCDID = modeldata.NCDID;

                        dbContext.NCD_Details.Add(nCD_Details);                        
                        dbContext.SaveChanges();
                    }

                    List<Allergies_Details> allergies_Details = allData.allergies_Details;
                    if (allData.allergies_Details == null)
                    {
                        allergies_Details = new List<Allergies_Details>();
                    }

                    foreach (Allergies_Details modeldata in allergies_Details)
                    {
                        Allergies_Details allergies = new Allergies_Details();
                        allergies.PatientID = maxID;
                        allergies.AllergiesID = modeldata.AllergiesID;
                        dbContext.Allergies_Details.Add(allergies);                        
                        dbContext.SaveChanges();
                    }

                    return Ok("Data Save Successfully");
                }
                else
                {
                    return BadRequest("Not a Valid Data");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
