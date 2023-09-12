
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/13/2023 00:39:39
-- Generated from EDMX file: D:\VisualStudiosProjects\FIT5032_TB_xray_report_system_v2\Models\TB_xray_systems.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
CREATE DATABASE [Database1];
GO
USE [Database1];
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

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [user_id] int IDENTITY(1,1) NOT NULL,
    [user_username] nvarchar(max)  NOT NULL,
    [user_password] nvarchar(max)  NOT NULL,
    [user_email] nvarchar(max)  NOT NULL,
    [user_fullname] nvarchar(max)  NOT NULL,
    [user_status] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DeletionRequestSet'
CREATE TABLE [dbo].[DeletionRequestSet] (
    [dr_id] int IDENTITY(1,1) NOT NULL,
    [Patient_patient_id] int  NOT NULL,
    [dr_reason] nvarchar(max)  NOT NULL,
    [dr_status] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ReportSet'
CREATE TABLE [dbo].[ReportSet] (
    [report_id] int IDENTITY(1,1) NOT NULL,
    [report_content] nvarchar(max)  NOT NULL,
    [ScreeningHistoryReport_Report_sh_id] int  NOT NULL
);
GO

-- Creating table 'ScreeningHistorySet'
CREATE TABLE [dbo].[ScreeningHistorySet] (
    [sh_id] int IDENTITY(1,1) NOT NULL,
    [sh_datetime] datetime  NOT NULL,
    [sh_additional] nvarchar(max)  NOT NULL,
    [MedicalProfessional_user_id] int  NOT NULL,
    [Patient_user_id] int  NOT NULL
);
GO

-- Creating table 'ScreeningImageSet'
CREATE TABLE [dbo].[ScreeningImageSet] (
    [si_id] int IDENTITY(1,1) NOT NULL,
    [si_file] nvarchar(max)  NOT NULL,
    [si_additional] nvarchar(max)  NOT NULL,
    [ScreeningHistory_sh_id] int  NOT NULL
);
GO

-- Creating table 'UserSet_Patient'
CREATE TABLE [dbo].[UserSet_Patient] (
    [patient_id] int IDENTITY(1,1) NOT NULL,
    [patient_address] nvarchar(max)  NOT NULL,
    [patient_phone] nvarchar(max)  NOT NULL,
    [patient_medical_history] nvarchar(max)  NOT NULL,
    [user_id] int  NOT NULL
);
GO

-- Creating table 'UserSet_MedicalProfessional'
CREATE TABLE [dbo].[UserSet_MedicalProfessional] (
    [mp_id] int IDENTITY(1,1) NOT NULL,
    [user_id] int  NOT NULL
);
GO

-- Creating table 'UserSet_Administrator'
CREATE TABLE [dbo].[UserSet_Administrator] (
    [admin_id] int IDENTITY(1,1) NOT NULL,
    [user_id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [user_id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([user_id] ASC);
GO

-- Creating primary key on [dr_id] in table 'DeletionRequestSet'
ALTER TABLE [dbo].[DeletionRequestSet]
ADD CONSTRAINT [PK_DeletionRequestSet]
    PRIMARY KEY CLUSTERED ([dr_id] ASC);
GO

-- Creating primary key on [report_id] in table 'ReportSet'
ALTER TABLE [dbo].[ReportSet]
ADD CONSTRAINT [PK_ReportSet]
    PRIMARY KEY CLUSTERED ([report_id] ASC);
GO

-- Creating primary key on [sh_id] in table 'ScreeningHistorySet'
ALTER TABLE [dbo].[ScreeningHistorySet]
ADD CONSTRAINT [PK_ScreeningHistorySet]
    PRIMARY KEY CLUSTERED ([sh_id] ASC);
GO

-- Creating primary key on [si_id] in table 'ScreeningImageSet'
ALTER TABLE [dbo].[ScreeningImageSet]
ADD CONSTRAINT [PK_ScreeningImageSet]
    PRIMARY KEY CLUSTERED ([si_id] ASC);
GO

-- Creating primary key on [user_id] in table 'UserSet_Patient'
ALTER TABLE [dbo].[UserSet_Patient]
ADD CONSTRAINT [PK_UserSet_Patient]
    PRIMARY KEY CLUSTERED ([user_id] ASC);
GO

-- Creating primary key on [user_id] in table 'UserSet_MedicalProfessional'
ALTER TABLE [dbo].[UserSet_MedicalProfessional]
ADD CONSTRAINT [PK_UserSet_MedicalProfessional]
    PRIMARY KEY CLUSTERED ([user_id] ASC);
GO

-- Creating primary key on [user_id] in table 'UserSet_Administrator'
ALTER TABLE [dbo].[UserSet_Administrator]
ADD CONSTRAINT [PK_UserSet_Administrator]
    PRIMARY KEY CLUSTERED ([user_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Patient_patient_id] in table 'DeletionRequestSet'
ALTER TABLE [dbo].[DeletionRequestSet]
ADD CONSTRAINT [FK_PatientDeletionRequest]
    FOREIGN KEY ([Patient_patient_id])
    REFERENCES [dbo].[UserSet_Patient]
        ([user_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientDeletionRequest'
CREATE INDEX [IX_FK_PatientDeletionRequest]
ON [dbo].[DeletionRequestSet]
    ([Patient_patient_id]);
GO

-- Creating foreign key on [ScreeningHistory_sh_id] in table 'ScreeningImageSet'
ALTER TABLE [dbo].[ScreeningImageSet]
ADD CONSTRAINT [FK_ScreeningHistoryScreeningImage]
    FOREIGN KEY ([ScreeningHistory_sh_id])
    REFERENCES [dbo].[ScreeningHistorySet]
        ([sh_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScreeningHistoryScreeningImage'
CREATE INDEX [IX_FK_ScreeningHistoryScreeningImage]
ON [dbo].[ScreeningImageSet]
    ([ScreeningHistory_sh_id]);
GO

-- Creating foreign key on [MedicalProfessional_user_id] in table 'ScreeningHistorySet'
ALTER TABLE [dbo].[ScreeningHistorySet]
ADD CONSTRAINT [FK_ScreeningHistoryMedicalProfessional]
    FOREIGN KEY ([MedicalProfessional_user_id])
    REFERENCES [dbo].[UserSet_MedicalProfessional]
        ([user_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScreeningHistoryMedicalProfessional'
CREATE INDEX [IX_FK_ScreeningHistoryMedicalProfessional]
ON [dbo].[ScreeningHistorySet]
    ([MedicalProfessional_user_id]);
GO

-- Creating foreign key on [Patient_user_id] in table 'ScreeningHistorySet'
ALTER TABLE [dbo].[ScreeningHistorySet]
ADD CONSTRAINT [FK_ScreeningHistoryPatient]
    FOREIGN KEY ([Patient_user_id])
    REFERENCES [dbo].[UserSet_Patient]
        ([user_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScreeningHistoryPatient'
CREATE INDEX [IX_FK_ScreeningHistoryPatient]
ON [dbo].[ScreeningHistorySet]
    ([Patient_user_id]);
GO

-- Creating foreign key on [ScreeningHistoryReport_Report_sh_id] in table 'ReportSet'
ALTER TABLE [dbo].[ReportSet]
ADD CONSTRAINT [FK_ScreeningHistoryReport]
    FOREIGN KEY ([ScreeningHistoryReport_Report_sh_id])
    REFERENCES [dbo].[ScreeningHistorySet]
        ([sh_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ScreeningHistoryReport'
CREATE INDEX [IX_FK_ScreeningHistoryReport]
ON [dbo].[ReportSet]
    ([ScreeningHistoryReport_Report_sh_id]);
GO

-- Creating foreign key on [user_id] in table 'UserSet_Patient'
ALTER TABLE [dbo].[UserSet_Patient]
ADD CONSTRAINT [FK_Patient_inherits_User]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[UserSet]
        ([user_id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [user_id] in table 'UserSet_MedicalProfessional'
ALTER TABLE [dbo].[UserSet_MedicalProfessional]
ADD CONSTRAINT [FK_MedicalProfessional_inherits_User]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[UserSet]
        ([user_id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [user_id] in table 'UserSet_Administrator'
ALTER TABLE [dbo].[UserSet_Administrator]
ADD CONSTRAINT [FK_Administrator_inherits_User]
    FOREIGN KEY ([user_id])
    REFERENCES [dbo].[UserSet]
        ([user_id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------