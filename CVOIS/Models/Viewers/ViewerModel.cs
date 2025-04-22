using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVOIS.Models.Viewers
{
    //public class ViewerModel
    //{

    //    public string AppointingAthority { get; set; }
    //    public string State { get; set; }
    //    public string Level { get; set; }

    //    public IEnumerable<SelectListItem> appointing_authority { get; set; }
    //    public IEnumerable<SelectListItem> state { get; set; }
    //    //public IEnumerable<SelectListItem> level { get; set; }

    //}

    public class ViewerModel
    {
        public string AppointingAuthority { get; set; }
        public string Level { get; set; }
        public string State { get; set; }
        public string Service { get; set; }
        public string CVO { get; set; }
        public string Ministry { get; set; }
        public string Department { get; set; }
        public string Organisation { get; set; }
        public string OrganisationCategory { get; set; }
        public string Tenurefrom { get; set; }
        public string TenureTo { get; set; }

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
