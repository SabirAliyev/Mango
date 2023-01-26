﻿using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;

namespace Mango.Utility;

public class EmailSender : IEmailSender
{
    public readonly IConfiguration _configuration;
    public MailJetSettings _mailJetSettings { get; set; }

    public EmailSender(IConfiguration configuration, MailJetSettings mailJetSettings)
    {
        _configuration = configuration;
        _mailJetSettings = mailJetSettings;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Execute(email, subject, htmlMessage);
    }

    public async Task Execute(string email, string subject, string body)
    {
        // Get mail seettings from json file
        _mailJetSettings = _configuration.GetSection("MailJet").Get<MailJetSettings>();

        MailjetClient client = new MailjetClient(_mailJetSettings.ApiKey, _mailJetSettings.SecretKey);
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
            {"Email", "mangowallpapers@proton.me"},
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
