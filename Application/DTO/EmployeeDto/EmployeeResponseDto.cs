namespace CMSApi.Application.DTO.EmployeeDto
{
    public class EmployeeResponseDto
    {
        public int? Id { get; set; }
        public string Fullname { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly DOB { get; set; }
        public string Role { get; set; }
        public int? DepartmentId { get; set; }
        public string? DeptName { get; set; }
        public bool IsActive { get; set; }

    }
}
