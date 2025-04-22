using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin
{
    public class MinistryModel
    {
        [Key]
        [Display(Name = "Min Id")]
        public int Min_id { get; set; }

        [Display(Name = "Min Code")]
        public string Mincode { get; set; }

        [Display(Name = "Ministry Name")]
        public string Ministry_Name { get; set; }

        [Display(Name = "Ministry Status")]
        public string Min_Status { get; set; }

        [Display(Name = "Ministry Type")]
        public string Ministry_Type { get; set; }


        public string CreatedBy { get; set; }
        public string CreatedByIP { get; set; }
        public string SessionID { get; set; }
    }
}
