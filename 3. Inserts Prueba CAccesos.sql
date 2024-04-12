USE UDP_CONTROL;
GO

INSERT INTO Edificio (NombreEdificio)
VALUES ('Unidad de Prácticas'), ('Edificio A'), ('Edificio E'), ('Edificio M'), ('Centro de Cómputo');

INSERT INTO Areas (NombreArea, IdEdificio)
VALUES ('Taller de Línea de Producción', 1),
       ('Taller de Manufactura Convencional', 1),
       ('Laboratorio de Manufactura Avanzada', 3),
       ('Laboratorio de Electromecánica', 1),
	   ('Laboratorio de Análisis de los Alimentos', 1),
       ('Laboratorio de Metrología y Pruebas', 1),
       ('Laboratorio de Neumática', 1);
       
INSERT INTO tipo_usuario (nombre_tipo)
VALUES ('Estudiante'), ('Docente');

INSERT INTO usuario (IdUsuario, Nombre, Apellidos, Tipo)
VALUES 
('20100001', 'Luis', 'Martinez Lopez', 1),
('20100002', 'Ana', 'Sanchez Garcia', 1),
('20100003', 'Pedro', 'Gonzalez Torres', 1),
('20100004', 'Mariana','Fuentes Ortiz', 1),
('20100005', 'Pablo','Cruz Jimenez', 1),
('20100007', 'Karla','Flores Pardo', 1),
('20100008', 'Eduardo','Rios Carrillo', 1),  
('20100009', 'Valeria','Sanchez Reyes', 1),
('20100011', 'Andres','Perez Estrada', 1),
('20100012', 'Fernanda','Moreno Vega', 1),
('20100013', 'Alejandro','Cortes Martinez', 1),
('20100014', 'Daniela','Leon Cervantes', 1),
('20100015', 'Samuel','Ramos Lopez', 1),
('20100016', 'Jimena','Sanchez Perez', 1),
('20100017', 'Oscar','Gutierrez Ramirez', 1),
('20100018', 'Elisa','Hernandez Ortiz', 1),
('20100019', 'Diego','Morales Flores', 1),  
('20100020', 'Camila','Gomez Cruz', 1),
('456', 'Alma', 'Rodriguez Santos', 2),
('789', 'Cesar', 'Mora Castillo', 2),
('234', 'Leon', 'Castro Castillo', 2);
       
INSERT INTO Carreras (NombreCarrera) 
VALUES	('Ing. en Innovación Agrícola Sustentable'),
		('Ing. Forestal'),
		('Ing. en Industrias Alimentarias'),
		('Ing. Electromecánica'),
		('Ing. Industrial'),
		('Ing. Informática'),
		('Contador Público'),
		('Gastronomía');

INSERT INTO TipoActividad (NombreActividad)
VALUES	('Práctica'),
		('Clase')

INSERT INTO ControlUsuario (IdUsuario, TipoActividad, CantidadAlumnos, Semestre, IdCarrera,Area)
VALUES	('456', 1, 15, 3, 5,2),
		('789', 1, 20, 5, 6,2), 
		('234', 1, 18, 7, 7,1),
		('456', 2, 12, 9, 8,3),
		('20100001', 2, 10, 4, 4,5),
		('20100005', 2, 8, 2, 3,6),  
		('20100012', 1, 22, 6, 6,1),
		('20100017', 1, 6, 4, 5,2),
		('20100020', 1, 14, 5, 4,2),
		('789', 1, 35, 8, 6,2);
       
INSERT INTO Bitacora (NombreActividad, IdRegistro, Observaciones)
VALUES	('Práctica de Manufactura de piezas', 3, 'Faltó materia prima'),
		('Práctica de Java', 4, 'Equipos con problemas'),
		('Práctica de amplificadores', 5, 'Resultados exitosos'),
		('Práctica Elaboración de quesos', 1, 'Problemas con pH'),
		('Práctica de HTML y CSS', 2, 'Alumnos participativos'),
		('Práctica Balance General', 7, 'Algún error en cifras'),
		('Práctica de metrología dimensional', 8, 'Todo correcto'),  
		('Práctica de motores de combustión', 9, 'Falla en equipo'),
		('Práctica de MySQL', 6, 'Lenta conexión a internet'),
		('Práctica de torno convencional ', 10, 'Pieza defectuosa');