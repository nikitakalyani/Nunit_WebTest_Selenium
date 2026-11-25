using Nunit_WebTest_Selenium;

[SetUpFixture]
public class AssemblyHooks
{
    [OneTimeTearDown]
    public void AfterAllTests()
    {
        var logPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "ExecutionLog.txt");
        File.WriteAllLines(logPath, TestReportTracker.LogLines);

        if (bool.Parse(TestContext.Parameters.Get("SendEmailReport", "false")))
        {
            EmailSender.SendRunReport(logPath);
        }

        var reportPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "ExecutionReport.html");
        //File.WriteAllText(reportPath, htmlContent);
    }
}
