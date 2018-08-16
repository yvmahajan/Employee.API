using FluentNHibernate.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Employee.Entities
{
    public class EmployeeInfo
    {
        [Range(0, int.MaxValue, ErrorMessage = "Only numeric allowed.")]
        public int ID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Only alphanumeric allowed.")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Only alphanumeric allowed.")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Only alphanumeric allowed.")]
        public string Designation { get; set; }

        [Range(0, byte.MaxValue, ErrorMessage = "Only numeric allowed.")]
        public byte Age { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary value is not valid.")]
        public double Salary { get; set; }
    }

    public class EmployeeInfoMap : ClassMap<EmployeeInfo>
    {
        public EmployeeInfoMap()
        {
            Table("Employee");
            Id(t => t.ID).GeneratedBy.Identity().UnsavedValue(0);
            Map(t => t.FirstName).Column("FirstName").Not.Nullable();
            Map(t => t.LastName).Column("LastName").Not.Nullable();
            Map(t => t.Designation).Column("Designation");
            Map(t => t.Age).Column("Age");
            Map(t => t.Salary).Column("Salary");
        }
    }
}
