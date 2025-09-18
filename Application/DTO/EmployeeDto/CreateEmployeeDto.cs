namespace CMSApi.Application.DTO.EmployeeDto
{
    public class CreateEmployeeDto
    {
        public required string Firstname { get; set; }
        public string? Middlename { get; set; }
        public  string Lastname { get; set; }
        public string Gender { get; set; }
        public string Email {  get; set; }
        public string Phone { get; set; }
        public DateOnly DOB { get; set; }
        public int DepartmentId { get; set; }
        public string Role { get; set; }
        
        //public required string Password { get; set; }
    }
}
