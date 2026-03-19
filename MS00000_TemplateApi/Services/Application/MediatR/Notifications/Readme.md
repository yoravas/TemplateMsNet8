# Markdown File

La cartella Notifications contiene le notifiche specifiche per la funzionalità NomeFunzione. Le notifiche sono utilizzate per comunicare eventi o cambiamenti di stato all'interno dell'applicazione.
Per esempio, se stai implementando una funzionalità per la gestione delle news, potresti avere notifiche come NewsCreatedNotification, NewsUpdatedNotification e NewsDeletedNotification all'interno di questa cartella.
Un esempio di notifica: 
```csharp
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiMediatR.Application.News.Notifications;

public record NewsCreatedNotification(Model.News News) : INotification;
```
Ogni notifica implementa l'interfaccia INotification di MediatR, che consente di definire eventi che possono essere pubblicati e gestiti da più handler all'interno dell'applicazione.
Puoi aggiungere ulteriori notifiche in questa cartella in base alle esigenze della tua applicazione.
Dentro la caertella Handlers puoi creare gli handler per le notifiche, ad esempio:
```csharp
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiMediatR.Application.News.Notifications;

namespace WebApiMediatR.Application.News.Handlers;

public class NewsCreatedHandler : INotificationHandler<NewsCreatedNotification>
{
    public async Task Handle(NewsCreatedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Nuova news creata: {notification.News.Title}");
        //return Task.CompletedTask;
    }
}
```
Ogni handler implementa l'interfaccia INotificationHandler di MediatR, che consente di definire la logica di elaborazione per una notifica specifica.
Puoi aggiungere ulteriori handler per le notifiche in questa cartella in base alle esigenze della tua applicazione.
