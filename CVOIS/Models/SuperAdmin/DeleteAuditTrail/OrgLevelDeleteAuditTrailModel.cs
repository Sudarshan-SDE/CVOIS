using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin.DeleteAuditTrail
{
    public class OrgLevelDeleteAuditTrailModel
    {
        [Key]
        [Display(Name = "S No.")]
        public int AuditID { get; set; }

        [Display(Name = "Org Level Id")]
        public int OrgLevel_Id { get; set; }


        [Display(Name = "OrgLevel Code")]
        public string OrgLevel_Code { get; set; }

        [Display(Name = "OrgLevel Name")]
        public string OrgLevel_Name { get; set; }


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
