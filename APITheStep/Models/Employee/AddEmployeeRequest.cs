using System.ComponentModel.DataAnnotations;

namespace APITheStep.Models.Employee
{
    public class AddEmployeeRequest
    {
        public string Name { get; set; }

        public string CivilId { get; set; }

        public string Position { get; set; }
        public int WorkplaceId { get; set; }
    }
}
