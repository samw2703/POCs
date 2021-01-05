create database TestDb;

CREATE USER 'TestDbAdmin'@'localhost' IDENTIFIED BY 'password';

GRANT ALL PRIVILEGES ON TestDb.* TO 'TestDbAdmin'@'localhost';

FLUSH PRIVILEGES;
go

use TestDb;

create table Message(
	Value varchar(512)
);

insert into Message
	(Value)
values
	('');