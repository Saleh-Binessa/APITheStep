using APITheStep.Models.DB;
using APITheStep.Models.JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APITheStep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenService service;
        private readonly BankContext _context;

        public LoginController(TokenService service, BankContext _context)
        {
            this.service = service;
            this._context = _context;
        }

        
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var response = service.GenerateToken(username, password);

            if (!response.IsValid)
            {
                return BadRequest("Username and/or Password is wrong");
            }
            else
            {
                return Ok(new { Token = response.Token });
            }
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRegistration userRegistration)
        {
            bool isAdmin = false;
            if (_context.Users.Count() == 0)
            {
                isAdmin = true;
            }

            var newAccount = UserAccount.Create(userRegistration.Username, userRegistration.Password, isAdmin);
            
            _context.Users.Add(newAccount);
            _context.SaveChanges();

            return Ok(new { Message = "User Created" });
        }
    }
}
