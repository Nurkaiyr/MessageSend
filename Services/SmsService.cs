using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Clients;
using Twilio.TwiML.Voice;
using System.Threading.Tasks;

namespace Services
{
    public class SmsService : ISmsService
    {
        public const string accountSid = "AC9f5e3a7163f6206897100515b37dc071";
        public const string authToken = "8755f55535d60db863d41419f68c66af";

        public async Task<MessageResource> SendSmsAsync(string from,string to,string body)
        {

            TwilioClient.Init(accountSid, authToken);
            var restClient = new TwilioRestClient(accountSid, authToken);

            var toPhoneNumber = new PhoneNumber(to);
            return await MessageResource.CreateAsync(toPhoneNumber, from: new PhoneNumber(from), body: body);

       //     var messages = MessageResource.Create(
       //     body: "This is the ship that made the Kessel Run in fourteen parsecs?",
       //     from: new Twilio.Types.PhoneNumber("+17755719556"),
       //     to: new Twilio.Types.PhoneNumber("+77024830828")
       //);
        }
    }
}
