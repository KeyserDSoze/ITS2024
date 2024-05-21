[Microservices](https://learn.microsoft.com/en-us/azure/architecture/guide/architecture-styles/microservices)
[Microservices example architecture](https://microservices.io/i/Microservice_Architecture.png)

[Domain Driven Design](https://en.wikipedia.org/wiki/Domain-driven_design)

Videos
[Clean architecture](https://www.youtube.com/watch?v=TQdLgzVk2T8&ab_channel=MilanJovanovi%C4%87)

[Container su VS2022](https://learn.microsoft.com/en-us/visualstudio/containers/overview?view=vs-2022)

[Docker commands](https://docs.docker.com/reference/dockerfile/)

[Docker compose](https://docs.docker.com/compose/compose-application-model/)

---

# Tutorial su Entity Framework Core

Questo tutorial offre una guida all'uso di Entity Framework Core (EF Core) in un'applicazione .NET, concentrando l'attenzione sugli approcci Code First e Database First. EF Core è un Object-Relational Mapper (ORM) che permette agli sviluppatori .NET di lavorare con un database utilizzando oggetti .NET, eliminando la necessità di scrivere la maggior parte del codice di accesso ai dati che di solito è necessario.

## Prerequisiti

- SDK .NET
- SQL Server LocalDB (per lo sviluppo locale)

## Configurazione dell'Ambiente

1. **Avvio di SQL Server LocalDB**:
   Questo comando avvia l'istanza LocalDB denominata "MSSQLLocalDB".
   ```bash
   sqllocaldb start "MSSQLLocalDB"
   ```

2. **Informazioni su LocalDB**:
   Questo comando mostra le informazioni sull'istanza LocalDB.
   ```bash
   sqllocaldb info "MSSQLLocalDB"
   ```

## Aggiunta dei Pacchetti EF Core

Prima di iniziare, assicurati di aggiungere i pacchetti EF Core al tuo progetto .NET.

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

## Approccio Code First

### Definizione del Context e del Modello

1. **Creazione di un DbContext**:
   - `AppDbContext` è la classe principale che coordina la funzionalità EF per un dato modello di dati.
   - Questo contesto deriva dalla classe base `DbContext`.
   - Include un `DbSet<Product>` per interrogare e salvare istanze di `Product`.

   ```csharp
   public class AppDbContext : DbContext
   {
       public DbSet<Product> Products { get; set; }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
           optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFCoreExample;Trusted_Connection=True;");
       }
   }
   ```

2. **Definizione di un Modello**:
   - `Product` rappresenta una tabella nel database.
   - EF Core utilizza la classe `Product` per creare la tabella del database.

   ```csharp
   public class Product
   {
       public int Id { get; set; }
       [Required]
       public string Name { get; set; }
       public decimal Price { get; set; }
   }
   ```

### Migrazioni e Aggiornamento del Database

3. **Generazione e Applicazione delle Migrazioni**:
   - Le migrazioni sono usate per mantenere lo schema del database sincronizzato con il modello di dati.
   - I seguenti comandi generano e applicano le migrazioni all'istanza LocalDB.

   - Setup:
   ```bash
    dotnet tool install --global dotnet-ef --version 8.*
   ```
   - Comandi per migrazione:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

## Approccio Database First

### Scaffolding del Context e dei Modelli

1. **Generazione del Modello da un Database Esistente**:
   - Questo comando crea uno scaffold di un contesto e classi di tipo entità per un database esistente.
   - Sostituisci `YourDatabaseName` con il nome del tuo database.

   ```bash
   dotnet ef dbcontext scaffold "Server=(localdb)\mssqllocaldb;Database=YourDatabaseName;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
   ```

Questo creerà un `DbContext` e classi modello basate sullo schema di `YourDatabaseName`, permettendoti di lavorare con il database usando oggetti .NET.