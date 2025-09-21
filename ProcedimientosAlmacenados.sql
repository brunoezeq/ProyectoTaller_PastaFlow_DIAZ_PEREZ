USE PastaFlowBD;
go
-- Filtrar ComboBox
CREATE PROCEDURE sp_buscar_empleado
    @dni NVARCHAR(20) = NULL,
    @id_rol INT = NULL
AS
BEGIN
    SELECT u.nombre, u.apellido, u.dni, u.correo_electronico, u.telefono, r.nombre_rol, u.id_rol,
           CASE WHEN u.estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS estado
    FROM Usuario u
    INNER JOIN Rol r ON u.id_rol = r.id_rol
    WHERE (@dni IS NULL OR u.dni = @dni)
      AND (@id_rol IS NULL OR u.id_rol = @id_rol)
END