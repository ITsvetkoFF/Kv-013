CREATE TABLE [dbo].[Templates]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Content] NVARCHAR (MAX) NOT NULL,
	[Type] NVARCHAR(50) NOT NULL, 
	[CategoryId] INT NULL,
	[Description] NVARCHAR(100) NOT NULL, 
	CONSTRAINT PK_Templates_Id PRIMARY KEY (Id), 
	CONSTRAINT FK_Templates_Categories FOREIGN KEY (CategoryId) REFERENCES Categories (Id)

)
