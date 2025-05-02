using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin.AuditTrail
{
    public class OrganizationAuditTrailModel
    {
        [Key]
        [Display(Name = "Audit Id")]
        public int auditID { get; set; }

        [Display(Name = "Details")]
        public string auditDetails { get; set; }

        [Display(Name = "Created By")]
        public string createdBy { get; set; }

        [Display(Name = "Created On")]
        public string createdOn { get; set; }


        [Display(Name = "Created By IP")]
        public string createdByIP { get; set; }

        [Display(Name = "Session ID")]
        public string sessionID { get; set; }


        [Display(Name = "Action Category")]
        public string actionCategory { get; set; }
    }
}
