CREATE TABLE IF NOT EXISTS platforms (
    platformid INT AUTO_INCREMENT PRIMARY KEY ,
    name VARCHAR(75) NOT NULL
);

CREATE TABLE IF NOT EXISTS genres (
    genreid INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE IF NOT EXISTS users (
    userid INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(100) NOT NULL,
    password CHAR(6) NOT NULL
);

CREATE TABLE IF NOT EXISTS games (
    gameid INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(150) NOT NULL,
    platform_id INT NOT NULL,
    genre_id INT NOT NULL,
    release_year INT NOT NULL,
    added_by INT NOT NULL,
    FOREIGN KEY (platform_id) REFERENCES platforms(platformid),
    FOREIGN KEY (genre_id) REFERENCES genres(genreid),
    FOREIGN KEY (added_by) REFERENCES users(userid)
);