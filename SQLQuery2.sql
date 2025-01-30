CREATE TABLE customer
(
     id INT PRIMARY KEY IDENTITY(1,1),
     book_id VARCHAR(MAX) NULL,
     full_name VARCHAR(MAX) NULL,
     email VARCHAR(MAX) NULL,
     contact VARCHAR(MAX) NULL,
     gender VARCHAR(MAX) NULL,
     address VARCHAR(MAX) NULL,
     room_id VARCHAR(MAX) NULL,
     price DECIMAL NULL,
     status_payment VARCHAR(MAX) NULL,
     status VARCHAR(MAX) NULL,
     date_from DATE NULL,
     date_to DATE NULL,
     date_book DATE NULL,
)

SELECT * FROM customer

SELECT COUNT(id) FROM customer

SELECT * FROM rooms