CREATE TABLE [dbo].[Activities]
(
	[Id] INT NOT NULL IDENTITY(1,1), 
    [UserId] INT NOT NULL, 
    [CurrentProjectId] INT NOT NULL, 
    [ActivityTypeId] INT NOT NULL, 
    [InvokeTime] DATETIME NULL,

	CONSTRAINT PK_Activities_Id PRIMARY KEY (Id),

	CONSTRAINT FK_Activities_ActivitiesTypes_Id FOREIGN KEY (ActivityTypeId) REFERENCES  ActivitiesTypes (Id)

)
