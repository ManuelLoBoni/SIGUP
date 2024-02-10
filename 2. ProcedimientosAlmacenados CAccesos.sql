USE UDP_CONTROL;
GO

----------------------------------------------SP - Edificio------------------------------------------------
CREATE PROCEDURE sp_RegistrarEdificio
(
	@NombreEdificio varchar(60),
	@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	BEGIN
		INSERT INTO Edificio (NombreEdificio) VALUES (@NombreEdificio)
        SET @Resultado = scope_identity()
	END
END
GO

CREATE PROCEDURE sp_EditarEdificio
(
	@IdEdificio int,
	@NombreEdificio varchar(60),
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF EXISTS (SELECT * FROM Edificio WHERE IdEdificio = @IdEdificio)
	BEGIN
		UPDATE Edificio
		SET
		NombreEdificio = @NombreEdificio
		WHERE IdEdificio = @IdEdificio
		SET @Resultado = 1
	END
	ELSE
	SET @Mensaje = 'El edificio con la id solicitada no fue encontrado.'
END
GO

CREATE PROCEDURE sp_EliminarEdificio
(
	@IdEdificio int,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Areas A INNER JOIN Edificio E 
	ON A.IdEdificio = E.IdEdificio WHERE A.IdEdificio = @IdEdificio)
	BEGIN
		DELETE FROM Edificio WHERE IdEdificio = @IdEdificio
		SET @Resultado = 1
	END
	ELSE
		SET @Mensaje = 'El edificio a eliminar está relacionado a una área.'
END
GO

----------------------------------------------SP - Area------------------------------------------------
CREATE PROCEDURE sp_RegistrarAreas
(
	@IdArea int, 
	@NombreArea varchar(60), 
	@IdEdificio int,
	@Resultado int output,
	@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Areas WHERE IdArea = @IdArea)
	BEGIN
		INSERT INTO Areas (IdArea,NombreArea,IdEdificio) VALUES (@IdArea,@NombreArea,@IdEdificio)
        SET @Resultado = scope_identity()
	END
	ELSE
		SET @Mensaje = 'Ya existe una área con ese id.'
END
GO

CREATE PROCEDURE sp_EditarAreas
(
	@IdArea int, 
	@NombreArea varchar(60), 
	@IdEdificio int,
	@Resultado int output,
	@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Resultado = 0
	IF EXISTS (SELECT * FROM Areas WHERE IdArea = @IdArea)
	BEGIN
		UPDATE Areas
		SET
		NombreArea = @NombreArea,
		IdEdificio = @IdEdificio
		WHERE IdArea = @IdArea
		SET @Resultado = 1
	END
	ELSE
	SET @Mensaje = 'El área con la id solicitada no fue encontrada.'
END
GO

CREATE PROCEDURE sp_EliminarAreas
(
	@IdArea int, 
	@Resultado int output,
	@Mensaje varchar(500) output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Areas A
        INNER JOIN ControlUsuario CU ON A.IdArea = CU.Area
        INNER JOIN prestamo P ON A.IdArea = P.Area
        WHERE A.IdArea = @IdArea)
	BEGIN
		DELETE FROM Areas WHERE IdArea = @IdArea
		SET @Resultado = 1
	END
	ELSE
		SET @Mensaje = 'El área a eliminar está relacionada a Control de Usuario o Préstamos.'
END
GO

----------------------------------------------SP - Carreras------------------------------------------------
CREATE PROCEDURE sp_RegistrarCarreras
(
	@NombreCarrera varchar(100),
	@Resultado int output,
    @Mensaje varchar(500) output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Carreras WHERE NombreCarrera = @NombreCarrera)
	BEGIN
		INSERT INTO Carreras (NombreCarrera) VALUES (@NombreCarrera)
        SET @Resultado = scope_identity()
	END
	ELSE
		SET @Mensaje = 'La categoria ya existe'
END
GO

CREATE PROCEDURE sp_EditarCarreras
(
	@IdCarrera int,
	@NombreCarrera varchar(100),
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF EXISTS (SELECT * FROM Carreras WHERE IdCarrera = @IdCarrera)
	BEGIN
		UPDATE Carreras
		SET
		NombreCarrera = @NombreCarrera
		WHERE IdCarrera = @IdCarrera
		SET @Resultado = 1
	END
	ELSE
	SET @Mensaje = 'La carrera con la id solicitada no fue encontrada.'
	
END
GO

CREATE PROCEDURE sp_EliminarCarreras
(
	@IdCarrera int,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Carreras C INNER JOIN ControlUsuario CE 
	ON C.IdCarrera = CE.IdCarrera WHERE C.IdCarrera = @IdCarrera)
	BEGIN
		DELETE FROM Carreras WHERE IdCarrera = @IdCarrera
		SET @Resultado = 1
	END
	ELSE
		SET @Mensaje = 'La carrera a eliminar está relacionada a Control de Usuario.'
END
GO

----------------------------------------------SP - ControlUsuario----------------------------------------------
CREATE PROCEDURE sp_RegistrarCU
(
    @IdUsuario varchar(50),
	@fecha date,
    @HoraEntrada time,
    @HoraSalida time,
    @TipoActividad int,
    @CantidadAlumnos int,
    @Semestre int,
    @IdCarrera int,
    @Area int,
	@Resultado int output
)
AS
BEGIN
	--SET NOCOUNT ON; --Esto sirve para que no muestre las filas registradas en la consola

	SET @Resultado = 0
	BEGIN
		INSERT INTO ControlUsuario (IdUsuario,fecha, HoraEntrada,HoraSalida, TipoActividad, CantidadAlumnos, Semestre, IdCarrera, Area)
		VALUES (@IdUsuario,@fecha, @HoraEntrada,@HoraSalida, @TipoActividad, @CantidadAlumnos, @Semestre, @IdCarrera, @Area)
        SET @Resultado = scope_identity()
	END
END
GO

CREATE PROCEDURE sp_EditarCU
(
	@IdRegistro int,
    @fecha date,
    @IdUsuario varchar(50),
    @HoraEntrada time,
    @HoraSalida time,
    @TipoActividad varchar(100),
    @CantidadAlumnos int,
    @Semestre int,
    @IdCarrera int,
    @Area int,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF EXISTS (SELECT * FROM ControlUsuario WHERE IdRegistro = @IdRegistro)
    BEGIN
        UPDATE ControlUsuario
        SET
            fecha = @fecha,
            IdUsuario = @IdUsuario,
            HoraEntrada = @HoraEntrada,
            HoraSalida = @HoraSalida,
            TipoActividad = @TipoActividad,
            CantidadAlumnos = @CantidadAlumnos,
            Semestre = @Semestre,
            IdCarrera = @IdCarrera,
            Area = @Area
        WHERE IdRegistro = @IdRegistro;
		SET @Resultado = 1
	END
	ELSE
		SET @Mensaje = 'El registro con la id solicitada no fue encontrado.'
END
GO

CREATE PROCEDURE sp_SalidaCU
(
	@IdRegistro int,
    @HoraSalida time,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF EXISTS (SELECT * FROM ControlUsuario WHERE IdRegistro = @IdRegistro)
    BEGIN
        UPDATE ControlUsuario
        SET
            HoraSalida = @HoraSalida
        WHERE IdRegistro = @IdRegistro;
		SET @Resultado = 1
	END
	ELSE
		SET @Mensaje = 'El registro con la id solicitada no fue encontrado.'
END
GO

CREATE PROCEDURE sp_EliminarCU
(
	@IdRegistro int,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM ControlUsuario CU INNER JOIN Bitacora B 
	ON CU.IdRegistro = B.IdRegistro WHERE CU.IdRegistro = @IdRegistro)
	BEGIN
		DELETE FROM ControlUsuario WHERE IdRegistro = @IdRegistro
		SET @Resultado = 1
	END
	ELSE
		SET @Mensaje = 'El registro a eliminar está relacionado a Bitácora.'
END
GO

----------------------------------------------SP - Bitácora----------------------------------------------
CREATE PROCEDURE sp_RegistrarBitacora
(
	@NombreActividad varchar(100),
    @IdRegistro int,
    @Observaciones varchar(150),
	@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	BEGIN
		INSERT INTO Bitacora (NombreActividad, IdRegistro, Observaciones)
        VALUES (@NombreActividad, @IdRegistro, @Observaciones);
        SET @Resultado = scope_identity()
	END
END
GO

CREATE PROCEDURE sp_EditarBitacora
(
    @IdPractica int,
    @NombreActividad varchar(100),
    @IdRegistro int,
    @Observaciones varchar(150),
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF EXISTS (SELECT * FROM Bitacora WHERE IdPractica = @IdPractica)
    BEGIN
        UPDATE Bitacora
        SET
			NombreActividad = @NombreActividad,
            IdRegistro = @IdRegistro,
            Observaciones = @Observaciones
        WHERE IdPractica = @IdPractica
	END
	ELSE
		SET @Mensaje = 'El registro con la id solicitada no fue encontrado.'
END
GO

CREATE PROCEDURE sp_EliminarBitacora
(
    @IdPractica int,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN
	SET @Resultado = 0
	IF EXISTS (SELECT * FROM Bitacora WHERE IdPractica = @IdPractica)
	BEGIN
		DELETE FROM Bitacora WHERE IdPractica = @IdPractica
		SET @Resultado = 1
	END
	ELSE
		SET @Mensaje = 'El registro a eliminar no fue encontrado.'
END
GO

--CREATE PROCEDURE sp_
--(
	
--)
--AS
--BEGIN
	
--END
--GO