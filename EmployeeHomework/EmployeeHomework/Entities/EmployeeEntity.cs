using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeHomework.Entities
{
    public class EmployeeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public DateTime EmploymentDate { get; set; }

        public int? BossId { get; set; }

        [Required]
        public string HomeAddress { get; set; }

        [Required]
        public decimal CurrentSalary { get; set; }

        [Required]
        public string Role { get; set; }

        public static EmployeeEntity From(Employee employee) => new EmployeeEntity
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Birthdate = employee.Birthdate,
            EmploymentDate = employee.EmploymentDate,
            BossId = employee.BossId,
            HomeAddress = employee.HomeAddress,
            CurrentSalary = employee.CurrentSalary,
            Role = employee.Role
        };
    }
}
