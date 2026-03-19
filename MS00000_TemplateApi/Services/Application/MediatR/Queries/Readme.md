# Markdown File

La cartella Queries contiene le query specifiche per la funzionalità NomeFunzione. Ogni query rappresenta una richiesta di dati all'interno dell'applicazione, come il recupero di informazioni o la ricerca di elementi specifici.
Come esempio prendiamo la gestione delle News. Potresti avere query come GetNewsByIdQuery, GetAllNewsQuery e SearchNewsQuery all'interno di questa cartella.
Un esempio di query: 
```csharp
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiMediatR.Application.News.Queries;

public record GetNewsByIdQuery(int Id) : IRequest<Model.News>;
```
Ogni query implementa l'interfaccia IRequest di MediatR, che consente di definire il tipo di risposta attesa quando la query viene eseguita.
Puoi aggiungere ulteriori query in questa cartella in base alle esigenze della tua applicazione.
Successivamente inserire gli handler per queste query nella cartella Handlers.
```csharp
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiMediatR.Application.News.Queries;
using WebApiMediatR.Model.EF;

namespace WebApiMediatR.Application.News.Handlers;

public class GetNewsByIdHandler : IRequestHandler<GetNewsByIdQuery, Model.News>
{
    private readonly NewsDbContext db;

    public GetNewsByIdHandler(NewsDbContext db)
    {
        this.db = db;
    }

    public async Task<Model.News> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
    {
        return await db.News.FindAsync(new object[] { request.Id }, cancellationToken);
    }
}
```
Ogni handler implementa l'interfaccia IRequestHandler di MediatR, che consente di definire la logica di elaborazione per una query specifica.
Puoi aggiungere ulteriori handler in questa cartella in base alle esigenze della tua applicazione.
