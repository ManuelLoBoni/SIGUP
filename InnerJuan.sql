SELECT CU.IdRegistro, CONVERT(char(10), CU.fecha, 103)Fecha, Us.IdUsuario, CONCAT(Us.Nombre,' ',Us.Apellidos)Usuario, CU.HoraEntrada,
CU.HoraSalida, TA.IdActividad, TA.NombreActividad, CU.CantidadAlumnos AS Alumnos,
CU.Semestre,Ca.IdCarrera,Ca.NombreCarrera AS Carrera,a.IdArea,a.NombreArea as Area
from ControlUsuario CU
INNER JOIN TipoActividad TA ON TA.IdActividad = CU.TipoActividad
INNER JOIN usuario Us ON Us.IdUsuario = CU.IdUsuario
INNER JOIN Carreras Ca ON CU.IdCarrera = Ca.IdCarrera
INNER JOIN Areas a on a.IdArea = CU.Area order by CU.IdRegistro

select IdHerramienta,  CONCAT( Nombre,' / ',cantidad) AS Nombre, Activo from Herramienta

SELECT IdArea, NombreArea, A.IdEdificio AS IdEd, E.NombreEdificio AS NEdi FROM Areas A INNER JOIN Edificio E ON E.IdEdificio = A.IdEdificio

SELECT Us.IdUsuario, CONCAT(Us.Nombre,' ',Us.Apellidos)Usuario
FROM  usuario Us
INNER JOIN tipo_usuario TU ON Us.Tipo = TU.IdTipo WHERE US.Tipo = 2