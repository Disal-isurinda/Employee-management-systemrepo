using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Hosting;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmailController : ApiController
    {
        public IHttpActionResult PostLeaveApplyMail(LeaveApply leaveapply)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            string subject = "Employee No. " + leaveapply.EmpID.ToString() + " - Leave Apply Request";
            string body = "Hi,<br/> Employee: " + leaveapply.EmpID.ToString() + " is requesting for a leave.<br/> Leave Type: " + leaveapply.LeaveTypeID + "<br/> Leave From: " + leaveapply.LeaveFrom + " <br/>Leave To: " + leaveapply.LeaveTo + "<br/> Leave Description: " + leaveapply.Description;
            // string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplates/LeaveApplyTMP.html"));
            body = body.Replace("{0}", "TEST");
            string to = "uldindrajith@yahoo.com";
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(smtpSection.From, "Employee Management System");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(smtpSection.Network.Host);
            smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
            smtp.Port = smtpSection.Network.Port;
            smtp.EnableSsl = smtpSection.Network.EnableSsl;
            //smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailUser"], ConfigurationManager.AppSettings["MailPW"]);
            smtp.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
            smtp.Send(mail);
            return Ok();
        }


    }
}
