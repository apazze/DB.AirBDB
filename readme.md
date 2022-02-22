# AirBDB - Projeto de apoio para mentoria de .NET Core e/ou .NET 5.0

AirBNB, mas simplificado e da DB :)

## Tech
- .NET 5.0
- MVC
- Swagger
- Microserviços

## Database
Usamos **Migration** para lidar com o banco. Assim sendo, precisamos executar alguns comandos para iniciar o banco de dados.

- Abra o `Package Manager Console` no Visual Studio.
- Selecione `DBCaio.AirBDB.DAL.Repository` como *Default Project*.
- Execute o comando `add-migration FirstMigration`
- Execute o comando `update-database`
