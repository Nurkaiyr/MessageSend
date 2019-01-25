using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Models;
using Services.Abstract;
using Services;
using DTO;

namespace MessageSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageContext _context;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        public MessagesController(MessageContext context, IEmailService emailService, ISmsService smsService)
        {
            _context = context;
            _emailService = emailService;
            _smsService = smsService;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _context.Messages.ToListAsync();
            return Ok(messages);
        }
        [HttpPost("sendemail")]
        public async Task<IActionResult> SendMessage([FromBody]EmailDTO emailDTO)
        {
            //EmailService emailService = new EmailService();
            await _emailService.SendEmailAsync(emailDTO.To, emailDTO.Subject, emailDTO.Text);
            _context.Messages.Add(new Models.Message
            {
                To = emailDTO.To,
                Text = emailDTO.Text,
                Subject = emailDTO.Subject,
                SendDate = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SendSms([FromBody]SmsDTO smsDTO)
        {
            await _smsService.SendSmsAsync(smsDTO.From, smsDTO.To, smsDTO.Text);
            _context.Messages.Add(new Models.Message
            {
                To = smsDTO.To,
                From = smsDTO.From,
                Text = smsDTO.Text,
                SendDate = DateTime.Now
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // PUT: api/Messages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage([FromRoute] int id, [FromBody] Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.Id)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}