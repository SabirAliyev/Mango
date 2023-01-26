using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;

namespace Mango.Utility;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Execute(email, subject, htmlMessage);
    }

    public async Task Execute(string email, string subject, string body)
    {
        MailjetClient client = new MailjetClient("e1466251588c9bd9c777b32094b9af6c", "5eaf4715e2a77d7e923364dbfe490751")
        {
            // Version = ApiVersion.V3_1,
        };
        MailjetRequest request = new MailjetRequest
        {
            Resource = Send.Resource,
        }
         .Property(Send.Messages, new JArray {
         new JObject {
          {
           "From",
           new JObject {
            {"Email", "sabir.aliyev.itdep@gmail.com"},
            {"Name", "Sabir"}
           }
          }, {
           "To",
           new JArray {
            new JObject {
             {
              "Email",
              email
             }, {
              "Name",
              "DotNetMastery"
             }
            }
           }
          }, {
           "Subject",
           subject
          }, {
           "HTMLPart",
           body
          }, 
         }
         });
        await client.PostAsync(request);
    }
}
