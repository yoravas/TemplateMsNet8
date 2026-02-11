using MS00000_TemplateApi.Configurations.AppSettings;
using MS00000_TemplateApi.Customizations.Consts;
using MS00000_TemplateApi.Customizations.Helpers;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace MS00000_TemplateApi;

public class Program
{

    public static void Main(string[] args)
    {
        SetSelfLogSerilog();

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        PlaceholdersComfig.SetPlaceholderConfig(builder);


        SetSerilog(builder);
        builder.Host.UseSerilog();


        Startup startup = new(builder.Configuration, builder.Environment);
        startup.ConfigureServices(builder.Services);

        WebApplication app = builder.Build();

        app.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = (diagCtx, httpCtx) =>
            {
                diagCtx.Set(SerilogColumCustom.CorrelationId, Ulid.NewUlid().ToString());
                diagCtx.Set(SerilogColumCustom.RequestPath, $"{httpCtx.Request.Method} {httpCtx.Request.Path}");
                diagCtx.Set(SerilogColumCustom.Metodo, LoggerInfoHelper.LogUsedItemInfo());
            };

        });

        startup.Configure(app);

        try
        {
            Log.Information("Starting web application");
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");

        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void SetSerilog(WebApplicationBuilder builder)
    {
        // =========================
        //  CONFIGURAZIONE SERILOG
        // =========================

        LogEventLevel logLevelSwitch = LogEventLevel.Error;

        if (Enum.TryParse(builder.Configuration.GetValue<string>("SerilogLevel"), out LogEventLevel logLevel))
        {
            logLevelSwitch = logLevel;
        }

        ColumnOptions columnOptions = new()
        {
            Store = new Collection<StandardColumn>
            {
                StandardColumn.Id,
                StandardColumn.TimeStamp,
                StandardColumn.Message,
                StandardColumn.MessageTemplate,
                StandardColumn.Level,
                StandardColumn.Exception,
                StandardColumn.Properties
            }
        };

        columnOptions.AdditionalColumns = new Collection<SqlColumn>
        {
            new SqlColumn(SerilogColumCustom.CorrelationId, SqlDbType.Char) { DataLength = 26, AllowNull = false },
            new SqlColumn(SerilogColumCustom.Metodo, SqlDbType.NVarChar) { DataLength = 1000, AllowNull = false },
            new SqlColumn(SerilogColumCustom.RequestPath, SqlDbType.NVarChar) { DataLength = -1, AllowNull = true },
            new SqlColumn(SerilogColumCustom.AdditionalData, SqlDbType.NVarChar) { DataLength = -1, AllowNull = true },
            new SqlColumn(SerilogColumCustom.FilePath, SqlDbType.NVarChar) { DataLength = -1, AllowNull = true }
        };

        string connectionString = builder.Configuration.GetSection("ConnectionStrings").GetSection("LogDatabase").GetValue<string>("Connection");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
            .MinimumLevel.Override("System", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "TemplateApi")
            .Enrich.WithProperty("Environment", "Production")

            // Default globale per evitare NULL in AdditionalColumns non-nullable
            .Enrich.WithProperty(SerilogColumCustom.CorrelationId, Ulid.NewUlid().ToString())        // valore di default
            .Enrich.WithProperty(SerilogColumCustom.Metodo, LoggerInfoHelper.LogUsedItemInfo())

            // Console (solo Error)
            .WriteTo.Console(
                restrictedToMinimumLevel: LogEventLevel.Error,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}"
            )

            // SQL Server tramite Async
            .WriteTo.Async(a => a.MSSqlServer(

                connectionString: connectionString,
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "TMP_ApiLogEvents",
                    SchemaName = "dbo",
                    AutoCreateSqlTable = true,
                    BatchPostingLimit = 50,
                    BatchPeriod = TimeSpan.FromSeconds(2)
                },
                restrictedToMinimumLevel: logLevelSwitch,
                columnOptions: columnOptions
            ))
            .CreateLogger();



    }
    private static void SetSelfLogSerilog() => SelfLog.Enable(msg =>
    {
        Console.Error.WriteLine($"[SERILOG-SELFLOG] {msg}");
        try
        {
            File.AppendAllText("serilog-selflog.txt",
                $"[{DateTime.Now:O}] {msg}{Environment.NewLine}");
        }
        catch
        {
            Console.Error.WriteLine("[SERILOG-SELFLOG] Unable to write to serilog-selflog.txt");
        }
    });





}
