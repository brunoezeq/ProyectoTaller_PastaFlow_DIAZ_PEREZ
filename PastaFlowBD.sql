-- CREACIÓN DE LA BASE DE DATOS --
CREATE DATABASE PastaFlowBD; 
GO

-- USO DE LA BASE DE DATOS --
USE PastaFlowBD; 
GO

-- TABLA ROL --
CREATE TABLE Rol (
    id_rol INT PRIMARY KEY IDENTITY(1,1),
    nombre_rol NVARCHAR(50) NOT NULL
);
GO

-- TABLA USUARIO --
CREATE TABLE Usuario (
    id_usuario INT PRIMARY KEY IDENTITY(1,1),
    dni NVARCHAR(20) UNIQUE NOT NULL,
    nombre NVARCHAR(100) NOT NULL,
    apellido NVARCHAR(100) NOT NULL,
    correo_electronico NVARCHAR(100),
    telefono NVARCHAR(20),
    estado BIT NOT NULL DEFAULT 1,
    id_rol INT NOT NULL,
    FOREIGN KEY (id_rol) REFERENCES Rol(id_rol),
	contrasena_hash VARBINARY(64) NOT NULL
);
GO

-- TABLA TURNO --
CREATE TABLE Turno (
    id_turno INT PRIMARY KEY IDENTITY(1,1),
    nombre_turno NVARCHAR(50) NOT NULL,
    hora_inicio TIME NOT NULL,
    hora_fin TIME NOT NULL
);

-- TABLA CAJA -- 
CREATE TABLE Caja (
    id_caja INT PRIMARY KEY IDENTITY(1,1),
    fecha_hora_apertura DATETIME NOT NULL,
    monto_inicial DECIMAL(10,2) NOT NULL,
    fecha_hora_cierre DATETIME NULL,
    monto_cierre DECIMAL(10,2) NULL,
    monto_esperado DECIMAL(10,2) NULL,
    id_usuario INT NOT NULL,
    id_turno INT NOT NULL,
    FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario),
    FOREIGN KEY (id_turno) REFERENCES Turno(id_turno)
);
GO

-- TABLA METODO DE PAGO --
CREATE TABLE Metodo_Pago (
    id_metodo INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(50) NOT NULL,
    recargo DECIMAL(5,2) DEFAULT 0
);
GO

-- TABLA CATEGORIA --
CREATE TABLE Categoria (
    id_categoria INT PRIMARY KEY IDENTITY(1,1),
    nombre_categoria NVARCHAR(100) NOT NULL
);
GO

-- TABLA PRODUCTO --
CREATE TABLE Producto (
    id_producto INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100) NOT NULL,
    descripcion NVARCHAR(255),
    precio DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL,
    estado BIT NOT NULL DEFAULT 1,
    id_categoria INT NOT NULL,
    FOREIGN KEY (id_categoria) REFERENCES Categoria(id_categoria)
);

-- TABLA VENTA --
CREATE TABLE Venta (
    id_venta INT PRIMARY KEY IDENTITY(1,1),
    fecha_venta DATETIME NOT NULL DEFAULT GETDATE(),
    total_venta DECIMAL(10,2) NOT NULL,
    numero_factura NVARCHAR(20) NOT NULL,
    id_caja INT NOT NULL,
    id_metodo INT NOT NULL,
    FOREIGN KEY (id_caja) REFERENCES Caja(id_caja),
    FOREIGN KEY (id_metodo) REFERENCES Metodo_Pago(id_metodo)
);
GO

-- TABLA DETALLE DE VENTA --
CREATE TABLE Detalle_Venta (
    id_detalle INT PRIMARY KEY IDENTITY(1,1),
    id_venta INT NOT NULL,
    id_producto INT NOT NULL,
    cantidad INT NOT NULL,
    precio_unitario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (id_venta) REFERENCES Venta(id_venta),
    FOREIGN KEY (id_producto) REFERENCES Producto(id_producto)
);
GO

-- TABLA PROMOCION --
CREATE TABLE Promocion (
    id_promocion INT PRIMARY KEY IDENTITY(1,1),
    id_producto INT NOT NULL,
    precio_promocional DECIMAL(10,2) NOT NULL,
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE NOT NULL,
    FOREIGN KEY (id_producto) REFERENCES Producto(id_producto)
);
GO

-- TABLA RESERVA --
CREATE TABLE Reserva (
    id_reserva INT PRIMARY KEY IDENTITY(1,1),
    nombre_cliente NVARCHAR(100) NOT NULL,
    apellido_cliente NVARCHAR(100) NOT NULL,
    fecha_hora_reserva DATETIME NOT NULL,
    cantidad_personas INT NOT NULL,
    estado NVARCHAR(50) NOT NULL,
    id_usuario INT NOT NULL,
    FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)
);
GO

-- TABLA QUEJA --
CREATE TABLE Queja (
    id_queja INT PRIMARY KEY IDENTITY(1,1),
    nombre_cliente NVARCHAR(100) NOT NULL,
    apellido_cliente NVARCHAR(100) NOT NULL,
    motivo_queja NVARCHAR(255) NOT NULL,
    descripcion_queja NVARCHAR(MAX),
    fecha_hora_queja DATETIME NOT NULL DEFAULT GETDATE(),
    id_usuario INT NOT NULL,
    FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario)
);
GO

-- CARGA DE ROLES --
INSERT INTO Rol (id_rol, nombre_rol) VALUES
(1, 'Administrador'),
(2, 'Gerente'),
(3, 'Cajero');
GO

-- CARGA DE USUARIOS. UNO POR ROL --
INSERT INTO Usuario (dni, nombre, apellido, correo_electronico, telefono, estado, id_rol, contrasena_hash)
VALUES
('11111111', 'Juan', 'Pérez', 'administrador@gmail.com', '123456789', 1, 1, HASHBYTES('SHA2_256', N'administrador')),
('22222222', 'Martín', 'Lopez', 'gerente@gmail.com', '1234567890', 1, 2, HASHBYTES('SHA2_256', N'gerente')),
('33333333', 'Jose', 'Lopez', 'cajero@gmail.com', '1234567890', 1, 3, HASHBYTES('SHA2_256', N'cajero'));
GO

-----------------------------------------------------
--------- PROCEDIMIENTOS ALMACENADOS ----------------
-----------------------------------------------------

-- REGISTRAR USUARIO ---
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
GO

-- FILTRAR POR ROLES --
CREATE PROCEDURE sp_BuscarUsuarios
    @dni NVARCHAR(20) = NULL,
    @id_rol INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT u.nombre, 
           u.apellido, 
           u.dni, 
           u.correo_electronico, 
           u.telefono, 
           r.nombre_rol,  
           u.id_rol,       
           CASE 
               WHEN u.estado = 1 THEN 'Activo' 
               ELSE 'Inactivo' 
           END AS estado
    FROM Usuario u
    INNER JOIN Rol r ON u.id_rol = r.id_rol
    WHERE (@dni IS NULL OR u.dni = @dni)
      AND (@id_rol IS NULL OR u.id_rol = @id_rol);
END;
