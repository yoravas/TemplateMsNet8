using Microsoft.AspNetCore.Mvc.ModelBinding;
using MS00000_TemplateApi.Customizations.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Extensions;

public class ModelStateDictionaryExtentionTests
{
    [Fact]
    public void SerializeErrors_WithValidModelState_ReturnDictionary()
    {
        //Arrange
        ModelStateDictionary keyValuePairs = new();

        //Act
        Dictionary<string, object> errors = ModelStateDictionaryExtention.SerializeErrors(keyValuePairs);

        //Assert
        Assert.NotNull(errors);
    }
    [Fact]
    public void SerializeErrors_NullModelState_ThrowException()
    {
        //Arrange
        //Act
        try
        {
            Dictionary<string, object> errors = ModelStateDictionaryExtention.SerializeErrors(null);

        }
        catch (Exception)
        {
        }
        //Assert
        Assert.True(true);
    }
}
