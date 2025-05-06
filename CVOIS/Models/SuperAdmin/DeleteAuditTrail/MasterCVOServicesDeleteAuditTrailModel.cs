using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin.DeleteAuditTrail
{
    public class MasterCVOServicesDeleteAuditTrailModel
    {
        [Key]
        [Display(Name = "S No.")]
        public int AuditID { get; set; }

        [Display(Name = "MasterCvoServices Id")]
        public int MasterCvoServices_Id { get; set; }


        [Display(Name = "MasterCvoServices Code")]
        public string MasterCvoServices_Code { get; set; }

        [Display(Name = "MasterCvoServices Name")]
        public string MasterCvoServices_Name { get; set; }


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
