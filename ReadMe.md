## To setup the project, follow these instructions:

1. Clone the repository to your local machine.
2. Navigate to the project directory in your command prompt.
3. Run the docker engine on your machine.
4. Run the command "docker-compose up" to install sql server.
5. In the Package Manager Console run the command "EntityFrameworkCore\Update-Database" to create the database.

## Accessing the API with Swagger
You can access the API using Swagger, which provides a user-friendly interface for interacting with the endpoints.

### To authenticate your connection, follow these steps:

1. Use the register method to register a user.
2. Use the login method to obtain a token.
3. Authorize the user by typing "Bearer <Token>" in the authorization window.
