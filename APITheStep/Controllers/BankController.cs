using APITheStep.Models.BankBranch;
using APITheStep.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace APITheStep.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly BankContext _context;

        public BankController(BankContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        [Authorize]
        public List<BankBranchResponse> GetAllBranches()
        {
            return _context.BankBranches.Select(b => new BankBranchResponse
            {
                BranchManager = b.BranchManager,
                Location = b.Location,
                Name = b.Name
            }).ToList();
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult BranchesDetails(int id)
        {
            var branch = _context.BankBranches.Find(id);
            if (branch == null)
            {
                return NotFound();
            }
            {
                var response = new BankBranchResponse
                {
                    BranchManager = branch.BranchManager,
                    Location = branch.Location,
                    Name = branch.Name
                };
                return Ok(response);
            }
        }


        [HttpPatch("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id, NewBranchRequest request)
        {
            var bank = _context.BankBranches.Find(id);
            bank.Name = request.Name;
            bank.BranchManager = request.BranchManager;
            bank.Location = request.Location;
            _context.SaveChanges();
            return Created(nameof(BranchesDetails), new { Id = bank.Id });

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            var bank = _context.BankBranches.Find(id);
            _context.BankBranches.Remove(bank);
            _context.SaveChanges();
            return Ok();

        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Add(NewBranchRequest request)
        {
            var newBank = (new BankBranch()
            {
                Name = request.Name,
                Location = request.Location,
                BranchManager = request.BranchManager,
                EmployeeCount = request.EmployeeCount,
            });
            _context.BankBranches.Add(newBank);
            _context.SaveChanges();
            return Created(nameof(BranchesDetails), new { Id = newBank.Id });
        }
    }
}