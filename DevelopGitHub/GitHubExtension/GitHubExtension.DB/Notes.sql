CREATE TABLE [dbo].[Notes]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[UserId] NVARCHAR (128) NOT NULL,
	[CollaboratorId] NVARCHAR (128) NOT NULL,
	[Body] NVARCHAR (1024) NULL,
	CONSTRAINT PK_Notes_Id PRIMARY KEY (Id),	
	CONSTRAINT [FK_Notes_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
	CONSTRAINT [FK_Notes_Collaborators] FOREIGN KEY ([CollaboratorId]) REFERENCES [dbo].[Users] ([Id]),
	CONSTRAINT [UQ_UserId_CollaboratorId] UNIQUE ([UserId],[CollaboratorId])
)
