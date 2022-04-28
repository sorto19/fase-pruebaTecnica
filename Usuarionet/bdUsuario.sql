

create database USUARIOSNET


USE USUIARIOSNET

CREATE TABLE USUARIO (
IdUsuario  int identity primary key,
Nombre varchar(50),
Apellido varchar(50),
Email varchar(100),
Contraseña varchar(100),
Telefono char(9),
Departamento varchar(100),
Municipio varchar(150)
)

create procedure sp_listar
as
begin
select * from USUARIO
end

create procedure sp_obtener(
@IdUsuario int


)
as
begin 
select * from USUARIO where IdUsuario=@IdUsuario
end

create procedure sp_guardar(
@Nombre varchar(50),
@Apellido varchar(50),
@Email varchar(100),
@Contraseña varchar(100),
@Telefono char(9),
@Departamento varchar(100),
@Municipio varchar(150)
)
as 
begin
insert into USUARIO(Nombre, Apellido,Email,Contraseña ,Telefono,Departamento,Municipio) values (@Nombre, @Apellido,@Email,@Contraseña ,@Telefono,@Departamento,@Municipio)
end

create procedure sp_editar(
@IdUsuario int,
@Nombre varchar(50),
@Apellido varchar(50),
@Email varchar(100),
@Contraseña varchar(100),
@Telefono char(9),
@Departamento varchar(100),
@Municipio varchar(150)

)
as 
begin
update USUARIO set Nombre=@Nombre, Apellido=@Apellido, Email=@Email, Contraseña=@Contraseña, Telefono=@Telefono, Departamento=@Departamento, Municipio=@Municipio where IdUsuario=@IdUsuario
end

create procedure sp_eliminar(
@IdUsuario int
)
as
begin
delete from USUARIO where IdUsuario=@IdUsuario
end