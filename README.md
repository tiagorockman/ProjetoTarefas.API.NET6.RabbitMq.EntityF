## API .NET 6
-url Api Swagger - https://localhost:7194/swagger/index.html

# RABBITMQ
-url: http://localhost:15672/
-user/pass: guest/guest
-Variaveis no appSettings
-> ConsumerWakeTime -> Tempo de espera até começar a monitorar fila em segundos
-> ConsumerReloadCheck -> Tempo programado para verificar fila em segundos

# BANCO DE DADOS SQL server
--connection string dbTarefa no appSettings

# Entity Framework
Instalar o pacote Microsoft.EntityFrameworkCore.Design no projeto Tarefas.Api e Infra e o SqlServer tamém
-Window Package Manager Console.
--Instalação Package
run -> dotnet tool install --global dotnet-ef
--Seleciona Default Project Tarefas.Infra (Criar Migrações) - Funcionou pelo (PowerShell)
run -> dotnet ef migrations add InitialCreate  --startup-project ..\Tarefas.Api\ 
run -> dotnet ef database update --startup-project ..\Tarefas.Api\