CREATE TABLE IF NOT EXISTS users
(
    id                  INTEGER PRIMARY KEY ASC,
    username            VARCHAR(32)        NOT NULL,
    normalized_username VARCHAR(32) UNIQUE NOT NULL,
    email               VARCHAR(32) UNIQUE NOT NULL,
    password_hash       CHAR(64)           NOT NULL,
    api_key_hash        CHAR(64),
    glicko_rating       INT                NOT NULL,
    glicko_rd           REAL               NOT NULL
);

CREATE TABLE IF NOT EXISTS games
(
    id        INTEGER PRIMARY KEY ASC,
    white     INT NOT NULL,
    black     INT NOT NULL,
    game_data TEXT,
    FOREIGN KEY (white) REFERENCES users (id),
    FOREIGN KEY (black) REFERENCES users (id)
);