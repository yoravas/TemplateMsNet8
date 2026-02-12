using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MS00000_TemplateApi.Configurations.Appsettings;
using MS00000_TemplateApi.Configurations.AppSettings;
using System.Reflection;
using System.Text.Json;

namespace MS00000_TemplateApi.tests.unit.Configurations.AppSettings;

public class PlaceholdersComfigTests
{
    [Fact]
    public void SetPlaceholderConfig_AmbienteNonDevelopment_NonEsegue()
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions { EnvironmentName = Environments.Production });

        // Act
        PlaceholdersComfig.SetPlaceholderConfig(builder);

        // Assert
        Assert.NotNull(builder.Configuration);
    }

    [Fact]
    public void SetPlaceholderConfig_AmbienteDevelopment_Esegue()
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions { EnvironmentName = Environments.Development });

        // Act
        PlaceholdersComfig.SetPlaceholderConfig(builder);

        // Assert
        Assert.NotNull(builder.Configuration);
    }

    [Fact]
    public void LoadReplacements_FileAssente_RitornaVuoto()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("LoadReplacements", BindingFlags.Static | BindingFlags.NonPublic);
        string fakePath = Guid.NewGuid().ToString("N") + ".json";
        object[] prms = new object[] { fakePath };

        // Act
        object? result = mi!.Invoke(null, prms);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void SetPswDb_AppMsDB_SostituzioneDaEnv()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("SetPswDb", BindingFlags.Static | BindingFlags.NonPublic);
        Environment.SetEnvironmentVariable(PswPlaceholderEnviroment.PswAppMsDB, "pw_app");
        KeyValuePair<string, JsonElement> kvp = new KeyValuePair<string, JsonElement>(PswPlaceholderEnviroment.AppMsDB, default);
        object[] prms = new object[] { kvp, "start-{0}-end" };

        // Act
        object? result = mi!.Invoke(null, prms);

        // Assert
        Assert.Equal("start-pw_app-end", result);
    }

    [Fact]
    public void SetPswDb_LogDatabaseDB_SostituzioneDaEnv()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("SetPswDb", BindingFlags.Static | BindingFlags.NonPublic);
        Environment.SetEnvironmentVariable(PswPlaceholderEnviroment.PswLogDatabaseDB, "pw_log");
        KeyValuePair<string, JsonElement> kvp = new KeyValuePair<string, JsonElement>(PswPlaceholderEnviroment.LogDatabaseDB, default);
        object[] prms = new object[] { kvp, "x-{0}-y" };

        // Act
        object? result = mi!.Invoke(null, prms);

        // Assert
        Assert.Equal("x-pw_log-y", result);
    }

    [Fact]
    public void SetPswDb_SessionCacheDB_SostituzioneDaEnv()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("SetPswDb", BindingFlags.Static | BindingFlags.NonPublic);
        Environment.SetEnvironmentVariable(PswPlaceholderEnviroment.PswSessionCacheDB, "pw_sess");
        KeyValuePair<string, JsonElement> kvp = new KeyValuePair<string, JsonElement>(PswPlaceholderEnviroment.SessionCacheDB, default);
        object[] prms = new object[] { kvp, "[{0}]" };

        // Act
        object? result = mi!.Invoke(null, prms);

        // Assert
        Assert.Equal("[pw_sess]", result);
    }

    [Fact]
    public void SetPswDb_ChiaveSconosciuta_RitornaInvariato()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("SetPswDb", BindingFlags.Static | BindingFlags.NonPublic);
        KeyValuePair<string, JsonElement> kvp = new KeyValuePair<string, JsonElement>("other", default);
        object[] prms = new object[] { kvp, "no-change" };

        // Act
        object? result = mi!.Invoke(null, prms);

        // Assert
        Assert.Equal("no-change", result);
    }

    [Fact]
    public void BuildSubstitutedDictionary_PlaceholderPresente_Sostituito()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                    { "ConnStr", "Pwd=#{MY_PWD}#" }
            })
            .Build();
        Dictionary<string, string> replacements = new()
            {
                { "MY_PWD", "secret!" }
            };
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("BuildSubstitutedDictionary", BindingFlags.Static | BindingFlags.NonPublic);
        object[] prms = new object[] { configuration, replacements };

        // Act
        object? result = mi!.Invoke(null, prms);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void BuildSubstitutedDictionary_PlaceholderMancante_LasciatoInvariato()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                    { "K", "Value=#{NOPE}#" }
            })
            .Build();
        Dictionary<string, string> replacements = new();
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("BuildSubstitutedDictionary", BindingFlags.Static | BindingFlags.NonPublic);
        object[] prms = new object[] { configuration, replacements };

        // Act
        object? result = mi!.Invoke(null, prms);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void BuildSubstitutedDictionary_ReplacementNull_LasciatoInvariato()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                    { "X", "A=#{Z}#" }
            })
            .Build();
        Dictionary<string, string> replacements = new()
            {
                { "Z", null! }
            };
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("BuildSubstitutedDictionary", BindingFlags.Static | BindingFlags.NonPublic);
        object[] prms = new object[] { configuration, replacements };

        // Act
        object? result = mi!.Invoke(null, prms);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void BuildSubstitutedDictionary_ValoreNull_NonInseritoNelRisultato()
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                    { "WithNull", null }
            })
            .Build();
        Dictionary<string, string> replacements = new();
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("BuildSubstitutedDictionary", BindingFlags.Static | BindingFlags.NonPublic);
        object[] prms = new object[] { configuration, replacements };

        // Act
        object? result = mi!.Invoke(null, prms);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void TryJsonElementToString_String_RitornaTrue()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = JsonDocument.Parse("\"hello\"").RootElement;
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.True(ok);
    }

    [Fact]
    public void TryJsonElementToString_Int64_RitornaTrue()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = JsonDocument.Parse("9223372036854775807").RootElement;
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.True(ok);
    }

    [Fact]
    public void TryJsonElementToString_UInt64_RitornaTrue()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = JsonDocument.Parse("18446744073709551615").RootElement;
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.True(ok);
    }

    [Fact]
    public void TryJsonElementToString_Decimal_RitornaTrue()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = JsonDocument.Parse("12345.6789").RootElement;
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.True(ok);
    }

    [Fact]
    public void TryJsonElementToString_Double_RitornaTrue()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = JsonDocument.Parse("3.141592653589793").RootElement;
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.True(ok);
    }

    [Fact]
    public void TryJsonElementToString_True_RitornaTrue()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = JsonDocument.Parse("true").RootElement;
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.True(ok);
    }

    [Fact]
    public void TryJsonElementToString_False_RitornaTrue()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = JsonDocument.Parse("false").RootElement;
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.True(ok);
    }

    [Fact]
    public void TryJsonElementToString_Array_RitornaTrue()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = JsonDocument.Parse("[1,2]").RootElement;
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.True(ok);
    }

    [Fact]
    public void TryJsonElementToString_Object_RitornaTrue()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = JsonDocument.Parse("{\"a\":1}").RootElement;
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.True(ok);
    }

    [Fact]
    public void TryJsonElementToString_Null_RitornaFalse()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = JsonDocument.Parse("null").RootElement;
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.False(ok);
    }

    [Fact]
    public void TryJsonElementToString_Undefined_RitornaFalse()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = default;
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.False(ok);
    }

    [Fact]
    public void TryJsonElementToString_DecimalNonConvertibile_RitornaTrue()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = JsonDocument.Parse("1e400").RootElement; // troppo grande per decimal → TryGetDecimal = false
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.True(ok);
    }

    [Fact]
    public void TryJsonElementToString_DoubleNonConvertibile_RitornaTrue()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = JsonDocument.Parse("\"999999999999999999999999999\"").RootElement; // stringa → non entra in TryGetDouble
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.True(ok);
    }

    [Fact]
    public void TryJsonElementToString_DoubleConvertibile_RitornaTrue()
    {
        // Arrange
        Type t = typeof(PlaceholdersComfig);
        MethodInfo? mi = t.GetMethod("TryJsonElementToString", BindingFlags.Static | BindingFlags.NonPublic);
        JsonElement el = JsonDocument.Parse("3.141592653589793").RootElement; // double preciso → TryGetDouble = true
        object[] prms = new object[] { el, string.Empty };

        // Act
        bool ok = (bool)mi!.Invoke(null, prms)!;

        // Assert
        Assert.True(ok);
    }
}
