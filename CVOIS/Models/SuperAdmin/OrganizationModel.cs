using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models.SuperAdmin
{
    public class OrganizationModel
    {
        [Key]
        [Display(Name = "Org ID")]
        public int ORG_ID { get; set; }
        public int row_num { get; set; }

        [Display(Name = "Org Code")]
        public string ORGCODE { get; set; }

        [Display(Name = "Ministry Name")]
        public string MINCODE { get; set; }

        [Display(Name = "Department Name")]
        public string? DeptName { get; set; }

        [Display(Name = "Department Name")]
        public string? DEPTCODE { get; set; }




        [Display(Name = "Ministry Name")]
        public string Ministry_Name { get; set; }

        

       

        [Display(Name = "Org Name")]
        public string ORGNAME { get; set; }

        [Display(Name = "File No")]
        public string file_no { get; set; }

        [Display(Name = "Appointing Authority")]
        public string APPOINTING_AUTHORITY { get; set; }

        [Display(Name = "Org Level")]
        public string org_level { get; set; }




        [Display(Name = "Section")]
        public int? section { get; set; }

        [Display(Name = "Status")]
        public string org_status { get; set; }

        [Display(Name = "Country")]
        public string org_country { get; set; }

        [Display(Name = "Address")]
        public string org_address { get; set; }



        [Display(Name = "State")]
        public string org_state { get; set; }

        [Display(Name = "District")]
        public string org_district { get; set; }

        [Display(Name = "Pincode")]
        public string pincode { get; set; }

        [Display(Name = "Phone No")]
        public string phoneno { get; set; }



        [Display(Name = "Fax")]
        public string fax { get; set; }

        [Display(Name = "Org Catagory")]
        public string org_category { get; set; }

        [Display(Name = "Email Id")]
        public string EMAIL_ID { get; set; }



        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime created_date { get; set; }

        [Display(Name = "Update Date")]
        [DataType(DataType.Date)]
        public DateTime UPDATE_DATE { get; set; }

        public string ddlDeptCode { get; set; }
        public string ddldistrictCode { get; set; }


        public string CreatedBy { get; set; }
        public string CreatedByIP { get; set; }
        public string SessionID { get; set; }
        public string actionCategory { get; set; }




    }
}
