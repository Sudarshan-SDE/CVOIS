using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin.DeleteAuditTrail
{
    public class StateDeleteAuditTrailModel
    {
        [Key]
        [Display(Name = "S No.")]
        public int AuditID { get; set; }

        [Display(Name = "State Id")]
        public string state_id { get; set; }


        [Display(Name = "State Name")]
        public string state_name { get; set; }

       
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
