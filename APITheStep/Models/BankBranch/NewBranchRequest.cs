using System.ComponentModel.DataAnnotations;

namespace APITheStep.Models.BankBranch
{
    public class NewBranchRequest
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string BranchManager { get; set; }

        public int EmployeeCount { get; set; }
    }
}

