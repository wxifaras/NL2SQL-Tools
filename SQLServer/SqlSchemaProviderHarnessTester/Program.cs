using SqlDbSchemaExtractor.Schema;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Local.json", optional: false, reloadOnChange: true)
            .Build();

var connectionString = configuration["DatabaseConnection"];
var databaseDescription = configuration["DatabaseDescription"];
var tables = configuration["Tables"];

var sqlHarness = new SqlSchemaProviderHarness(connectionString, databaseDescription);
var tableNames = tables.Split("|");
var jsonSchema = await sqlHarness.ReverseEngineerSchemaJSONAsync(tableNames);

Console.WriteLine(jsonSchema);