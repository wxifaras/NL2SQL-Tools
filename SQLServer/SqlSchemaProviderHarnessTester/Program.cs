using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using SqlDbSchemaExtractor;
using SqlDbSchemaExtractor.Schema;
using System.Text.Json;

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Local.json", optional: false, reloadOnChange: true)
            .Build();

var config = configuration.GetSection("Nl2SqlConfig").Get<Nl2SqlConfigRoot>();
var connectionString = configuration["DatabaseConnection"];

var sqlHarness = new SqlSchemaProviderHarness(connectionString, config.Database.Description);
var jsonSchema = await sqlHarness.ReverseEngineerSchemaJSONAsync(config).ConfigureAwait(false);

Console.WriteLine(jsonSchema);