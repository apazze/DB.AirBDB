# AirBDB - Projeto de apoio para mentoria de .NET Core e/ou .NET 5.0

AirBNB, mas simplificado e da DB :)

- WebAPI para cadastro e listagem de ofertas de hospedagem.
- Disponível métodos REST GET, POST, PUT e DELETE para Usuários, Lugares e Reservas.
- É possível filtrar por uma cidade e datas de início e fim que irá listar as acomodações disponíveis no período.

Algumas das regras criadas:

- Intervalo mínimo para data inicio e data fim da reserva: 1 dia (configurável via appsettings.json).
- Intervalo máximo das datas para reserva: 365 dias (configurável via appsettings.json).
- Não permite Reserva do Lugar em data ocupada.
- Não permite Reserva do Usuário se já possuir uma no mesmo período.
- Entre outras.

## Tech
- .NET 5.0
- MVC
- Swagger
- Microserviços
- Nos testes automatizados foi utilizado: XUnit, FluentAssertion, Moq, Mapper, InMemoryDatabase e AutoFixture.

## Database
Usamos **Migration** para lidar com o banco. Assim sendo, precisamos executar alguns comandos para iniciar o banco de dados.

- Abra o `Package Manager Console` no Visual Studio.
- Selecione `DBCaio.AirBDB.DAL.Repository` como *Default Project*.
- Execute o comando `add-migration FirstMigration`
- Execute o comando `update-database`
