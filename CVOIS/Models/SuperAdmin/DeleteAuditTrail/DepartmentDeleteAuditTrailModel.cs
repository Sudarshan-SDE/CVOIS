using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin.DeleteAuditTrail
{
    public class DepartmentDeleteAuditTrailModel
    {
        [Key]
        [Display(Name = "S No.")]
        public int AuditID { get; set; }

        [Display(Name = "Dept Id")]
        public string Dept_Id { get; set; }

        [Display(Name = "Dept Code")]
        public string Deptcode { get; set; }

        [Display(Name = "Mincode")]  
        public string Mincode { get; set; }

        [Display(Name = "Dept Name")]
        public string DeptName { get; set; }

        [Display(Name = "Dept Status")]
        public string Dept_status { get; set; }

        [Display(Name = "Session ID")]
        public string SessionID { get; set; }

        [Display(Name = "Deleted On")]
        public string DeletedOn { get; set; }

        [Display(Name = "Created By")]
        public string createdBy { get; set; }

        [Display(Name = "Created By IP")]
        public string createdByIP { get; set; }
    }
}
