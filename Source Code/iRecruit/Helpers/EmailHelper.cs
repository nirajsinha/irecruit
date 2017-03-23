using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using iRecruit.Repository;
using iRecruit.Entity;

namespace iRecruit.Helpers
{
    public class EmailHelper
    {
        public EmailMessage emailMessage = null;
        public void Send(EmailMessage emailMessage, List<string> fileAttachments, string calAttachment, bool isTrans = true)
        {

            using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage())
            {
                message.From = new System.Net.Mail.MailAddress(emailMessage.From ?? "irecruit@ihealthtechnologies.com");
                message.Subject = emailMessage.Subject;
                message.Body = emailMessage.Body;
                string[] toes = emailMessage.To.Split(',');
                foreach (string t in toes)
                {
                    if (!string.IsNullOrWhiteSpace(t.Trim()))
                    {
                        message.To.Add(t.Trim());
                    };
                };
                if (!string.IsNullOrWhiteSpace(emailMessage.Cc))
                {
                    string[] cces = emailMessage.Cc.Split(',');
                    foreach (string c in cces)
                    {
                        if (!string.IsNullOrWhiteSpace(c.Trim()))
                        {
                            message.CC.Add(c.Trim());
                        };
                    };
                }
                message.IsBodyHtml = true;
                // add attachments if any
                if (fileAttachments != null)
                {
                    foreach (string file in fileAttachments)
                    {
                        message.Attachments.Add(new System.Net.Mail.Attachment(file));
                    }
                }
                // add calender attachments if any
                if (!string.IsNullOrWhiteSpace(calAttachment))
                {
                    message.Attachments.Add(new System.Net.Mail.Attachment(calAttachment, "text/calendar"));
                }
                using (System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient())
                {
                    // Passing values to smtp object


                    try
                    {
                        smtp.Send(message);
                        if (isTrans)
                        {
                            try
                            {
                                new MasterRepository().SaveEmailNotifications(
                                    new EmailNotifications()
                                    {
                                        EmailFrom = message.From.Address,
                                        EmailTo = emailMessage.To,
                                        EmailCc = emailMessage.Cc,
                                        Subject = message.Subject,
                                        BodyHtml = message.Body,
                                        Status = 1
                                    });
                            }
                            catch { }
                        }
                    }
                    catch
                    {
                        // if email send fails
                        if (isTrans)
                        {
                            try
                            {
                                new MasterRepository().SaveEmailNotifications(
                                    new EmailNotifications()
                                    {
                                        EmailFrom = message.From.Address,
                                        EmailTo = emailMessage.To,
                                        EmailCc = emailMessage.Cc,
                                        Subject = message.Subject,
                                        BodyHtml = message.Body,
                                        Status = 0
                                    });
                            }
                            catch { }
                        }
                        throw;
                    }
                }
            }

        }
        public void CreateCalenderEvent(string evtLocation, string evtSubject, string evtDescription, DateTime evtBeginTime, DateTime evtEndTime, ref string returnIcsFile)
        {
            if (!string.IsNullOrWhiteSpace(returnIcsFile) && returnIcsFile.Contains(".ics"))
            {
                //PUTTING THE MEETING DETAILS INTO AN ARRAY OF STRING
                String[] contents = { "BEGIN:VCALENDAR",
                                  "PRODID:-//Flo Inc.//FloSoft//EN",
                                  "BEGIN:VEVENT",
                                  "DTSTART:" + evtBeginTime.ToString("yyyyMMdd\\THHmmss\\Z"), 
                                  "DTEND:" + evtEndTime.ToString("yyyyMMdd\\THHmmss\\Z"), 
                                  "LOCATION:" + evtLocation, 
	                              "DESCRIPTION;ENCODING=QUOTED-PRINTABLE:" + evtDescription,
                                  "SUMMARY:" + evtSubject, 
                                  "PRIORITY:3", 
	                              "END:VEVENT", 
                                  "END:VCALENDAR" };
                System.IO.File.WriteAllLines(returnIcsFile, contents);
            }
        }
    }
    public class EmailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
   
    }

}
