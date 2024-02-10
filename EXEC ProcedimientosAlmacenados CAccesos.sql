USE UDP_CONTROL
GO

----------------------------------------------SP - Comprobaciones----------------------------------------------
-- Nombre, Resultado
EXEC sp_RegistrarEdificio 'N',1
-- Id,nombre, mensaje, resultado
EXEC sp_EditarEdificio 6,'Edits','',1
-- id, mensaje, resultado
EXEC sp_EliminarEdificio 6,'',1

-- id, nombre, idedificio, resultado, mensaje
EXEC sp_RegistrarAreas 8,'Pruebas',1,1,''
-- id, nombre, idedificio, resultado, mensaje
EXEC sp_EditarAreas 8,'Edit',1,1,''
-- id, resultado, mensaje
EXEC sp_EliminarAreas 8,1,''

-- Nombre, Resultado
EXEC sp_RegistrarCarreras 'Nueva',1
-- id, nombre, mensaje, resultado
EXEC sp_EditarCarreras 9,'Edit','',1
-- id, mensaje, resultado
EXEC sp_EliminarCarreras 9,'',1

-- idusr, fecha, horaen, horasal, actividad, cantalum, semestre, idcarrera, area, resultado
EXEC sp_RegistrarCU '456','','08:00:00','',1,30,7,3,1,1
-- id, fecha, idusr, horaen,horasal, actividad, cantalum, semestre, idcarrera, area, mensaje, resultado
EXEC sp_EditarCU 11,'','234','09:00:00','14:00:00',1,30,7,1,1,'',1
-- id, mensaje, resultado
EXEC sp_EliminarCU 11,'',1
-- id, hsalida, mensaje, resultado
EXEC sp_SalidaCU 4,'14:00','',1

-- NamActiv, idcu, observaciones, resultado
EXEC sp_RegistrarBitacora 'Deslechado',1,'Faltaron tazones',1
-- idprac, NamActiv, idcu, observaciones, mensaje, resultado
EXEC sp_EditarBitacora 11,'Deslechado Edit',1,'Faltaron tazones','',1
-- idprac, mensaje, resultado
EXEC sp_EliminarBitacora 11,'',1