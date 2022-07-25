create database AddressBookSystem_Day34
use AddressBookSystem_Day34

create Table Books
(
 BookId int not null Primary Key Identity,
 BookName varchar(25) not null,
 BookType varchar(25),
 DateofBookCreation date
)

create Table Contacts
(
 BookId int not null Foreign Key References Books(BookId),
 PersonId int not null Primary Key identity,
 PersonName varchar(30) not null,
 Gender char not null,
 MobileNo Bigint
)

create Table Addresses
(
 PersonId int not null Foreign Key References Contacts(PersonId),
 AddressId int not null Primary Key identity,
 City varchar(20),
 State varchar(20),
 ZipCode int,
 Country varchar(20)
)

select * from Books

drop table Books
drop table Contacts
drop table Addresses

