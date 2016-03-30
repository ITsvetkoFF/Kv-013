CREATE TABLE [dbo].[ActivitiesTypes]
(
	[Id] INT NOT NULL IDENTITY(1,1), 
    [Name] VARCHAR(50) NOT NULL,

	CONSTRAINT UN_Name UNIQUE(Name),

	CONSTRAINT PK_ActivitiesType_Id PRIMARY KEY (Id),


)
