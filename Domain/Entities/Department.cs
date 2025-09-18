using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSApi.Domain.Entities
{
    public class Department : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string DeptName { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
