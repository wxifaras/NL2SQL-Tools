﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace SqlDbSchemaExtractor.Schema;

public static class SchemaDefinitionExtensions
{
    public static async Task<string> FormatAsync(this SchemaDefinition schema, ISchemaFormatter formatter)
    {
        formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));

        using var writer = new StringWriter();

        await formatter.WriteAsync(writer, schema).ConfigureAwait(false);

        return writer.ToString();
    }
}
