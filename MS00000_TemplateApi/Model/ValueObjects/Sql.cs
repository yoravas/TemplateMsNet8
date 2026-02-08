namespace MS00000_TemplateApi.Model.ValueObjects;
public class Sql
{
    public Sql(string value)
    {
        Value = value;
    }

    public Sql()
    {
    }

    //Proprietà per conservare il valore originale
    public string Value { get; set; }

    //Conversione da/per il tipo string        
    public override string ToString()
    {
        return this.Value;
    }
}
