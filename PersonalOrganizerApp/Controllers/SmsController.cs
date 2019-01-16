using System;
using System.Collections.Generic;
using Twilio.AspNet.Core;
using Twilio.AspNet.Common;
using Twilio.TwiML;
using Twilio;
using Twilio.Rest.Api.V2010.Account;


// not in use for the project
namespace PersonalOrganizerApp.Controllers
{
    public class SmsController : TwilioController
    {
        // twilio credentials
        private const string accountSid = "id_here";
        private const string authToken = "token_here";

        // string list of commands for bot
        public static List<string> commands = new List<string>
        {
            "commands (list of commands)",
            "reminders (list of reminders)",
            "time (current time)"
        };

        // handles incoming messages from users
        public TwiMLResult Index(SmsRequest incomingMessage)
        {
            MessagingResponse messagingResponse = new MessagingResponse();

            switch(incomingMessage.Body.ToLower().Trim())
            {
                case "reminders":
                    messagingResponse.Message("Work at 4:30PM");
                    break;
                case "commands":
                    messagingResponse.Message(CommandListToString());
                    break;
                case "time":
                    messagingResponse.Message(DateTime.Now.ToString());
                    break;
                case "hello":
                    messagingResponse.Message("Whats up dude?");
                    break;
                default:
                    messagingResponse.Message("No Command Exists");
                    break;
            }
            return TwiML(messagingResponse);
        }

        // sends text message with a string parameter of the message 
        // app wants to send
        public static void SendTextMessage(string _message)
        {
            TwilioClient.Init(accountSid, authToken);

            MessageResource message = MessageResource.Create(
                body: _message,
                from: new Twilio.Types.PhoneNumber("+16473622283"),
                to: new Twilio.Types.PhoneNumber("+19053349025")
            );
        }

        // formats commands string list into a messageable string
        public static string CommandListToString()
        {
            string list = "";
            foreach(var command in commands) {
                list += command + "\n";
            }
            return list;
        }
    }
}
