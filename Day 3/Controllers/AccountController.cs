using Day_3.DTO;
using Day_3.Models;
using Day_3.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Day_3.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		IUserRepo userRepo;
		public AccountController(IUserRepo userRepo)
		{
			this.userRepo = userRepo;
		}
		[HttpPost]
		[Route("Login")]
		public IActionResult Login(string email ,string password)
		{
			Account account = userRepo.Authenticate(email, password);
			if (account!=null)
			{
				List<Claim> Uclaims = new List<Claim>
				{  
					new Claim(ClaimTypes.Email, email),
					new Claim(ClaimTypes.Role, account.Role)
				};
				string key ="If YOU enter I will get in trouble";
				var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
				var signCred =new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
				var token = new JwtSecurityToken(
					claims: Uclaims,
					expires: DateTime.Now.AddDays(5),
					signingCredentials: signCred
					);
				return Ok(new JwtSecurityTokenHandler().WriteToken(token));
			}
			else
			{
				return Unauthorized();
			}
		}
	}
}
