using System.ComponentModel.DataAnnotations;

namespace HrCrud.Models
{
    public class Employee
    {
        public int? Id { get; set; }

        [Required, StringLength(20, MinimumLength = 5)]
        public string NIK { get; set; }
    
        [Required, StringLength(80)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(60)]
        public string PlaceOfBirth { get; set; } = string.Empty;

        [Required, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required, Range(1000000, 1000000000, ErrorMessage = "1 jt – 1 M")]
        public decimal BasicSalary { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public MaritalStatus MaritalStatus { get; set; }
    }
}