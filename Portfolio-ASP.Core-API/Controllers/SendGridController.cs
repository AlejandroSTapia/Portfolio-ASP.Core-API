using Application.Interfaces;
using Domain.DTos;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio_ASP.Core_API.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class SendGridController : ControllerBase
	{
		private readonly IEmailService _emailService;
		public SendGridController(IEmailService emailService)
		{
			_emailService = emailService;
		}

		[HttpPost]
		[Route("SendEmail")]
		public async Task<IActionResult> SendEmail([FromBody] EmailRequestDto dto)
		{
			await _emailService.SendEmailAsync(dto.Name, dto.Email, dto.Message);
			return Ok("Email sent successfully");
		}
	}
	
}
