using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITheStep.Models.Employee
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CivilId { get; set; }
        [Required]
        public string Position { get; set; }
        public int WorkplaceId { get; set; }
        public BankBranch.BankBranch Workplace { get; set; }
    }
}


