using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace CMS.Common.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class BaseLoggerConfiguration
    {
        public static LoggerConfiguration CreateLoggerConfiguration(string applicationName)
        {
            LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .MinimumLevel.Override("System", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", applicationName);
            return loggerConfiguration;
        }

        public static LoggerConfiguration WriteToSql(this LoggerConfiguration loggerConfiguration, string connectionString)
        {
            MSSqlServerSinkOptions sinkOpts = new()
            {
                TableName = "Logs",
                AutoCreateSqlTable = true
            };

            ColumnOptions columnOptions = new()
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new() { ColumnName = "ApplicationName", DataType = SqlDbType.NVarChar, DataLength = 64, AllowNull = true },
                }
            };
            _ = columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.Store.Add(StandardColumn.LogEvent);
            columnOptions.TimeStamp.NonClusteredIndex = true;
            _ = loggerConfiguration.WriteTo.MSSqlServer(connectionString, sinkOpts, columnOptions: columnOptions);
            return loggerConfiguration;
        }
    }
}
