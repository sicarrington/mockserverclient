namespace MockServer.Client.Net.Models
{
    public sealed class SchemaValue
    {
        public string Type { get; private set; }
        public string Pattern { get; private set; }
        public string Format { get; private set; }

        public static SchemaValue Integer()
        {
            return new SchemaValue { Type = "integer" };
        }

        public static SchemaValue StringWithFormat(string format)
        {
            return new SchemaValue { Type = "string", Format = format };
        }

        public static SchemaValue StringWithPattern(string pattern)
        {
            return new SchemaValue { Type = "string", Pattern = pattern };
        }

        public static SchemaValue Uuid()
        {
            return new SchemaValue { Type = "string", Format = "uuid" };

        }
    }
}