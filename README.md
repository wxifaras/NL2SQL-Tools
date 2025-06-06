# SQL Server Schema Extractor

A .NET library for extracting SQL Server database schemas and serializing them to JSON or YAML. 
The primary purpose of the JSON output is to inject the database schema directly into prompts for natural language to SQL (NL2SQL).

## Features

- Extracts schema from SQL Server databases.
- Outputs schema in JSON or YAML format.
- Configurable via `appsettings.json`.

## Usage

1. **Configure Connection and Schema**

   Edit `appsettings.json` (or `appsettings.Local.json`) to set your database connection and schema extraction options:
   ```json
      "DatabaseConnection": "your-connection-string-here",
      "Nl2SqlConfig": {
        "database": {
        "description": "Your database description",
        "schemas": [
          {
            "name": "SchemaName",
            "tables": [
              "Table1",
              "Table2"
            ]
          }
        ]
      }
    }

2. **Usage**
   ```
   var config = configuration.GetSection("Nl2SqlConfig").Get<Nl2SqlConfigRoot>();
   var connectionString = configuration["DatabaseConnection"];

   var sqlHarness = new SqlSchemaProviderHarness(connectionString, config.Database.Description);
   var jsonSchema = await sqlHarness.ReverseEngineerSchemaJSONAsync(config).ConfigureAwait(false);
