
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/10/2016 14:59:13
-- Generated from EDMX file: c:\users\prestamo\documents\visual studio 2015\Projects\PromotorDePeliculas\ModelPeliculas\ModeloPeliculas.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PeliculasDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Peliculas'
CREATE TABLE [dbo].[Peliculas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Titulo] nvarchar(max)  NOT NULL,
    [LugarEstreno] nvarchar(max)  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [Nacionalidad] nvarchar(max)  NOT NULL,
    [FechaEstreno] datetime  NOT NULL,
    [ProductorId] int  NOT NULL,
    [DirectorId] int  NOT NULL
);
GO

-- Creating table 'Actores'
CREATE TABLE [dbo].[Actores] (
    [Id] int  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Direccion] nvarchar(max)  NOT NULL,
    [Telefono] nvarchar(max)  NOT NULL,
    [FechaNacimiento] datetime  NOT NULL
);
GO

-- Creating table 'Productor'
CREATE TABLE [dbo].[Productor] (
    [Id] int  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Direccion] nvarchar(max)  NOT NULL,
    [Telefono] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DirectorSet'
CREATE TABLE [dbo].[DirectorSet] (
    [Id] int  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Direccion] nvarchar(max)  NOT NULL,
    [Telefono] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Comentarios'
CREATE TABLE [dbo].[Comentarios] (
    [Descripcion] nvarchar(max)  NOT NULL,
    [Lugar] nvarchar(max)  NOT NULL,
    [FechaComentario] datetime  NOT NULL,
    [UsuarioId] int  NOT NULL,
    [PeliculaId] int  NOT NULL
);
GO

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Premios'
CREATE TABLE [dbo].[Premios] (
    [TipoPremio] nvarchar(max)  NOT NULL,
    [PeliculaId] int  NOT NULL,
    [CertamenId] int  NOT NULL
);
GO

-- Creating table 'Certamenes'
CREATE TABLE [dbo].[Certamenes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ciudad] nvarchar(max)  NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Papeles'
CREATE TABLE [dbo].[Papeles] (
    [Descripcion] nvarchar(max)  NOT NULL,
    [TipoPapel] nvarchar(max)  NOT NULL,
    [PeliculaId] int  NOT NULL,
    [ActorId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Peliculas'
ALTER TABLE [dbo].[Peliculas]
ADD CONSTRAINT [PK_Peliculas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Actores'
ALTER TABLE [dbo].[Actores]
ADD CONSTRAINT [PK_Actores]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Productor'
ALTER TABLE [dbo].[Productor]
ADD CONSTRAINT [PK_Productor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DirectorSet'
ALTER TABLE [dbo].[DirectorSet]
ADD CONSTRAINT [PK_DirectorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UsuarioId], [PeliculaId] in table 'Comentarios'
ALTER TABLE [dbo].[Comentarios]
ADD CONSTRAINT [PK_Comentarios]
    PRIMARY KEY CLUSTERED ([UsuarioId], [PeliculaId] ASC);
GO

-- Creating primary key on [Id] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [PeliculaId], [CertamenId] in table 'Premios'
ALTER TABLE [dbo].[Premios]
ADD CONSTRAINT [PK_Premios]
    PRIMARY KEY CLUSTERED ([PeliculaId], [CertamenId] ASC);
GO

-- Creating primary key on [Id] in table 'Certamenes'
ALTER TABLE [dbo].[Certamenes]
ADD CONSTRAINT [PK_Certamenes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ActorId], [PeliculaId] in table 'Papeles'
ALTER TABLE [dbo].[Papeles]
ADD CONSTRAINT [PK_Papeles]
    PRIMARY KEY CLUSTERED ([ActorId], [PeliculaId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProductorId] in table 'Peliculas'
ALTER TABLE [dbo].[Peliculas]
ADD CONSTRAINT [FK_PeliculaProductor]
    FOREIGN KEY ([ProductorId])
    REFERENCES [dbo].[Productor]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PeliculaProductor'
CREATE INDEX [IX_FK_PeliculaProductor]
ON [dbo].[Peliculas]
    ([ProductorId]);
GO

-- Creating foreign key on [DirectorId] in table 'Peliculas'
ALTER TABLE [dbo].[Peliculas]
ADD CONSTRAINT [FK_PeliculaDirector]
    FOREIGN KEY ([DirectorId])
    REFERENCES [dbo].[DirectorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PeliculaDirector'
CREATE INDEX [IX_FK_PeliculaDirector]
ON [dbo].[Peliculas]
    ([DirectorId]);
GO

-- Creating foreign key on [UsuarioId] in table 'Comentarios'
ALTER TABLE [dbo].[Comentarios]
ADD CONSTRAINT [FK_ComentarioUsuario]
    FOREIGN KEY ([UsuarioId])
    REFERENCES [dbo].[Usuarios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PeliculaId] in table 'Comentarios'
ALTER TABLE [dbo].[Comentarios]
ADD CONSTRAINT [FK_PeliculaComentario]
    FOREIGN KEY ([PeliculaId])
    REFERENCES [dbo].[Peliculas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PeliculaComentario'
CREATE INDEX [IX_FK_PeliculaComentario]
ON [dbo].[Comentarios]
    ([PeliculaId]);
GO

-- Creating foreign key on [PeliculaId] in table 'Premios'
ALTER TABLE [dbo].[Premios]
ADD CONSTRAINT [FK_PeliculaPremio]
    FOREIGN KEY ([PeliculaId])
    REFERENCES [dbo].[Peliculas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CertamenId] in table 'Premios'
ALTER TABLE [dbo].[Premios]
ADD CONSTRAINT [FK_PremioCertamen]
    FOREIGN KEY ([CertamenId])
    REFERENCES [dbo].[Certamenes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PremioCertamen'
CREATE INDEX [IX_FK_PremioCertamen]
ON [dbo].[Premios]
    ([CertamenId]);
GO

-- Creating foreign key on [PeliculaId] in table 'Papeles'
ALTER TABLE [dbo].[Papeles]
ADD CONSTRAINT [FK_PapelPelicula]
    FOREIGN KEY ([PeliculaId])
    REFERENCES [dbo].[Peliculas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PapelPelicula'
CREATE INDEX [IX_FK_PapelPelicula]
ON [dbo].[Papeles]
    ([PeliculaId]);
GO

-- Creating foreign key on [ActorId] in table 'Papeles'
ALTER TABLE [dbo].[Papeles]
ADD CONSTRAINT [FK_ActorPapel]
    FOREIGN KEY ([ActorId])
    REFERENCES [dbo].[Actores]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------