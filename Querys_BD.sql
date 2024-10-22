select * from USUARIO
select * from ROL
INSERT INTO ROL(Descripcion) VALUES ('ADMINISTRADOR');
INSERT INTO ROL(Descripcion) VALUES ('EMPLEADO');

SELECT * FROM USUARIO

SELECT * FROM PERMISO

INSERT INTO USUARIO(Documento, NombreCompleto, Correo, Clave,IdRol,Estado) VALUES ('3683739080101','Danilo Giron', 'giron605@gmail.com','123',1,1);

INSERT INTO USUARIO(Documento, NombreCompleto, Correo, Clave,IdRol,Estado) VALUES ('1010','Josue Xom', '@gmail.com','123',2,1);

INSERT INTO PERMISO(IdRol,NombreMenu) VALUES
(1,'menuusuario'),
(1,'menumantenedor'),
(1,'menuventas'),
(1,'menucompras'),
(1,'menuclientes'),
(1,'menuproveedores'),
(1,'menureportes'),
(1,'menuacercade')

select u.IdUsuario, u.Documento, u.NombreCompleto, u.Correo, u.Clave, u.Estado, r.IdRol, r.Descripcion from usuario u
inner join ROL r on r.IdRol = u.IdRol


UPDATE USUARIO set Estado = 0 where IdUsuario = 2


select * from USUARIO

Create proc SP_REGISTRARUSUARIO(

@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar (100),
@IdRol int,
@Estado bit,
@IdUsuarioResultado int output, 
@Mensaje varchar(500) output

)

as  
begin

	set @IdUsuarioResultado = 0
	set @Mensaje = ''
	
	if not exists(select * from USUARIO where Documento = @Documento)
	begin 

		insert into USUARIO (Documento, NombreCompleto, Correo, Clave, IdRol, Estado) 
		values (@Documento, @NombreCompleto, @Correo, @Clave,@IdRol, @Estado)

		set @IdUsuarioResultado = SCOPE_IDENTITY()
		
	end

	else

	   set @Mensaje = 'No se puede repetir el documento para mas de 1 usuario'

end


declare @idusuariogenerado int
declare @mensaje varchar(500)
exec SP_REGISTRARUSUARIO '123','pruebas','test@gmail','456',2,1,@idusuariogenerado output,@Mensaje output 

select @idusuariogenerado
select @mensaje

go


Create proc SP_EDITARUSUARIO(


@IdUsuario int,
@Documento varchar(50),
@NombreCompleto varchar(100),
@Correo varchar(100),
@Clave varchar (100),
@IdRol int,
@Estado bit,
@Respuesta bit output, 
@Mensaje varchar(500) output

)

as  
begin

	set @Respuesta = 0
	set @Mensaje = ''
	
	if not exists(select * from USUARIO where Documento = @Documento and IdUsuario != @IdUsuario)
	begin 

		update USUARIO set
		Documento = @Documento, 
		NombreCompleto = @NombreCompleto, 
		Correo = @Correo, 
		Clave = @Clave, 
		IdRol = @IdRol, 
		Estado = @Estado

		where IdUsuario = @IdUsuario
		

		set @Respuesta = 1
		
	end

	else

	   set @Mensaje = 'No se puede repetir el documento para mas de 1 usuario'

end

declare @Respuesta bit
declare @mensaje varchar(500)
exec SP_EDITARUSUARIO 1,'123','pruebas 2','test@gmail','456',2,1,@Respuesta output,@Mensaje output 

select @Respuesta
select @mensaje

select * from USUARIO

go

Create proc SP_ELIMINARUSUARIO(


@IdUsuario int,
@Respuesta bit output, 
@Mensaje varchar(500) output

)

as  
begin

	set @Respuesta = 0
	set @Mensaje = ''
	declare @pasoreglas bit  = 1


	IF EXISTS (SELECT * FROM COMPRA C
	INNER JOIN USUARIO U ON U.IdUsuario = C.IdUsuario
	WHERE U.IdUsuario = @IdUsuario
	)

	BEGIN
		
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una compra\n' 

	END

	IF EXISTS (SELECT * FROM VENTA V
	INNER JOIN USUARIO U ON U.IdUsuario = V.IdUsuario
	WHERE U.IdUsuario = @IdUsuario
	)

	BEGIN
		
		set @pasoreglas = 0
		set @Respuesta = 0
		set @Mensaje = @Mensaje + 'No se puede eliminar porque el usuario se encuentra relacionado a una venta\n' 

	END

	if (@pasoreglas = 1)

	begin

	   delete from USUARIO where IdUsuario = @IdUsuario

	   set @Respuesta = 1

	end

end


INSERT INTO CATEGORIA(Descripcion,Estado) VALUES ('FIEBRE Y TOS',1)
INSERT INTO CATEGORIA(Descripcion,Estado) VALUES ('PRESION ARTERIAL',1)


create PROC SP_RegistrarCategoria(
@Descripcion varchar(50),
@Estado bit,
@Resultado int output,
@Mensaje varchar(500) output
)as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion)
	begin
		insert into CATEGORIA(Descripcion,Estado) values (@Descripcion,@Estado)
		set @Resultado = SCOPE_IDENTITY()
	end
	ELSE
		set @Mensaje = 'No se puede repetir la descripcion de una categoria'
	
end

go

Create procedure sp_EditarCategoria(
@IdCategoria int,
@Descripcion varchar(50),
@Estado bit,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion =@Descripcion and IdCategoria != @IdCategoria)
		update CATEGORIA set
		Descripcion = @Descripcion,
		Estado = @Estado
		where IdCategoria = @IdCategoria
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'No se puede repetir la descripcion de una categoria'
	end

end

go

create procedure sp_EliminarCategoria(
@IdCategoria int,
@Resultado bit output,
@Mensaje varchar(500) output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (
	 select *  from CATEGORIA c
	 inner join PRODUCTO p on p.IdCategoria = c.IdCategoria
	 where c.IdCategoria = @IdCategoria
	)
	begin
	 delete top(1) from CATEGORIA where IdCategoria = @IdCategoria
	end
	ELSE
	begin
		SET @Resultado = 0
		set @Mensaje = 'La categoria se encuentara relacionada a un producto'
	end

end

GO