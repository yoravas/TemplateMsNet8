FROM instance-quay-quay-registry.apps.ocps.sviluppo.inps.it/global/dotnet-80-runtime-datapower AS base
USER default

ENV LD_LIBRARY_PATH=/app/clidriver/lib/

FROM base AS final
WORKDIR /app
COPY ./publish .
RUN find /app/
ENTRYPOINT ["dotnet", "MS00000_TemplateApi.dll"]
HEALTHCHECK NONE 