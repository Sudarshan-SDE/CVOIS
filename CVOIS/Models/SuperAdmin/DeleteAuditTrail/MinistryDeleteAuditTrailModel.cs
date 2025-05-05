using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin.DeleteAuditTrail
{
    public class MinistryDeleteAuditTrailModel
    {
        [Key]
        [Display(Name = "S No.")]
        public int AuditID { get; set; }



        [Display(Name = "Min Id")]
        public string Min_Id { get; set; }

        [Display(Name = "Mincode")]
        public string Mincode { get; set; }

        [Display(Name = "Ministry Name")]
        public string Ministry_Name{ get; set; }

        [Display(Name = "Min Status")]
        public string Min_Status { get; set; }

        [Display(Name = "Ministry Type")]
        public string MinistryType{ get; set; }


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
