using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Text;
using MailKit;

namespace Nunit_WebTest_Selenium
{
    public class EmailSender
    {
        public static void SendRunReport(string logFilePath)
        {
            var records = TestReportTracker.Results.ToList();
            if (records.Count == 0) return;

            int total = records.Count;
            int passed = records.Count(r => r.Status == "Passed");
            int failed = records.Count(r => r.Status != "Passed");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Automation Runner", TestContext.Parameters.Get("SmtpUser", "")));
            message.To.Add(MailboxAddress.Parse(TestContext.Parameters.Get("ReportToEmail", "")));
            message.Subject = $"Automation Report | Total: {total} | Passed: {passed} | Failed: {failed}";

            var sb = new StringBuilder();
            sb.Append($"<h3>Execution Summary (Total: {total}, Passed: {passed}, Failed: {failed})</h3>");
            sb.Append("<table border='1' cellpadding='6' cellspacing='0' style='border-collapse:collapse;font-family:Arial;'>");
            sb.Append("<tr style='background:#f2f2f2;'><th>Test Name</th><th>Status</th><th>Failure Reason</th></tr>");

            foreach (var r in records.OrderBy(x => x.TestName))
            {
                string color = r.Status == "Passed" ? "green" : "red";
                string reason = string.IsNullOrWhiteSpace(r.FailureReason) ? "-" : System.Net.WebUtility.HtmlEncode(r.FailureReason);
                sb.Append($"<tr><td>{r.TestName}</td><td style='color:{color};font-weight:bold;'>{r.Status}</td><td>{reason}</td></tr>");
            }
            sb.Append("</table>");

            var builder = new BodyBuilder { HtmlBody = sb.ToString() };

            // Attach execution log file
            if (File.Exists(logFilePath))
                builder.Attachments.Add(logFilePath);

            // Attach all failure screenshots
            foreach (var shot in records.Where(r => !string.IsNullOrEmpty(r.ScreenshotPath) && File.Exists(r.ScreenshotPath)))
            {
                builder.Attachments.Add(shot.ScreenshotPath!);
            }

            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            string host = TestContext.Parameters.Get("SmtpHost", "://gmail.com");
            int port = int.Parse(TestContext.Parameters.Get("SmtpPort", "587"));
            client.Connect(host, port, SecureSocketOptions.StartTls);
            client.Authenticate(
                TestContext.Parameters.Get("SmtpUser", ""),
                TestContext.Parameters.Get("SmtpPassword", "")
            );
            client.Send(message);
            client.Disconnect(true);
        }

    }
}
