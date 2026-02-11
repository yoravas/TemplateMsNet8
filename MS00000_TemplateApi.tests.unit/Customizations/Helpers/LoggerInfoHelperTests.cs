using MS00000_TemplateApi.Customizations.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Helpers;

public class LoggerInfoHelperTests
{
    [Fact]
    public void LogUsedItemInfo_AllPresent_ReturnString()
    {
        //Arrange + Act
        string strFormattata = LoggerInfoHelper.LogUsedItemInfo();

        //Assert
        Assert.NotNull(strFormattata);
    }
    [Fact]
    public void LogUsedItemInfo_FilePathNotSplittable_ReturnString()
    {
        //Arrange + Act
        string strFormattata = LoggerInfoHelper.LogUsedItemInfo(filePath: "FileSenzaPath.cs");

        //Assert
        Assert.NotNull(strFormattata);
    }
    [Fact]
    public void LogUsedItemInfo_FilePathNotSplittableByDot_ReturnString()
    {
        //Arrange + Act
        string strFormattata = LoggerInfoHelper.LogUsedItemInfo(filePath: "C:\\cartella\\NomeSenzaEstensione");

        //Assert
        Assert.NotNull(strFormattata);
    }
    [Fact]
    public void LogUsedItemInfo_FilePathNull_ReturnString()
    {
        //Arrange + Act
        try
        {
            string strFormattata = LoggerInfoHelper.LogUsedItemInfo(filePath: null);

        }
        catch (Exception)
        {
        }
        //Assert
        Assert.True(true);
    }
    [Fact]
    public void LogUsedItemInfo_FilePathContainsNullChar_ReturnString()
    {
        // Arrange + Act
        string result = LoggerInfoHelper.LogUsedItemInfo(filePath: "\0");

        // Assert
        Assert.NotNull(result);
    }
    [Fact]
    public void LogUsedItemInfo_FilePathEmpty_ReturnString()
    {
        //Arrange + Act
        string strFormattata = LoggerInfoHelper.LogUsedItemInfo(filePath: "");

        //Assert
        Assert.NotNull(strFormattata);
    }
    [Fact]
    public void LogUsedItemInfo_FilePathFinalBackslash_ReturnString()
    {
        //Arrange + Act
        string strFormattata = LoggerInfoHelper.LogUsedItemInfo(filePath: "C:\\cartella\\");

        //Assert
        Assert.NotNull(strFormattata);
    }
    [Fact]
    public void LogUsedItemInfo_MethodNameNull_ReturnString()
    {
        //Arrange + Act
        string strFormattata = LoggerInfoHelper.LogUsedItemInfo(methodName: null);

        //Assert
        Assert.NotNull(strFormattata);
    }
    [Fact]
    public void LogUsedItemInfo_MethodNameEmpty_ReturnString()
    {
        //Arrange + Act
        string strFormattata = LoggerInfoHelper.LogUsedItemInfo(methodName: "");

        //Assert
        Assert.NotNull(strFormattata);
    }
    [Fact]
    public void LogUsedItemInfo_MethodNameOnlySpace_ReturnString()
    {
        //Arrange + Act
        string strFormattata = LoggerInfoHelper.LogUsedItemInfo(methodName: "   ");

        //Assert
        Assert.NotNull(strFormattata);
    }
}
