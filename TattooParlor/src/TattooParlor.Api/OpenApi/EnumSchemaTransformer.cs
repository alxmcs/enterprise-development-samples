using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System.Text;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace TattooParlor.Api.OpenApi;

/// <summary>
/// Дополняет схемы enum-ов: добавляет в описание список "значение = Имя — комментарий из XML-документации"
/// и расширение x-enum-varnames с именами членов (его понимают генераторы клиентов)
/// </summary>
public class EnumSchemaTransformer : IOpenApiSchemaTransformer
{
    private static readonly Dictionary<string, Dictionary<string, string>> _docsCache = [];

    /// <inheritdoc />
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        var type = Nullable.GetUnderlyingType(context.JsonTypeInfo.Type) ?? context.JsonTypeInfo.Type;
        if (!type.IsEnum)
            return Task.CompletedTask;

        var docs = GetMemberDocs(type);
        var names = Enum.GetNames(type);

        var description = new StringBuilder();
        if (docs.TryGetValue(type.Name, out var typeSummary))
            description.AppendLine(typeSummary).AppendLine();

        foreach (var name in names)
        {
            var value = Convert.ToInt64(Enum.Parse(type, name));
            description.Append("- `").Append(value).Append("` = **").Append(name).Append("**");
            if (docs.TryGetValue(name, out var summary))
                description.Append(" — ").Append(summary);
            description.AppendLine();
        }

        schema.Description = description.ToString().TrimEnd();

        var varNames = new JsonArray();
        foreach (var name in names)
            varNames.Add(name);
        schema.Extensions ??= new Dictionary<string, IOpenApiExtension>();
        schema.Extensions["x-enum-varnames"] = new JsonNodeExtension(varNames);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Читает summary членов enum-а из XML-документации сборки (имя члена -> текст, для самого типа — по имени типа)
    /// </summary>
    private static Dictionary<string, string> GetMemberDocs(Type enumType)
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{enumType.Assembly.GetName().Name}.xml");
        lock (_docsCache)
        {
            if (_docsCache.TryGetValue(enumType.FullName!, out var cached))
                return cached;

            var result = new Dictionary<string, string>();
            if (File.Exists(xmlPath))
            {
                foreach (var member in XDocument.Load(xmlPath).Descendants("member"))
                {
                    var key = (string?)member.Attribute("name");
                    var summary = member.Element("summary")?.Value.Trim();
                    if (key is null || string.IsNullOrWhiteSpace(summary))
                        continue;

                    if (key == $"T:{enumType.FullName}")
                        result[enumType.Name] = summary;
                    else if (key.StartsWith($"F:{enumType.FullName}."))
                        result[key[($"F:{enumType.FullName}.").Length..]] = summary;
                }
            }

            _docsCache[enumType.FullName!] = result;
            return result;
        }
    }
}
