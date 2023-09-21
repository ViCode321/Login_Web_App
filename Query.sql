-- Crear la tabla Users
CREATE TABLE Users (
	UserId INT IDENTITY (1,1),
    Name VARCHAR(50),
    Pass VARBINARY(8000),
    Email VARCHAR(50),
    Salt VARBINARY(16) -- Agregar una columna para almacenar la sal
)

-- Generar una sal aleatoria de 16 bytes (128 bits)
DECLARE @salt VARBINARY(16);
SET @salt = NEWID();

-- Encriptar la contraseña utilizando la sal generada
INSERT INTO Users (Name, Pass, Email, Salt)
VALUES ('Victor', HASHBYTES('SHA2_256', 'password' + CAST(@salt AS NVARCHAR(MAX))), 'guevaravic100@gmail.com', @salt)

-- Consulta para verificar los datos insertados
SELECT * FROM Users;

drop table Users;

create database proyect_BD;

use proyect_BD;