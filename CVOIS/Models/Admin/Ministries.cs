using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.Admin
{
    public class Ministries
    {
        [Display(Name = "Ministry ID")]
        public  int  Min_id { get; set; }

        [Display(Name = "Ministry Code")]
        public string Mincode{ get; set; }

        [Display(Name = "Ministry Name")]
        public string Ministry_Name{ get; set; }

        [Display(Name = "Ministry Status")]
        public string Min_Status{ get; set; }

        [Display(Name = "Ministry Type")]
        public string Ministry_Type{ get; set; }

        public List<Ministries> Ministry_List { get; set; } = new List<Ministries>();
    }
}
