-- SQLite
DROP TABLE Surfboard;
-- SELECT * FROM Surfboard;
CREATE TABLE Surfboard (id INTEGER PRIMARY KEY AUTOINCREMENT, title TEXT, volume REAL, length REAL, width REAL, price REAL, thickness REAL, type TEXT, equipment TEXT NULLABLE, rentedout BOOL DEFAULT FALSE);