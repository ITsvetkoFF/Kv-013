CREATE TABLE [dbo].[Activities]
(
    [Id] INT NOT NULL IDENTITY(1,1), 
    [UserId] NVARCHAR(128) NOT NULL, 
    [CurrentRepositoryId] INT NOT NULL, 
    [ActivityTypeId] INT NOT NULL, 
    [InvokeTime] DATETIME NULL,
    [ImageUrl] NVARCHAR (128) NULL,

    [Message] NVARCHAR(MAX) NULL, 
    CONSTRAINT PK_Activities_Id PRIMARY KEY (Id),
    CONSTRAINT FK_Activities_ActivitiesTypes FOREIGN KEY (ActivityTypeId) REFERENCES ActivitiesTypes(Id),
    CONSTRAINT FK_Activities_Users FOREIGN KEY (UserId) REFERENCES Users(Id) 
)