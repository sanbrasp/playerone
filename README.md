# 👾 PlayerOne 🎮

---

PlayerOne is a personal project for keeping game nostalgia alive.

The program uses `Docker` for setting up an isolated database, and allows the users to 
add games, genres and platforms for later reference and reminiscing.

---

## Background
This idea came to life after a conversation with my brother in December 2025, where we discussed how we could
most easily gather information about the many games we have loved over the years.  
I suggested that we might set up a database and store the data this way, and thus the seed was sown.

---

## How to
1. Clone the repo:  

- Github:  
`git clone repo git@github.com:sanbrasp/playerone.git`

- Gitlab:  
`git clone git@gitlab.com:sanbrasp/playerone.git`


2. Navigate to the solution root with your terminal:  
`cd PlayerOne`


3. Restore or build the project:  
`dotnet restore`  
`dotnet build`

**Json Settings**  
1. Fill in your credentials in [appsettings.example.json](appsettings.example.json).  


2. Rename the file to `appsettings.json`.

<br>

**Docker**  

1. Fill in your credentials in [.env.example](Docker/.env.example).  


2. Rename the file to `.env`.


3. Start Docker Desktop, or whichever version you use


4. Navigate to the `Docker` directory, and:
`docker compose up -d`

5. Use a program like DBeaver, MySQL Workbench, or Rider's integrated database manager to manually check the database.


6. Run the program:  
`dotnet run`

---

## Tools and technologies

| Component      | Name                 |
|----------------|----------------------|
| IDE            | Visual Studio, Rider |
| Framework      | .NET                 |
| Languages      | C#, MySQL            |
| Virtualization | Docker Desktop       |

---

## Acknowledgements
- AI has been used for boilerplate and conceptual reminders during the development of this project

---