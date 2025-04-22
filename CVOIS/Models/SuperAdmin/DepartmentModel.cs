using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin
{
    public class DepartmentModel
    {
        [Key]
        [Display(Name = "Dept Id")]
        public int Dept_Id { get; set; }

        [Display(Name = "Dept Code")]
        public string Deptcode { get; set; }

        [Display(Name = "Ministry Name")]
        public string Mincode { get; set; }

        [Display(Name = "Dept Name")]
        public string DeptName { get; set; }

        [Display(Name = "Status")]
        public string Dept_status { get; set; }



        [Display(Name = "S. No")]
        public int row_num { get; set; }

        [Display(Name = "Ministry Name")]
        public string Ministry_Name { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByIP { get; set; }
        public string SessionID { get; set; }

    }
}
