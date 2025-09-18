namespace CMSApi.Application.DTO.EmployeeDto
{
    public class UpdateEmployeeDto
    {
        public string? Firstname { get; set; }
        public string? Middlename { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public int? DepartmentId { get; set; }

    }
}