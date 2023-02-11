using System.ComponentModel.DataAnnotations;

namespace InterviewApplication.Models
{  

    public class PatientInfo
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Epilepsy Epilepsy { get; set; }
    }
    public class DiseaseInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class NCD
    {
        public int Id { get; set; }
        public string NCDName { get; set; }
    }
    public class NCD_Details
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        [Required]
        public int NCDID { get; set; }
    }
    public class Allergies
    {
        public int Id { get; set; }
        public string AllergiesName { get; set; }
    }
    public class Allergies_Details
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        [Required]
        public int AllergiesID { get; set; }
    }
    public enum Epilepsy { Yes, No }

    public class Common
    {
        public List<PatientInfo> P_Info { get; set; }
        public List<DiseaseInfo> D_Info { get; set; }
        public List<NCD> ncd { get; set; }
        public List<NCD_Details> ncdDtails { get; set; }
        public List<Allergies> allergies { get; set; }
        public List<Allergies_Details> allergies_Details { get; set; }
        public Epilepsy [] epilepsies { get; set; }
    }
}
