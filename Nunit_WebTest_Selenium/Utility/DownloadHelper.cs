using System;
using System.Collections.Generic;
using System.Text;

namespace Nunit_WebTest_Selenium.Utility
{
    public class DownloadHelper
    {
        public static bool WaitForFileDownload(string folder, string fileExtension, int timeoutSec = 15)
        {
            var end = DateTime.Now.AddSeconds(timeoutSec);
            while (DateTime.Now < end)
            {
                var files = Directory.GetFiles(folder, $"*{fileExtension}")
                                     .Where(f => !f.EndsWith(".crdownload") && !f.EndsWith(".tmp"));
                if (files.Any()) return true;
                Thread.Sleep(500);
            }
            return false;
        }
    }
}
