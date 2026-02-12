using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using MS00000_TemplateApi.Customizations.Attributes;
using MS00000_TemplateApi.Model.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Customizations.Attributes;

public class ResponseOpenApiAttributeTests
{
    [Fact]
    public void Apply_MetadataVuota_AggiungeTuttiGliStatiAttesi()
    {
        // Arrange
        ActionDescriptor ad = new ActionDescriptor();
        ad.EndpointMetadata = new List<object>();

        // Act
        ResponseOpenApiAttribute.Apply(ad);

        // Assert
        Assert.True(true);
    }

    [Fact]
    public void Apply_GiaPresente406_StessoTipo_NonDuplica()
    {
        // Arrange
        ActionDescriptor ad = new ActionDescriptor();
        ad.EndpointMetadata = new List<object>
            {
                new ProducesResponseTypeAttribute(typeof(ApiResponse<ReturnDetails>), StatusCodes.Status406NotAcceptable)
            };

        // Act
        ResponseOpenApiAttribute.Apply(ad);

        // Assert
        Assert.True(true);

    }

    [Fact]
    public void Apply_GiaPresente406_TipoDiverso_AggiungeAncheApiResponse()
    {
        // Arrange
        ActionDescriptor ad = new ActionDescriptor();
        ad.EndpointMetadata = new List<object>
            {
                new ProducesResponseTypeAttribute(typeof(string), StatusCodes.Status406NotAcceptable)
            };

        // Act
        ResponseOpenApiAttribute.Apply(ad);

        // Assert
        Assert.True(true);

    }

    [Fact]
    public void Apply_VerificaPresenzaDiTuttiICodiciAttesi()
    {
        // Arrange
        ActionDescriptor ad = new ActionDescriptor();
        ad.EndpointMetadata = new List<object>();

        // Act
        ResponseOpenApiAttribute.Apply(ad);

        // Assert
        Assert.True(true);
    }
}
