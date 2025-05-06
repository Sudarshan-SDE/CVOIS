using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin.DeleteAuditTrail
{
    public class AppointingAuthorityDeleteAuditTrailModel
    {
        [Key]
        [Display(Name = "S No.")]
        public int AuditID { get; set; }

        [Display(Name = "Appointing Auth Id")]
        public int AppointingAuthority_Id { get; set; }


        [Display(Name = "Appointing Auth Code")]
        public string AppointingAuthority_Code { get; set; }

        [Display(Name = "Appointing Auth Name")]
        public string AppointingAuthority_Name { get; set; }


        [Display(Name = "Create By")]
        public string createdBy { get; set; }

        [Display(Name = "createdBy IP")]
        public string createdByIP { get; set; }

        [Display(Name = "Session ID")]
        public string SessionID { get; set; }

        [Display(Name = "Deleted On")]
        public string DeletedOn { get; set; }
    }
}
