using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace CVOIS.Models.Admin
{
    public class Organizations
    {
        [Key]
        [Display(Name = "S No.")]
        public int SNo { get; set; }

        [Display(Name = "Min Name")]
        public string Ministry_Name { get; set; }

        [Display(Name = "Dept name")]
        public string Dept_Name { get; set; }

        [Display(Name = "Org Name")]
        public string Org_Name { get; set; }

        [Display(Name = "Org Address")]
        public string Org_Address { get; set; }

        [Display(Name = "Pincode")]
        public string pincode { get; set; }

        [Display(Name = "Phone No.")]
        public string Phone_no { get; set; }

        [Display(Name = "Org Category")]
        public string Org_category { get; set; }

        [Display(Name = "Org Status")]
        public string Org_Status { get; set; }
    }
}
