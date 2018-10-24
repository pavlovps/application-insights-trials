using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace app_insights_test
{
    class Program
    {
        private class MyClass
        {
            public string FirstCase { get; set; }
            public int FirstCaseAppereances { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hi!");

            var telemetry = new TelemetryClient();
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.ApplicationInsightsEvents(telemetry)
                .CreateLogger();

            log.Information("Serilog is in action!");

            for (int i = 0; i < 10; i++)
            {
                var caseToShow = new MyClass
                {
                    FirstCase = "Case_" + i.ToString(),
                    FirstCaseAppereances = i
                };

                log.Warning("Beware of {@case}", caseToShow);
                telemetry.TrackEvent("SerilogGame");
            }

            Console.ReadLine();
        }
    }
}
