using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Nunit_WebTest_Selenium
{
    public static class TestReportTracker
    {
        public record TestExecutionRecord(
    string TestName,
    string Status,
    string? FailureReason,
    string? ScreenshotPath
);


        public static ConcurrentBag<TestExecutionRecord> Results { get; } = new();
        public static ConcurrentBag<string> LogLines { get; } = new();

        public static void Log(string message)
        {
            LogLines.Add($"[{DateTime.Now:HH:mm:ss}] [{Environment.CurrentManagedThreadId}] {message}");
        }

    }
}
