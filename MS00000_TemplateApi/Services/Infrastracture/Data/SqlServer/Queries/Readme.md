# Markdown File

Inserire dentro la cartella `Queries` tutte le query SQL necessarie per interagire con il database SQL Server. 
Queste query possono essere utilizzate dai repository.
Detto questo, è importante mantenere una struttura organizzata all'interno della cartella `Queries`, ad esempio suddividendo le query in sottocartelle in base alla funzionalità o al modulo dell'applicazione a cui si riferiscono.

N.B. 
Le query SQL devono essere scritte in modo efficiente e sicuro, evitando vulnerabilità come l'iniezione SQL.
Ogni file .sql che contiene le query devono essere nominati in modo di identificare se sono select, insert, update o delete.
Alla fine della creazione del file .sql, è importante visualizzare le proprietà del file e selezionare in `Azione di compilazione` la voce `Risorsa incorporata`.