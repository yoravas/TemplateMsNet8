using AutoBogus;
using Castle.Core.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using MS00000_TemplateApi.Model.Application.DTOs;
using MS00000_TemplateApi.Services.Application.ConfigApp.Queries.ConfigApp.GetAll;
using MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.ConfigApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS00000_TemplateApi.tests.unit.Services.Application.ConfigApp.Queries.ConfigApp.GetAll;

public class GetConfigAppAllQueryHandlerTests
{
    [Fact]
    public async Task Handle_AllPresent_ReturnListConfigAppDto()
    {
        //Arrange
        Mock<IConfigAppRepository> configAppRepositoryMock = new();
        Mock<IMediator> mediatorMock = new();
        Mock<ILogger<GetConfigAppAllQueryHandler>> loggerMock = new();

        List<ConfigAppDto> listResult = [new ConfigAppDto()];
        configAppRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(listResult);

        GetConfigAppAllQueryHandler service = new(configAppRepositoryMock.Object, mediatorMock.Object, loggerMock.Object);
        
        //Act
        List<ConfigAppDto> listaNuova = await service.Handle(new GetConfigAppAllQuery(), CancellationToken.None);

        //Assert
        Assert.NotEmpty(listaNuova);
    }
    [Fact]
    public async Task Handle_NullResult_ReturnListConfigAppDto()
    {
        //Arrange
        Mock<IConfigAppRepository> configAppRepositoryMock = new();
        Mock<IMediator> mediatorMock = new();
        Mock<ILogger<GetConfigAppAllQueryHandler>> loggerMock = new();

        List<ConfigAppDto> listResult = null;
        configAppRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(listResult);

        GetConfigAppAllQueryHandler service = new(configAppRepositoryMock.Object, mediatorMock.Object, loggerMock.Object);
        
        //Act
        List<ConfigAppDto> listaNuova = await service.Handle(new GetConfigAppAllQuery(), CancellationToken.None);

        //Assert
        Assert.Empty(listaNuova);
    }
}
