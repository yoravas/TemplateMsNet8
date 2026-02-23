namespace MS00000_TemplateApi.Model.ValueObjects;

public class Sql
{
    public Sql(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    //Proprietà per conservare il valore originale
    public string Value { get; }

    //Conversione da/per il tipo string
    public override string ToString() => Value;
}