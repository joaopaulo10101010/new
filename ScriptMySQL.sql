create database dbProjetocidade;
use dbProjetocidade;

create table Usuarios(
Id int auto_increment,
Nome varchar(100),
Email varchar(100) unique,
Senha varchar(100),
primary key(Id)
);

create table Produtos(
Id int auto_increment,
Nome varchar(100),
Descricao varchar(100),
Preco varchar(100),
Quantidade int,
primary key(Id)
)
