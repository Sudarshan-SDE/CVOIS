using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.Admin
{
    public class Charges_StaticDropdownModel
    {
        [Required(ErrorMessage ="Please Select Charges")]
        [DisplayName("Charges")]
        
        public string SelectedCharges { get; set; }
        public List<SelectListItem> ChargesList { get; set; }

    }
    public class Title_StaticDropdownModel
    {
        [DisplayName("Titile")]
        [Required(ErrorMessage = "Please Select Titile")]
        public string SelectedTitle { get; set; }
        public List<SelectListItem> TilteList { get; set; }

    }

    public class Gender_StaticDropdownModel
    {
        [DisplayName("Gender")]
        [Required(ErrorMessage = "Please Select Gender")]
        public string SelectedGender { get; set; }
        public List<SelectListItem> GenderList { get; set; }

    }

    public class Status_StaticDropdownModel
    {
        [DisplayName("Status")]
        [Required(ErrorMessage = "Please Select Status")]
        public string SelectedStatus { get; set; }
        public List<SelectListItem> StatusList { get; set; }

    }
}
