using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.Admin
{
    public class FullTimeCVOModel
    {
        [Display(Name = "S No.")]
        public int Sno { get; set; }

        [Display(Name = "Dept Name")]
        public string cvoName { get; set; }

        [Display(Name = "Org Name")]
        public string orgName { get; set; }

        [Display(Name = "CVO Designation")]
        public string cvoDesignation { get; set; }

        [Display(Name = "CVO Services")]
        public string cvoServices { get; set; }

        [Display(Name = "CVO Batch")]
        public string cvoBatch { get; set; }

        [Display(Name = "CVO Level")]
        public string cvoLevel { get; set; }

        [Display(Name = "CVO Tenure From")]
        public string cvoTenureFrom { get; set; }

        [Display(Name = "CVO Tenure To")]
        public string cvoTenureTo { get; set; }

        [Display(Name = "Charges")]
        public string CHARGES { get; set; }

        [Display(Name = "Phone No.")]
        public string PHONE_NO { get; set; }

        [Display(Name = "Email Id")]
        public string EMAIL_ID { get; set; }
        public List<FullTimeCVOModel> FullTimeCVO_List { get; internal set; }
    }
}
