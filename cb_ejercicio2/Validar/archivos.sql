USE FilesDB;

CREATE TABLE cbt_procesodeanalisis(
	ProcesoDeAnalisisId int identity primary key,
	Comentarios varchar(150)
)

CREATE TABLE cbt_procesodecarga(
	ProcesoID int identity primary key,
	NombreArchivoRespaldo varchar(300)
)

CREATE TABLE cbt_procesodeanalisis_carga(
	ProcesoDeAnalisisId int,
	ProcesoID int,

	CONSTRAINT FK_procesodeanalisis FOREIGN KEY (ProcesoDeAnalisisId)
    REFERENCES cbt_procesodeanalisis(ProcesoDeAnalisisId),

	CONSTRAINT FK_procesodecarga FOREIGN KEY (ProcesoID)
    REFERENCES cbt_procesodecarga(ProcesoID)
)

INSERT INTO cbt_procesodeanalisis VALUES('Proceso 1');
INSERT INTO cbt_procesodeanalisis VALUES('Proceso 2');

INSERT INTO cbt_procesodecarga VALUES('Archivo1.txt');
INSERT INTO cbt_procesodecarga VALUES('Archivo2.pdf');
INSERT INTO cbt_procesodecarga VALUES('Archivo3.txt');
INSERT INTO cbt_procesodecarga VALUES('Archivo4.pdf');

INSERT INTO cbt_procesodeanalisis_carga VALUES(1, 1);
INSERT INTO cbt_procesodeanalisis_carga VALUES(1, 2);
INSERT INTO cbt_procesodeanalisis_carga VALUES(2, 3);
INSERT INTO cbt_procesodeanalisis_carga VALUES(2, 4);

CREATE PROC cbt_procesodeanalisis_listado()
as
begin
	select * from cbt_procesodeanalisis;
end

CREATE PROC cbt_procesodeanalisis_archivoscarga(
	@ProcesoDeAnalisisId int
)
as
begin
	SELECT pdc.ProcesoID, pdc.NombreArchivoRespaldo
	FROM cbt_procesodecarga as pdc
	INNER JOIN cbt_procesodeanalisis_carga as pdac
		ON pdc.ProcesoID = pdac.ProcesoID
	INNER JOIN cbt_procesodeanalisis as pda
		ON pda.ProcesoDeAnalisisId = pdac.ProcesoDeAnalisisId
	WHERE pdac.ProcesoDeAnalisisId = @ProcesoDeAnalisisId
end
