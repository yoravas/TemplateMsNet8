using System.Text.Json;

namespace MS00000_TemplateApi.Configurations;
public class PascalCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        // Verifica se il nome è vuoto o se ha già la prima lettera maiuscola.
        if (string.IsNullOrEmpty(name) || char.IsUpper(name[0]))
        {
            return name;
        }

        // Converti il primo carattere in maiuscolo.
        return $"{char.ToUpper(name[0])}{name.Substring(1)}";
    }
}
