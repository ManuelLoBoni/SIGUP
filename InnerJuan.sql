select IdHerramienta,  CONCAT( Nombre,' / ',cantidad) AS Nombre, Activo from Herramienta

SELECT IdArea, NombreArea, A.IdEdificio AS IdEd, E.NombreEdificio AS NEdi FROM Areas A INNER JOIN Edificio E ON E.IdEdificio = A.IdEdificio

SELECT Us.IdUsuario, CONCAT(Us.Nombre,' ',Us.Apellidos)Usuario
FROM  usuario Us
INNER JOIN tipo_usuario TU ON Us.Tipo = TU.IdTipo WHERE US.Tipo = 2


SELECT CA.IdRegistro, CONVERT(char(10), CA.fecha, 103)Fecha, Us.IdUsuario, CONCAT(Us.Nombre,' ',Us.Apellidos)Usuario, CA.HoraEntrada,
CA.HoraSalida, TA.IdActividad, TA.NombreActividad, CA.CantidadAlumnos AS Alumnos,
CA.Semestre,Cr.IdCarrera,Cr.NombreCarrera AS Carrera,a.IdArea,a.NombreArea as Area
from ControlAccesos CA
INNER JOIN TipoActividad TA ON TA.IdActividad = CA.TipoActividad
INNER JOIN usuario Us ON Us.IdUsuario = CA.IdUsuario
INNER JOIN Carreras Cr ON CA.IdCarrera = Cr.IdCarrera
INNER JOIN Areas a on a.IdArea = CA.Area order by CA.IdRegistro DESC

SELECT CA.IdRegistro, CONVERT(char(10), CA.fecha, 103)Fecha, Us.IdUsuario, CONCAT(Us.Nombre,' ',Us.Apellidos)Usuario, CA.HoraEntrada,
CA.HoraSalida, TA.IdActividad, TA.NombreActividad, CA.CantidadAlumnos AS Alumnos,
CA.Semestre,Cr.IdCarrera,Cr.NombreCarrera AS Carrera,a.IdArea,a.NombreArea as Area
from ControlAccesos CA
INNER JOIN TipoActividad TA ON TA.IdActividad = CA.TipoActividad
INNER JOIN usuario Us ON Us.IdUsuario = CA.IdUsuario
INNER JOIN Carreras Cr ON CA.IdCarrera = Cr.IdCarrera
INNER JOIN Areas a on a.IdArea = CA.Area where TA.IdActividad = 1 order by CA.IdRegistro DESC

select IdPractica, CONVERT(char(10), CU.fecha, 103)Fecha,CU.IdUsuario, CONCAT(US.Nombre,' ',US.Apellidos)Usuario,Bitacora.NombreActividad, CU.CantidadAlumnos, CA.NombreCarrera,Semestre,CU.IdRegistro, Observaciones from Bitacora
inner join ControlAccesos CU on Bitacora.IdRegistro = CU.IdRegistro
inner join Carreras CA ON CA.IdCarrera = CU.IdCarrera
inner join usuario US ON CU.IdUsuario = US.IdUsuario
inner join TipoActividad TA ON TA.IdActividad = 1 order by Bitacora.IdPractica DESC

SELECT *
FROM ControlAccesos CA
WHERE NOT EXISTS (
    SELECT 1
    FROM Bitacora B
    WHERE CA.IdRegistro = B.IdRegistro
);

--Seleccion cuando el id es 1 y no existe un duplicado, es decir se listan solo los registros que no hayan sido asignados
SELECT CA.IdRegistro, CONVERT(char(10), CA.fecha, 103) AS Fecha, Us.IdUsuario, CONCAT(Us.Nombre,' ',Us.Apellidos) AS Usuario,
CA.HoraEntrada, CA.HoraSalida, TA.IdActividad, TA.NombreActividad, CA.CantidadAlumnos AS Alumnos,
CA.Semestre, Cr.IdCarrera, Cr.NombreCarrera AS Carrera, a.IdArea, a.NombreArea AS Area
FROM ControlAccesos CA
INNER JOIN TipoActividad TA ON TA.IdActividad = CA.TipoActividad
INNER JOIN usuario Us ON Us.IdUsuario = CA.IdUsuario
INNER JOIN Carreras Cr ON CA.IdCarrera = Cr.IdCarrera
INNER JOIN Areas a ON a.IdArea = CA.Area WHERE TA.IdActividad = 1
AND NOT EXISTS (SELECT 1 FROM Bitacora B WHERE CA.IdRegistro = B.IdRegistro)
ORDER BY CA.IdRegistro DESC;
