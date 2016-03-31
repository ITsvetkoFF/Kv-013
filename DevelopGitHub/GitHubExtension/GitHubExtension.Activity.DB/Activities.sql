CREATE TABLE [dbo].[Activities]
(
	[Id] INT NOT NULL IDENTITY(1,1), 
    [UserId] VARCHAR(50) NOT NULL, 
    [CurrentProjectId] INT NOT NULL, 
    [ActivityType] INT NOT NULL, 
    [InvokeTime] DATETIME NULL,

	CONSTRAINT PK_Activities_Id PRIMARY KEY (Id),
)
