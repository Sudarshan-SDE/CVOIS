namespace CVOIS.Models.Admin
{
    public class AdminDashboardModel
    {
        public string? TotalRequestReceived { get; set; }
        public string? TotalRequestApprovedCount { get; set; }
        public string? TotalRequestRejected { get; set; }
        public string? TotalRequestPendingCount { get; set; }
    }
}
