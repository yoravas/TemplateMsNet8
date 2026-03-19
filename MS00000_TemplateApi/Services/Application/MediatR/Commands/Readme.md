# Markdown File

La cartella Commands contiene i comandi specifici per la funzionalità NomeFunzione. Ogni comando rappresenta un'azione che può essere eseguita all'interno dell'applicazione, come la creazione, l'aggiornamento o l'eliminazione di dati.
Come esempio prendiamo la gestione delle News. Potresti avere comandi come CreateNewsCommand, UpdateNewsCommand e DeleteNewsCommand all'interno di questa cartella.
Un esempio di comando: 

```csharp
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiMediatR.Model;

namespace WebApiMediatR.Application.News.Commands;

public record CreateNewsCommand(string Title, string Content) : IRequest<Model.News>;
```
Ogni comando implementa l'interfaccia IRequest di MediatR, che consente di definire il tipo di risposta attesa quando il comando viene eseguito.
Puoi aggiungere ulteriori comandi in questa cartella in base alle esigenze della tua applicazione.
