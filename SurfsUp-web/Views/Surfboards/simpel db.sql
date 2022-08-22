-- SQLite
-- SELECT * FROM Surfboard;
DROP TABLE Surfboard;
CREATE TABLE Surfboard (id INTEGER PRIMARY KEY AUTOINCREMENT, title TEXT, width REAL, length REAL, thickness REAL, volume REAL, type TEXT, equipment TEXT, rentedout BOOL, price DECIMAL, picture BLOB NULLABLE);