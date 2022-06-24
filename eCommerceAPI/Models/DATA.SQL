drop database ecommerceDB;
create database ecommerceDB;

use ecommerceDB;
drop table Categories;

create table Categories(
    ID integer,
    CategoryName varchar(50) not null,
    primary key (ID),
);

create table Products(
    ID integer,
    ProductName varchar(50) not null,
    Price float not null,
    Description varchar(50) not null,
    primary key (ID),
    CategoryID integer references Categories(ID)

);

create table Users(
    ID int,
    FullName varchar(50) not null,
    Email varchar(50) not null,
    Password varchar(50) not null,
    IsAdmin int not null,
    primary key (ID)
)