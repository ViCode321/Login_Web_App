CREATE Table Users (
	UserId INT Identity (1,1),
    Name VARCHAR(50),
    Pass VARBINARY(8000),
    Email VARCHAR(50),   
)

INSERT INTO Users(Name, Pass, Email)
VALUES ('Victor', ENCRYPTBYPASSPHRASE('password', '1234'), 'guevaravic100@gmail.com')

select * from Users;

drop table Users;

create database proyect_BD;

CREATE Table Users (
	UserId INT Identity (1,1),
    Name VARCHAR(50),
    Pass VARBINARY(8000),
    Email VARCHAR(50),   
)

INSERT INTO Users(Name, Pass, Email)
VALUES ('Victor', ENCRYPTBYPASSPHRASE('password', '1234'), 'guevaravic100@gmail.com')

select * from Users;

use proyect_BD;