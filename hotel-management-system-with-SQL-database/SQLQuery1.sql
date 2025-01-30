CREATE TABLE users
(
     id INT PRIMARY KEY IDENTITY(1,1),
     username VARCHAR(MAX) NULL,
     password VARCHAR(MAX) NULL,
     role VARCHAR(MAX) NULL,
     status VARCHAR(MAX) NULL,
     date_register DATE NULL
)

SELECT * FROM users

CREATE TABLE rooms
(
     id INT PRIMARY KEY IDENTITY(1,1),
     room_id VARCHAR(MAX) NULL,
     type VARCHAR(MAX) NULL,
     room_name VARCHAR(MAX) NULL,
     price FLOAT NULL,
     image_path VARCHAR(MAX) NULL,
     status VARCHAR(MAX) NULL,
     date_register DATE NULL,
     date_update DATE NULL,
     date_delete DATE NULL
)

SELECT * FROM rooms

SELECT room_id FROM rooms

SELECT * FROM rooms WHERE date_delete IS NULL