using MS00000_TemplateApi.Model.Application;
using System.Net;
using System.Text.Json;

namespace MS00000_TemplateApi.Customizations.Middlewares;
public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Stream originalBody = context.Response.Body;
        await using MemoryStream buffer = new();
        context.Response.Body = buffer;

        try
        {
            await next(context);
        }
        catch (Exception ex)
        {

            string traceId = context.TraceIdentifier;

            logger.LogErrorCustom(ex, $"Eccezione non gestita per {context.Request.Path}, con traceId: {traceId}");
            ApiResponse<ReturnDetails> payload;

            if (!context.Response.HasStarted)
            {
                // sovrascrivi il buffer con il tuo payload
                buffer.SetLength(0); // reset
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json; charset=utf-8";

                payload = new ApiResponse<ReturnDetails>(new ReturnDetails
                {
                    Message = $"TraceId dell'errore: {traceId}",
                    StatusCode = context.Response.StatusCode,
                    DescStatusCode = nameof(HttpStatusCode.InternalServerError)
                });

                // Scrivi nel buffer poiché Response.Body == buffer
                await JsonSerializer.SerializeAsync(buffer, payload, cancellationToken: context.RequestAborted);
                await buffer.FlushAsync(context.RequestAborted);
            }
            else
            {
                // non puoi riscrivere; rilancia
                throw;
            }
        }
        finally
        {
            // Copia SEMPRE sullo stream originale PRIMA del dispose del buffer
            buffer.Position = 0;
            await buffer.CopyToAsync(originalBody, context.RequestAborted);
            context.Response.Body = originalBody;
        }
    }


}
