using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin
{
    public class AppointingAuthorityModel
    {
        [Key]
        [Display(Name = "S No.")]
        public int sno { get; set; }

        [Display(Name = "S No.")]
        public int row_num { get; set; }

        [Display(Name = "Appointing Authority Code")]
        public string AppointingAuthority_Code { get; set; }

        [Display(Name = "Appointing Authority")]
        public string APPOINTING_AUTHORITY { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByIP { get; set; }
        public string SessionID { get; set; }
        public string actionCategory { get; set; }
    }
}
