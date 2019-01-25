using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Chat.V1.Service.Channel;

namespace Services.Abstract
{
    public interface ISmsService
    {
        Task<Twilio.Rest.Api.V2010.Account.MessageResource> SendSmsAsync(string from, string to,string body);
    }
}
