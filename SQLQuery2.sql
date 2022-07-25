-- creating Stored Procedure

create proc [dbo].[SP_AddNewBook]
@BookName varchar(25),
@BookType varchar(25),
@DateofBookCreation date
as 

insert into Books values(@BookName,@BookType,@DateofBookCreation)

go

create proc [dbo].[SP_AddContacts]
@BookID int,
@PersonName varchar(30),
@Gender char,
@MobileNo bigint
as

insert into Contacts values(@BookID,@PersonName,@Gender,@MobileNo)

go

create proc [dbo].[SP_AddAddresses]
@PersonID int,
@City varchar(20),
@State varchar(20),
@ZipCode int,
@Country varchar(20)
as

insert into Addresses values(@PersonId,@City,@State,@ZipCode,@Country)

go

