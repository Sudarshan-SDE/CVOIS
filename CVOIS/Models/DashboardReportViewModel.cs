using CVOIS.Models.Admin;

namespace CVOIS.Models
{
    public class DashboardReportViewModel
    {
        public IEnumerable<AdminViewRequestDetailsModel> Requests { get; set; } // For table data
        public StaticDropdownModel StatusFilter { get; set; } // For dropdown
    }
}
