using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin
{
    public class MasterCvoServicesModel
    {
        [Key]
        [Display(Name = "S No.")]
        public int sno { get; set; }

        [Display(Name = "S No.")]
        public int row_num { get; set; }

        [Display(Name = "Service Code")]
        public string serviceCode { get; set; }

        [Display(Name = "Service Name")]
        public string serviceDesc { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByIP { get; set; }
        public string SessionID { get; set; }
    }
}
