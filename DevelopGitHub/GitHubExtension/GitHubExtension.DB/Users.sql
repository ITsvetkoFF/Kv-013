CREATE TABLE [dbo].[Users] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [GitHubUrl]            NVARCHAR (MAX) NULL,
    [ProviderId]           INT            NOT NULL,
    [AvatarUrl]            NVARCHAR (MAX) NULL,
    [IsMailVisible]        BIT            NOT NULL,
    [Email]                NVARCHAR (MAX) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);