CREATE TABLE [dbo].[Categories]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Description] NVARCHAR(128) NULL,

	CONSTRAINT PK_Categories_Id PRIMARY KEY (Id)
)
