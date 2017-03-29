CREATE TABLE [dbo].[Teachers]
(
	[codice_insegnanti] INT NOT NULL PRIMARY KEY, 
    [materia_insegnata] NVARCHAR(50) NOT NULL DEFAULT 'nessuna', 
    [seconda_materia_insegnata] NVARCHAR(50) NOT NULL DEFAULT 'nessuna', 
    [cattedra] INT NOT NULL DEFAULT 0, 
    [scuola_attuale] NVARCHAR(50) NOT NULL DEFAULT 'dissocupato', 
    [utente_login] NVARCHAR(50) NOT NULL, 
    [password] NVARCHAR(50) NOT NULL, 
    [telefono] INT NULL, 
    [cellulare] INT NULL, 
    [email] NVARCHAR(50) NULL, 
    [Nome] NVARCHAR(50) NULL, 
    [Cognome] NVARCHAR(50) NULL, 
    [data_di_nascita] DATE NULL
)
