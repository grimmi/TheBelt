using Serilog;
using System;

namespace TheBelt
{
    public static class BeltSetup
    {
        public static void Init()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.SQLite(@"logs\belt.db", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug, storeTimestampInUtc: true)
                .WriteTo.RollingFile(@"logs\belt-{Date}.log", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
                .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
                .CreateLogger();

            Log.Error("error logging!");
        }

        public static void Teardown()
        {
            Log.CloseAndFlush();
        }
    }
}
