create database IndiaEvents2;

use IndiaEvents2;

-- Events Table
create table Events(
EventName varchar(30),
EventType varchar(30),
EventFee int,
Events varchar(max),
EventFromDate date,
EventToDate date,
CollegeName varchar(45),
Department varchar(30),
City varchar(30),
State varchar(30),
Address varchar(max),
Poster varBinary(MAX),
Website varchar(30),
EventID int primary key identity(1,1) not null);

create table LookupType(
lookupID UniqueIdentifier primary key NOT NULL default newid(),
lookupType varchar(100))

create table Lookups(
sno int primary key identity(1,1),
lookupID UniqueIdentifier NOT NULL default newid() REFERENCES LookupType(lookupID),
lookups varchar(100),
lookupsId uniqueIdentifier
)

create table users(
sno int primary key identity(1,1),
userName varchar(100),
password varchar(100),
mobile varchar(100),
email varchar(100))

--select * from LookupType;
--select * from Lookups
insert into LookupType values (NEWID(), 'Eventtypes');
insert into Lookups values ((select lookupID from LookupType where LookupType='Eventtypes'), 'Seminar', NEWID());
insert into Lookups values ((select lookupID from LookupType where LookupType='Eventtypes'), 'Workshop', NEWID());
insert into Lookups values ((select lookupID from LookupType where LookupType='Eventtypes'), 'Symposium', NEWID());
insert into Lookups values ((select lookupID from LookupType where LookupType='Eventtypes'), 'Meetting', NEWID());

