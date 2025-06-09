# SQL Server Schema Extractor

A .NET library that extracts SQL Server database schemas and converts them to JSON. The JSON output is designed for use in prompts, allowing large language models (LLMs) to translate natural language queries into SQL statements.

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

## Additional Notes
Adding column descriptions is crucial for large language models (LLMs) to translate natural language queries into SQL statements accurately.

To add column descriptions to your database metadata, use the <b>sp_addextendedproperty</b> stored procedure.

### Example
```
EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'This column stores the user email address', 
    @level0type = N'SCHEMA', @level0name = 'dbo',
    @level1type = N'TABLE',  @level1name = 'Users',
    @level2type = N'COLUMN', @level2name = 'Email';
