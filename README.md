## Pre-Installation process

Install the necessary tools:

1. .NET SDK from [Microsoft Official Page](https://dotnet.microsoft.com/en-us/download/dotnet/9.0). Need to be version
   9.0 or above.

## Installation process

1. Clone the repository to your local machine.
2. Open your IDE or Code Editor and navigate to the project folder.
3. Create the .env file in the root folder and add environment variable:

```
USER_IDENTITY_DB_CONNECTION_STRING=Host=database_ip;Port=5432;Database=user_identity;Username=username;Password=password;
```

## Important notes

1. Use SonarQube for IDE to keep the code clean and consistent.
