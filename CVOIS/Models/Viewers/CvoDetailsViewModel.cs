using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVOIS.Models.Viewers
{
    public class CvoDetailsViewModel
    {
        public Search_GetCvoDetails_Model Filters { get; set; }
        public List<CVO_Details_Model> CvoDetailsList { get; set; }

        // Dropdown lists
        public List<SelectListItem> AppointingAuthorityList { get; set; }
        public List<SelectListItem> LevelList { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> ServiceList { get; set; }
        public List<SelectListItem> CVOList { get; set; }
        public List<SelectListItem> MinistryList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> OrganisationList { get; set; }
        public List<SelectListItem> OrganisationCategoryList { get; set; }
    }
}
