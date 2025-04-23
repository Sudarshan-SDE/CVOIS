using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace CVOIS.Models.Admin
{
    public class Organizations
    {
        [Key]
        [Display(Name = "S No.")]
        public int SNo { get; set; }
        public int ORG_ID { get; set; }

        [Display(Name = "Min Name")]
        public string Ministry_Name { get; set; }

        [Display(Name = "Dept name")]
        public string Dept_Name { get; set; }
        public string DeptName{ get; set; }

        [Display(Name = "Org Name")]
        public string Org_Name { get; set; }

        [Display(Name = "Org Address")]
        public string org_address { get; set; }

        [Display(Name = "Pincode")]
        public string pincode { get; set; }

        [Display(Name = "Phone No.")]
        public string phoneno { get; set; }

        [Display(Name = "Org Category")]
        public string org_category { get; set; }

        [Display(Name = "Org Status")]
        public string org_status { get; set; }












        public List<Organizations> Organizations_List { get; internal set; }
    }
}
