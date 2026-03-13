using Flowify.Contracts;
using Moq;
using MS00000_TemplateApi.Model.Application.DTOs;
using MS00000_TemplateApi.Services.Application.ConfigApp.Queries.ConfigApp.GetAll;
using MS00000_TemplateApi.Services.Application.Logger;
using MS00000_TemplateApi.Services.Infrastracture.Data.SqlServer.Repository.ConfigApp;

namespace MS00000_TemplateApi.tests.unit.Services.Application.ConfigApp.Queries.ConfigApp.GetAll;

public class GetConfigAppAllQueryHandlerTests
{
    [Fact]
    public async Task Handle_AllPresent_ReturnListConfigAppDto()
    {
        //Arrange
        Mock<IConfigAppRepository> configAppRepositoryMock = new();
        Mock<IMediator> mediatorMock = new();
        Mock<IApplicationLogger> loggerMock = new();

        List<ConfigAppDto> listResult = [new ConfigAppDto()];
        configAppRepositoryMock
            .Setup(x => x.GetAllAsync(new CancellationToken(false)))
            .ReturnsAsync(listResult);

        GetConfigAppAllQueryHandler service = new(configAppRepositoryMock.Object, loggerMock.Object);

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
        Mock<IApplicationLogger> loggerMock = new();

        List<ConfigAppDto> listResult = null;
        configAppRepositoryMock
            .Setup(x => x.GetAllAsync(new CancellationToken(false)))
            .ReturnsAsync(listResult);

        GetConfigAppAllQueryHandler service = new(configAppRepositoryMock.Object, loggerMock.Object);

        //Act
        List<ConfigAppDto> listaNuova = await service.Handle(new GetConfigAppAllQuery(), CancellationToken.None);

        //Assert
        Assert.Empty(listaNuova);
    }
}