using System.ComponentModel.DataAnnotations;

namespace CVOIS.Models
{
    public class LoginModel
    {
        [Key]
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string ? Role { get; set; }
        public int ISLOCKEDCNT { get; set; }
        public string? User_Status { get; set; }
        public int Tempswd { get; set; }
        public string? TempPswdStatus { get; set; }
        public string? Temppass { get; set; }
        public string? Mobile_no { get; set; }
        public string? Mincode { get; set; }
        public string? Deptcode { get; set; }
        public DateTime? User_CreatedDate { get; set; }
        public DateTime? User_UpdatedDate { get; set; }
        public string? User_CreatedByIP { get; set; }
        public string? User_CreatedSession { get; set; }

       
    }
}
