namespace CVOIS.Models.Admin
{
    public class AdminViewRequestDetailsModel
    {
        public string MinName { get; set; }  // Ministry Name
        public string OrgName { get; set; }  // Organization Name
        public string ReqId { get; set; }  // Request ID
        public string RequestIssue { get; set; }  // Request Issue
        public string ReqComments { get; set; }  // Request Comments
        public string ApproverRemarks { get; set; }  // Approver Remarks
        public string ReqSendAdminFlag { get; set; }  // Request Sent to Admin Flag
        public DateTime? ReqVerificationDate { get; set; }  // Verification Date (nullable)
        public string RequestStatus { get; set; }  // Request Status

    }
}
