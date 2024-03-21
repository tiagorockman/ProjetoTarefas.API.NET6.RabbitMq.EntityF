## API .NET 6
> **URL Api Swagger -** *https://localhost:7194/swagger/index.html*

## RABBITMQ
- url: http://localhost:15672/
- user/password: guest/guest
- Variaveis no appSettings *(![United States](https://raw.githubusercontent.com/stevenrskelton/flag-icon/master/png/16/country-4x3/us.png "United States") AppSettings Variable)*
     - **ConsumerWakeTime** -> Tempo de espera até começar a monitorar fila em segundos *(![United States](https://raw.githubusercontent.com/stevenrskelton/flag-icon/master/png/16/country-4x3/us.png "United States") Waiter time to start monitoring the queue in seconds)*
     - **ConsumerReloadCheck** -> Tempo programado para verificar fila em segundos *(![United States](https://raw.githubusercontent.com/stevenrskelton/flag-icon/master/png/16/country-4x3/us.png "United States") Time scheduled to verify the queue in seconds)*

## BANCO DE DADOS SQL Server *(![United States](https://raw.githubusercontent.com/stevenrskelton/flag-icon/master/png/16/country-4x3/us.png "United States") SQL Database Connection)*
> Connection string **dbTarefa** at the appSettings file

## Entity Framework
1. Install the package Microsoft.EntityFrameworkCore.Design in the projects Tarefas.Api, Infra and SqlServer also
you can use **Window Package Manager Console**.

* Instalação Package

```
       dotnet tool install --global dotnet-ef
     
       # Select Default Project Tarefas.Infra (Criar Migrações) - It has worked also by command line (PowerShell)
       dotnet ef migrations add InitialCreate  --startup-project ..\Tarefas.Api\

       dotnet ef database update --startup-project ..\Tarefas.Api\
```
![UserSeach](https://github.com/tiagorockman/ProjetoTarefas.API.NET6.RabbitMq.EntityF/blob/main/arquivosExtras/tela.png)
