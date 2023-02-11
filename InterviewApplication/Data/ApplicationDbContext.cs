using InterviewApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewApplication.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
        {

        }
        public DbSet<PatientInfo> PatientInfos { get; set; }
        public DbSet<DiseaseInfo> DiseaseInfos { get; set; }
        public DbSet<NCD> NCDs { get; set; }
        public DbSet<NCD_Details> NCD_Details { get; set; }
        public DbSet<Allergies> Allergies { get; set; }
        public DbSet<Allergies_Details> Allergies_Details { get; set; }
    }
}
