CREATE TABLE [dbo].[ActivitiesTypes]
(
	[Id] INT NOT NULL IDENTITY(1,1),

	[Name] NVARCHAR(50) NOT NULL, 

    CONSTRAINT PK_ActivitiesTypes_Id PRIMARY KEY (Id),
	CONSTRAINT UN_ActivitiesTypes_Name UNIQUE (Name)

)
