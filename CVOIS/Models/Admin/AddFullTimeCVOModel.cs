using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.Admin
{
    public class AddFullTimeCVOModel
    {
        [Required(ErrorMessage = "Organization can not be null ")]
        public string Organisation { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile No.")]
        [DisplayName("Mobile No.")]        
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Please Select Tenure From date")]

        [DisplayName("Tenure From")]
        public DateTime? TenureFrom  { get; set; }

        [Required(ErrorMessage = "Please Select Tenure To date")]
        [DisplayName("Tenure To ")]
        public DateTime? TenureTo { get; set; }

        [Required(ErrorMessage ="Please Enter Email Id")]
        [DisplayName("Email Id ")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please Enter CVO Name")]
        [DisplayName("Name")]
        public string EnterCVOName { get; set; }

        public List<SelectListItem> _cvoname { get; set; } // For cvoname dropdown

        public string? SelectedOrg { get; set; }
        public List<SelectListItem> _Orgfilter { get; set; } // For Org dropdown
        public Charges_StaticDropdownModel _charges_Dropdown { get; set; }

        public Title_StaticDropdownModel _title_Dropdown { get; set; }

        public Gender_StaticDropdownModel _gender { get; set; }

        //service
        public string? SelectedService { get; set; }
        public List<SelectListItem> _servicesfilter { get; set; }

        //batch
        public string? SelectedBatch { get; set; }
        public List<SelectListItem> _batchfilter { get; set; }

        public Status_StaticDropdownModel _status { get; set; }

        public List<CheckOrgWithcvoModel> _checkorgwithcvo { get; set; }
    }
}
