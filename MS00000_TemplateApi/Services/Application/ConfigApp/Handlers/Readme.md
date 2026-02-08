# Markdown File

La carttella Handlers contiene gli handler specifici per la funzionalità NomeFunzione. Ogni handler è responsabile dell'elaborazione di un comando o di una query specifica all'interno dell'applicazione.
Per esempio, se stai implementando una funzionalità per la gestione delle news, potresti avere handler come CreateNewsCommandHandler, UpdateNewsCommandHandler e DeleteNewsCommandHandler all'interno di questa cartella.
Un esempio di handler: 
```csharp
using WebApiMediatR.Application.News.Commands;
using WebApiMediatR.Application.News.Notifications;
using WebApiMediatR.Model.EF;

namespace WebApiMediatR.Application.News.Handlers;

public class CreateNewsHandler : IRequestHandler<CreateNewsCommand, Model.News>
{
    private readonly NewsDbContext db;
    private readonly IMediator mediator;

    public CreateNewsHandler(NewsDbContext db, IMediator mediator)
    {
        this.db = db;
        this.mediator = mediator;
    }

    public async Task<Model.News> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
    {
        Model.News news = new()
        {
            Title = request.Title,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow
        };

        db.News.Add(news);
        await db.SaveChangesAsync(cancellationToken);

        // Pubblica la notifica
        await mediator.Publish(new NewsCreatedNotification(news), cancellationToken);

        return news;
    }
}
```
Ogni handler implementa l'interfaccia IRequestHandler di MediatR, che consente di definire la logica di elaborazione per un comando o una query specifica.
Puoi aggiungere ulteriori handler in questa cartella in base alle esigenze della tua applicazione.
