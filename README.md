# Chess\_.NET

This is a web-based chess game for my personal improvement built with .NET for the backend and React for the frontend.

## Tech Stack:

- **Backend**: .NET 8.0.401
- **Frontend**: React 18.3.1 + TypeScript 5.2.2 + Vite 5.3.1
- **Game Engine**: Bitboard logic for efficient chess piece movement and game state management.

## How to set up a project

### Make sure you have installed this:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/en)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Docker](https://www.docker.com/products/docker-desktop/)

### You also need to have google app password:

[link how to create it](https://support.google.com/accounts/answer/185833?hl=en)

### Lets get started

#### Docker

In work directory:

`docker compose up -d`

#### Frontend

```
cd frontend
npm i
```

#### Backend

##### Account service:

1.  **User-secrets set up**

Explanation of each one:

- SenderPassword: your google app password.
- EncryptionKey: using for encryption service (the length must be 32 symbols); it's just a random string.
- EncryptionIV: using for encryption service (the length must be 16 symbols)' it's just a random string.

```
    cd backend/Microservices/Account
    dotnet user-secrets init
    dotnet user-secrets set "SenderPassword" "aaaa bbbb cccc dddd"
    dotnet user-secrets set "EncryptionKey" "Your32SymbolsSecretString"
    dotnet user-secrets set "EncryptionIV" "Your16SymbolsSecretIV"
```

2. **Apply migrations in account service:**

```
dotnet ef migrations add "Initial"
dotnet ef database update
```

3. **Create .env file**

   Create .env file in /backend/Microservices/Account

   Copy data from example.env and paste into .env (you must change JWT secret with 256 bit string)

4. **Run Account service**

   `dotnet run`

###### Chess service

1. Go to /backend/Microservices/Chess
2. Copy .env file from account service and paste into chess service (JWT secret keys must be the same)
3. **Apply migrations in chess service:**

`dotnet ef migrations add "Initial"`

`dotnet ef database update`

4. Run chess service:

`dotnet run`

## Useful information, that I have used:

- **Bitboards**: [More on Bitboards](https://www.chessprogramming.org/Bitboards).
- **Knight Moves**: [Learn More about Knight's Move Patterns](https://www.chessprogramming.org/Knight_Pattern). Addition: [OneStepOnly](https://www.chessprogramming.org/General_Setwise_Operations#OneStepOnly)
- **FEN (Forsyth-Edwards Notation)**: [More about FEN](https://ru.wikipedia.org/wiki/).
