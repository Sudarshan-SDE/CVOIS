using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.Admin
{
    public class PendingFullTimeOrgnizations
    {
        public int ID { get; set; }

        [Display(Name = "CVO Name")]
        public string CVO_NAME { get; set; }

        [Display(Name = "Org Name")]
        public string ORGNAME { get; set; }
        public string Designation { get; set; }
        public string Charges{ get; set; }
        public string Services{ get; set; }
        public string Batch{ get; set; }
        public string Tenure_From { get; set; }
        public string Tenure_To{ get; set; }
        public string Status { get; set; }
    }
}
