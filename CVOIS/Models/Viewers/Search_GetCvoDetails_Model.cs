using System.Runtime.InteropServices;

namespace CVOIS.Models.Viewers
{
    public class Search_GetCvoDetails_Model
    {
        public string appointingAuthority { get; set; }
        public string level { get; set; }
        public string org_category { get; set; }
        public DateTime? tenureFrom { get; set; }
        public DateTime? tenureTo { get; set; }
        public string org_state { get; set; }
        public string SERVICES { get; set; }
        public string CVO_NAME { get; set; }
        public string minCode { get; set; }
        public string deptCode { get; set; }
        public string orgCode { get; set; }
        public string charges { get; set; }
        public string phone { get; set; }
        public string EMAIL_ID { get; set; }

    }
}
