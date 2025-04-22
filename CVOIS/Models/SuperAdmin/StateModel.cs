using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin
{
    public class StateModel
    {
        [Display(Name = "State Id")]
        public string state_id { get; set; }

        [Display(Name = "State Name")]
        public string state_name { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByIP { get; set; }
        public string SessionID { get; set; }
    }
}
