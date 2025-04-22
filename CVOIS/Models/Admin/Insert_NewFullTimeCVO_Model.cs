using System.Runtime.InteropServices;

namespace CVOIS.Models.Admin
{
    public class Insert_NewFullTimeCVO_Model
    {
       public string? TITLE  { get; set; }
       public string? CVO_NAME { get; set; }
       public string? DESIGNATION { get; set; } 
       public string? GENDER  { get; set; } 
       public string? ORGCODE { get; set; } 
       public string? SERVICES { get; set; } 
       public DateTime TENURE_FROM  { get; set; }
       public DateTime TENURE_TO { get; set; }
        public string? PHONE_NO { get; set; }
        public string? BATCH { get; set; }
       public string? CHARGES { get; set; }
        public string? EMAIL_ID { get; set; }
        public string? CVO_STATUS { get; set; }
    }
}
