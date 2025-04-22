using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace CVOIS.Models.Admin
{
    public class StaticDropdownModel
    {
        public string SelectedStatus { get; set; }
        public List<SelectListItem> StatusList { get; set; }
    }
}
