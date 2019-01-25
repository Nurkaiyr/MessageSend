using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string title, string body);
    }
}
