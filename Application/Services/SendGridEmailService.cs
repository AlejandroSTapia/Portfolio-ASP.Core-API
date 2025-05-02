using Application.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class SendGridEmailService : IEmailService
	{
		private readonly IResourcesRepository _resourcesRepository;

		public SendGridEmailService(IResourcesRepository resourcesRepository)
		{
			_resourcesRepository = resourcesRepository;
		}

		public async Task SendEmailAsync(string name, string email, string message)
		{
			var apiKey = await _resourcesRepository.GetValueAsync("apiKeySendGrindEmail");
			if (string.IsNullOrWhiteSpace(apiKey))
				throw new InvalidOperationException("API key no configurada.");

			var fromEmail = await _resourcesRepository.GetValueAsync("FromEmail");
			var fromName = await _resourcesRepository.GetValueAsync("FromName");
			var toEmail = await _resourcesRepository.GetValueAsync("ToEmail");
			var subject = await _resourcesRepository.GetValueAsync("DefaultSubject");

			var client = new SendGridClient(apiKey);
			var from = new EmailAddress(fromEmail, fromName);
			var to = new EmailAddress(toEmail);

			// Armar cuerpo del mensaje
			var fullMessage = $"Nombre: {name}\nCorreo: {email}\n\nMensaje:\n{message}";

			var msg = MailHelper.CreateSingleEmail(from, to, subject, fullMessage, fullMessage);
			await client.SendEmailAsync(msg);
		}

	}
}
