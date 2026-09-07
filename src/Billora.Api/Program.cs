using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog((_, configuration) =>
    configuration.ReadFrom.Configuration(builder.Configuration));

var conStr = builder.Configuration.GetConnectionString("Database");
if (string.IsNullOrEmpty(conStr))
{
    throw new InvalidOperationException("Could not find connection string 'Database'.");
}

builder.Services.AddHealthChecks()
    .AddSqlServer(
    connectionString: conStr,
    name: "sqlserver",
    tags: ["ready"]);

var app = builder.Build();

app.UseSerilogRequestLogging();

app.MapGet("/", () => "Hello World!");

app.MapHealthChecks("/health", new() { Predicate = _ => false });
app.MapHealthChecks("/health/ready", new() { Predicate = check => check.Tags.Contains("ready") });

await app.RunAsync();
