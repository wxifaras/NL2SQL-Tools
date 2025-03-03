﻿using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Threading.Tasks;

namespace SqlDbSchemaExtractor.Schema;

/// <summary>
/// Harness for utilizing <see cref="SqlSchemaProvider"/> to capture live database schema
/// definitions: <see cref="SchemaDefinition"/>.
/// </summary>
public sealed class SqlSchemaProviderHarness
{
    private string _connectionString;
    private string _databaseDescription;

    public SqlSchemaProviderHarness(string connectionString, string databaseDescription)
    {
        _connectionString = connectionString;
        _databaseDescription = databaseDescription;
    }

    public async Task<string> ReverseEngineerSchemaYAMLAsync(string[] tableNames)
    {
        string dbName = GetDatabaseName();
        var yaml = await this.CaptureSchemaYAMLAsync(dbName, _connectionString, _databaseDescription, tableNames).ConfigureAwait(false);

        return yaml;    
    }

    public async Task<string> ReverseEngineerSchemaJSONAsync(string[] tableNames)
    {
        string dbName = GetDatabaseName();
        var yaml = await this.CaptureSchemaJSONAsync(dbName, _connectionString, _databaseDescription, tableNames).ConfigureAwait(false);

        return yaml;
    }

    private async Task<string> CaptureSchemaYAMLAsync(string databaseKey, string? connectionString, string? description, params string[] tableNames)
    {
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync().ConfigureAwait(false);

        var provider = new SqlSchemaProvider(connection);

        var schema = await provider.GetSchemaAsync(databaseKey, description, tableNames).ConfigureAwait(false);

        await connection.CloseAsync().ConfigureAwait(false);

        var yamlText = await schema.FormatAsync(YamlSchemaFormatter.Instance).ConfigureAwait(false);
        
        return yamlText;

        // If you want to save to a file    
        //await this.SaveSchemaAsync("yaml", databaseKey, yamlText).ConfigureAwait(false);
        //await this.SaveSchemaAsync("json", databaseKey, schema.ToJson()).ConfigureAwait(false);
    }

    private async Task<string> CaptureSchemaJSONAsync(string databaseKey, string? connectionString, string? description, params string[] tableNames)
    {
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync().ConfigureAwait(false);

        var provider = new SqlSchemaProvider(connection);

        var schema = await provider.GetSchemaAsync(databaseKey, description, tableNames).ConfigureAwait(false);

        await connection.CloseAsync().ConfigureAwait(false);

        var yamlText = await schema.FormatAsync(YamlSchemaFormatter.Instance).ConfigureAwait(false);

        return schema.ToJson();

        // If you want to save to a file
        //await this.SaveSchemaAsync("yaml", databaseKey, yamlText).ConfigureAwait(false);
        //await this.SaveSchemaAsync("json", databaseKey, schema.ToJson()).ConfigureAwait(false);
    }

    private string GetDatabaseName()
    {
        var builder = new DbConnectionStringBuilder();
        builder.ConnectionString = _connectionString;
        object? databaseName;

        if (builder.TryGetValue("Initial Catalog", out databaseName) || builder.TryGetValue("Database", out databaseName))
        {
            return databaseName?.ToString() ?? string.Empty;
        }
        else
        {
            return string.Empty;
        }
    }
}