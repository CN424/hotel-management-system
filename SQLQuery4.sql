SELECT * FROM customer

SELECT * FROM users

SELECT COUNT(id) FROM users WHERE role = 'staff'

SELECT * FROM rooms

SELECT COUNT(id) FROM rooms WHERE status = 'Active' OR status = 'Available'

SELECT SUM(price) FROM customer