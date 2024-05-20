[Microservices](https://learn.microsoft.com/en-us/azure/architecture/guide/architecture-styles/microservices)
[Microservices example architecture](https://microservices.io/i/Microservice_Architecture.png)

[Domain Driven Design](https://en.wikipedia.org/wiki/Domain-driven_design)

Videos
[Clean architecture](https://www.youtube.com/watch?v=TQdLgzVk2T8&ab_channel=MilanJovanovi%C4%87)

```
sqllocaldb start "MSSQLLocalDB"
sqllocaldb info "MSSQLLocalDB"


dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
		//optionsBuilder.UseInMemoryDatabase("MyDatabase");
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFCoreExample;Trusted_Connection=True;");
    }
}

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public decimal Price { get; set; }
}

dotnet tool install --global dotnet-ef --version 8.*
dotnet ef migrations add InitialCreate
dotnet ef database update


dotnet ef dbcontext scaffold "Server=(localdb)\mssqllocaldb;Database=YourDatabaseName;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
```