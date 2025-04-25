using System.ComponentModel;

using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.Admin
{
    public class DepartmentsModel
    {
        [Display(Name = "S No.")]
        public int  SNo  { get; set; }

        public int row_num{ get; set; }

        [Display(Name = "Ministry Name")] 
        public string Ministry_Name{ get; set; }

        [Display(Name = "Dept Name")]
        public string DeptName{ get; set; }

        [Display(Name = "Dept Status")]
        public string Dept_status { get; set; }
        public List<DepartmentsModel> Department_List { get; internal set; }
    }
}
