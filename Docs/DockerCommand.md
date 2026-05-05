# How to

Spin up server in docker using:

> docker run --name player_one -e MYSQL_ROOT_PASSWORD=groot -e MYSQL_DATABASE=player_one -p 3306:3306 -d mysql:8

OR use `docker compose up -d`

Docker Compose file created - just for testing purposes for now.

---

Docker Compose commands:


Run it:

`docker compose up -d`

Stop it: 

`docker compose down`

Take it down and remove volumes:

`docker compose down -v`