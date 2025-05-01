using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin
{
    public class OrgLevelModel
    {
        [Display(Name = "S No.")]
        public int sno { get; set; }

        [Display(Name = "S. No.")]
        public int row_num { get; set; }

        [Display(Name = "Org Code Name")]
        public string Code { get; set; }

        [Display(Name = "Org Level Name")]
        public string org_level { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByIP { get; set; }
        public string SessionID { get; set; }
        public string actionCategory { get; set; }
    }
}
