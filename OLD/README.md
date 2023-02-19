## Book-Library

NexerGroup project created by Quốc Việt [viet99cm@gmail.com]

### Initial Setup

- Update the (DefaultConnection) string in the "appsettings.json"

- In the Package Manager Console.
  In the ( Default project ) dropdown, select - Book.Library.Data project.

- Run add-migration => Initial Migration - to create Migrations folder.
  Run update-database => To create the database in Microsoft SQL Server.

- Make sure that ( Book.Library.Api ) is the startup project.

- Run the application, the database will be seeded with the ( books.json ) file.

- Test the API endroints with Swagger UI or PostMan.
