use pizzaria_db;

CREATE TABLE Pizzas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Sabor VARCHAR(100) NOT NULL,
    TempoPreparo INT NOT NULL
);

INSERT INTO Pizzas (Sabor, TempoPreparo)
VALUES 
    ('Marguerita', 30),
    ('Calabresa', 30),
    ('Quatro Queijos', 35),
    ('Frango com Catupiry', 40),
    ('Palmito com tomate seco', 45),
    ('Portuguesa', 25),
    ('Brócolis com queijo', 30),
    ('Milho', 25);

