-- Crear la tabla Users
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50),
    Pass VARBINARY(8000),
    Email VARCHAR(50)
);

-- Declarar y definir las variables
DECLARE @UserName VARCHAR(50) = 'Victor';
DECLARE @Password NVARCHAR(100) = '1234';  -- Contraseña en texto plano
DECLARE @Email VARCHAR(50) = 'guevaravic100@gmail.com';
DECLARE @SecretPassphrase NVARCHAR(100) = 'YourSecretPassphrase'; -- Utiliza la misma frase secreta en ambas consultas

-- Encriptar la contraseña utilizando la frase secreta
DECLARE @EncryptedPassword VARBINARY(8000);
SET @EncryptedPassword = ENCRYPTBYPASSPHRASE(@SecretPassphrase, @Password);


-- Seleccionar usuarios y desencriptar contraseñas
SELECT Id, Name, CONVERT(NVARCHAR(100), DECRYPTBYPASSPHRASE(@SecretPassphrase, Pass)) AS DecryptedPassword, Email
FROM Users;

-- Insertar usuario en la tabla
INSERT INTO Users (Name, Pass, Email)
VALUES (@UserName, @EncryptedPassword, @Email);


-- Consulta para verificar los datos insertados
SELECT * FROM Users;



drop table Users;

create database proyect_BD;

use proyect_BD;