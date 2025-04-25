using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.Admin
{
    public class VacantFullTimeCVOModel
    {
        [Display(Name = "S No.")]
        public int SNo { get; set; }

        [Display(Name = "CVO Name")]
        public string cvoName { get; set; }

        [Display(Name = "Org Name")]
        public string orgName { get; set; }

        [Display(Name = "Vacant From")]
        public string VacantFrom { get; set; }
        public List<VacantFullTimeCVOModel> VacantFullTimeCVO_List { get; internal set; }
    }
}
