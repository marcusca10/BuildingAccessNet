CREATE TABLE [dbo].[Tenants] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [Name]            NVARCHAR (MAX)   NULL,
    [Caption]         NVARCHAR (MAX)   NULL,
    [MetadataAddress] NVARCHAR (MAX)   NULL,
    [Realm]           NVARCHAR (MAX)   NULL,
    [Owner]           NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_dbo.Tenants] PRIMARY KEY CLUSTERED ([Id] ASC)
);

