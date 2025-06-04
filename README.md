# SQL Server Schema Extractor

A .NET tool for extracting SQL Server database schemas and serializing them to JSON or YAML. 
Designed for reverse engineering, documentation, and seamless integration with natural language to SQL (NL2SQL) systems powered by large language models (LLMs).

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

2. **Run the Extractor**

   Build and run the `SqlSchemaProviderHarnessTester` project:
   dotnet run --project SqlSchemaProviderHarnessTester

   The tool will connect to your database, extract the schema, and print the result as JSON.
