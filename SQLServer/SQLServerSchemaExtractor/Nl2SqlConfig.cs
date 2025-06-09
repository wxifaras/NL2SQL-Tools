using System.Collections.Generic;

namespace SqlDbSchemaExtractor;

public class Nl2SqlConfigRoot
{
    public Nl2SqlConfig Database { get; set; }
}

public class Nl2SqlConfig
{
    public string Description { get; set; }
    public List<DbSchema> Schemas { get; set; }
}

public class DbSchema
{
    public string Name { get; set; }
    public List<string> Tables { get; set; }
}