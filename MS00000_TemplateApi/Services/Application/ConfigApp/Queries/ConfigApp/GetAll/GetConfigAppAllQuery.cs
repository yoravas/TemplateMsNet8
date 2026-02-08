using MediatR;
using MS00000_TemplateApi.Model.Application.DTOs;

namespace MS00000_TemplateApi.Services.Application.ConfigApp.Queries.ConfigApp.GetAll;
public record GetConfigAppAllQuery : IRequest<List<ConfigAppDto>>
{
}

