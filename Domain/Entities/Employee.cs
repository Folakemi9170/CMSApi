using System.ComponentModel.DataAnnotations.Schema;

namespace CMSApi.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Firstname { get; set; }
        public string? Middlename { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly DOB { get; set; }
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        //public string PasswordHash { get; set; }
        //public string? RefreshToken { get; set; }
        //public DateTime RefreshTokenExpiryTime { get; set; }

        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

    }
}
