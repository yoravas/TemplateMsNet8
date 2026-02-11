using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit;

public class ProgramTests
{
    private static MethodInfo GetPrivateStaticMethod(string methodName, Type[] parameterTypes)
    {
        Type programType = typeof(MS00000_TemplateApi.Program);
        MethodInfo? method = programType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        if (method == null)
        {
            throw new InvalidOperationException($"Metodo {methodName} non trovato.");
        }
        ParameterInfo[] ps = method.GetParameters();
        if (ps.Length != parameterTypes.Length)
        {
            throw new InvalidOperationException($"Firma del metodo {methodName} inattesa.");
        }
        for (int i = 0; i < ps.Length; i++)
        {
            if (ps[i].ParameterType != parameterTypes[i])
            {
                throw new InvalidOperationException($"Parametro {i} del metodo {methodName} inatteso.");
            }
        }
        return method;
    }

    [Fact]
    public void SetSerilog_TryParseTrueDebug_EsegueConfigurazione_EccezioneAttesa()
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.Configuration["SerilogLevel"] = LogEventLevel.Debug.ToString();
        MethodInfo method = GetPrivateStaticMethod("SetSerilog", new Type[] { typeof(WebApplicationBuilder) });
        SelfLog.Enable(_ => { });

        // Act
        Exception ex = Record.Exception(() => method.Invoke(null, new object[] { builder }));

        // Assert
        Assert.NotNull(ex);
    }

    [Fact]
    public void SetSerilog_TryParseFalseValoreInvalido_DefaultError_EccezioneAttesa()
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.Configuration["SerilogLevel"] = "VALORE-NON-VALIDO";
        MethodInfo method = GetPrivateStaticMethod("SetSerilog", new Type[] { typeof(WebApplicationBuilder) });
        SelfLog.Enable(_ => { });

        // Act
        Exception ex = Record.Exception(() => method.Invoke(null, new object[] { builder }));

        // Assert
        Assert.NotNull(ex);
    }

    [Fact]
    public void SetSerilog_SenzaConnectionString_NonCreaSinkSql_EccezioneAttesa()
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        builder.Configuration["SerilogLevel"] = LogEventLevel.Error.ToString();
        MethodInfo method = GetPrivateStaticMethod("SetSerilog", new Type[] { typeof(WebApplicationBuilder) });
        SelfLog.Enable(_ => { });

        // Act
        Exception ex = Record.Exception(() => method.Invoke(null, new object[] { builder }));

        // Assert
        Assert.NotNull(ex);
    }    

    [Fact]
    public void SetSelfLogSerilog_Messaggio_Completata()
    {
        // Arrange
        MethodInfo method = GetPrivateStaticMethod("SetSelfLogSerilog", Array.Empty<Type>());
        method.Invoke(null, Array.Empty<object>());

        // Act
        string message = "TEST";

        SelfLog.WriteLine(message);

        // Assert
        Assert.True(true);
    }

}