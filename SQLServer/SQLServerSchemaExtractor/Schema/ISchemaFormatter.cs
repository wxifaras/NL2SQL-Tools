using System.IO;
using System.Threading.Tasks;

namespace SqlDbSchemaExtractor.Schema;

public interface ISchemaFormatter
{
    Task WriteAsync(TextWriter writer, SchemaDefinition schema);
}
