# Markdown File

Per consentire l'esecuzione del microservizio, è necessario creare le seguenti tabelle all'interno di un database SQL Server:

-- Table dbo.TMP_ApiLog

CREATE TABLE [dbo].[TMP_ApiLog]
(
 [Id] Bigint IDENTITY(1,1) NOT NULL,
 [TimeStamp] Datetime NULL,
 [Level] Nvarchar(max) NULL,
 [Message] Nvarchar(max) NULL,
 [MessageTemplate] Nvarchar(max) NULL,
 [Exception] Nvarchar(max) NULL,
 [Properties] Nvarchar(max) NULL,
 [TraceId] Nvarchar(max) NULL,
 [SpanId] Nvarchar(max) NULL,
 [CorrelationID] Char(26) NULL,
 [ApiSerilogID] Bigint NULL,
 [Metodo] Nvarchar(1000) NULL
)
go

-- Add keys for table dbo.TMP_ApiLog

ALTER TABLE [dbo].[TMP_ApiLog] ADD CONSTRAINT [PK_TMP_ApiLog] PRIMARY KEY ([Id])
go


-- Table dbo.TMP_AdditionalDataLogApi

CREATE TABLE [dbo].[TMP_AdditionalDataLogApi]
(
 [AdditionalDataLogID] Bigint NOT NULL,
 [RequestPath] Nvarchar(max) NULL,
 [FilePath] Nvarchar(max) NULL,
 [AdditionalData] Nvarchar(max) NULL,
 [Exception] Nvarchar(max) NULL
)
go

-- Add keys for table dbo.TMP_AdditionalDataLogApi

ALTER TABLE [dbo].[TMP_AdditionalDataLogApi] ADD CONSTRAINT [PK_TMP_AdditionalDataLogApi] PRIMARY KEY ([AdditionalDataLogID])
go


-- Table TMP_ConfigApp

CREATE TABLE [TMP_ConfigApp]
(
 [ConfigAppID] Int NOT NULL,
 [Funzione] Nvarchar(1000) NOT NULL,
 [Attivata] Bit DEFAULT 0 NOT NULL
)
go

-- Add keys for table TMP_ConfigApp

ALTER TABLE [TMP_ConfigApp] ADD CONSTRAINT [PK_TMP_ConfigApp] PRIMARY KEY ([ConfigAppID])
go

INSERT INTO DbTemplate.dbo.TMP_ConfigApp
(ConfigAppID, Funzione, Attivata)
VALUES(1, 'All', 1);
GO

CREATE SEQUENCE [dbo].[TMP_SeqLogApi] AS bigint
 START WITH 1
 INCREMENT BY 1
 NO MINVALUE
 NO MAXVALUE
go

Come potete vedere, le tabelle sono progettate per memorizzare i log delle API e le configurazioni
dell'applicazione. 
La tabella `TMP_ApiLog` è utilizzata per registrare i dettagli delle richieste API,
inclusi timestamp, livello di log, messaggi, eccezioni e altre informazioni correlate. 
La tabella `TMP_AdditionalDataLogApi` è destinata a memorizzare dati aggiuntivi relativi ai log delle API, 
come il percorso della richiesta ed eventuali eccezioni. 
La tabella `TMP_ConfigApp` viene utilizzata per gestire le configurazioni dell'applicazione, 
indicando se una determinata funzione è attivata o meno.
Infine, la sequence `TMP_SeqLogApi` è utilizzata per generare valori unici per i log delle API; nello specifico per i campi `ApiSerilogID` della tabella `TMP_ApiLog` ed il campo `AdditionalDataLogID` della tabella `TMP_AdditionalDataLogApi`.

L'acronimo iniziale delle tabelle "TMP" suggerisce che l'acronimo dovrà essere sostituito con l'acronimo appropriato al database di destinazione.
Si rammenta che le connessioni presenti all'interno del file appsettings.json vanno sostiutite con le stringhe di connessione del database di destinazione.
le attuali stringhe di connessione presenti all'interno del file appsettings.json sono le seguenti:
- db-session-cache
- db-log
- db-app-ms
Le strighe di connessione vere si trovano all'interno del file devplaceholders.json, che è presente all'interno del progetto.
Nelle stringhe di connessione, è presente il placeholder {0}; questo va gestito inserendo all'interno del file launchSettings.json le vere password per connettersi al db.
Questo va fatto inserendo variabili ambiente all'interno del laucher Container (Dockerfile).
- vedi esempio:
"Container (Dockerfile)": {
      "commandName": "Docker",
      "launchBrowser": true,
      "launchUrl": "{Scheme}://{ServiceHost}:32906/swagger",
      "publishAllPorts": false,
      "environmentVariables": {
        "PSW_SESSION_CACHE_DB": "*******",
        "PSW_LOGDATABASE_DB": "*******",
        "PSW_APP_MS_DB": "*******"
      }
    },

Aggiungere ulteriori variabili ambiente all'interno del laucher Container (Dockerfile) se si dovessero aggiungere nuove stringhe di connessione all'interno del file appsettings.json.
Si ricorda però che si dovrà gestire il codice all'interno della classe PlaceholdersConfig per leggere le nuove variabili ambiente e sostituire i placeholder presenti nelle stringhe di connessione con i valori reali.
Vedere la classe PlaceholdersConfig per maggiori dettagli su come gestire i placeholder e le variabili ambiente.
Nello specifico sarà necessario adattare il metodo privato "SetPswDb".