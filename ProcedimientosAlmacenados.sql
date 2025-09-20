USE PastaFlowBD;
GO

-- REGISTRAR NUEVO USUARIO
CREATE PROCEDURE sp_RegistrarUsuario
    @dni NVARCHAR(20),
    @nombre NVARCHAR(25),
    @apellido NVARCHAR(25),
    @correo NVARCHAR(100),
    @telefono NVARCHAR(15),
    @id_rol INT,
    @contrasena VARBINARY(64),
    @estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Usuario (dni, nombre, apellido, correo_electronico, telefono, id_rol, contrasena_hash, estado)
    VALUES (@dni, @nombre, @apellido, @correo, @telefono, @id_rol, @contrasena, @estado);
END;

-- output msj de error, excepciones,