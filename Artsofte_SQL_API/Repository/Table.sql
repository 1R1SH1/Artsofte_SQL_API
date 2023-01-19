CREATE TABLE [dbo].[Employee] (
    [Id]            INT primary key IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) NULL,
    [SurName]       NVARCHAR (50) NULL,
    [Age]           INT  NULL,
    [Gender]        NVARCHAR (50) NULL,
    [DepartmentId]  INT NULL,
    [LanguageId]    INT NULL
);

CREATE TABLE [dbo].[Department] (
    [Id]             INT primary key IDENTITY (1, 1) NOT NULL,
    [DepartmentName] NVARCHAR (50) NULL,
    [Floor]          INT NULL
);

create table [dbo].[ProgrammingLanguage]
(
    [Id]           INT primary key IDENTITY (1, 1) NOT NULL, 
    [LanguageName] NVARCHAR (50) NULL
);

SET IDENTITY_INSERT [dbo].[Employee] ON
INSERT INTO [dbo].[Employee] ([Id], [Name], [SurName], [Age], [Gender], [DepartmentId], [LanguageId]) VALUES (1, 'Bred', 'Pit', 37, 'Male', 1, 1)
INSERT INTO [dbo].[Employee] ([Id], [Name], [SurName], [Age], [Gender], [DepartmentId], [LanguageId]) VALUES (2, 'Angelina', 'Pit', 37, 'Female', 1, 1)
INSERT INTO [dbo].[Employee] ([Id], [Name], [SurName], [Age], [Gender], [DepartmentId], [LanguageId]) VALUES (3, 'Tom', 'Cruze', 37, 'Male', 2, 2)
INSERT INTO [dbo].[Employee] ([Id], [Name], [SurName], [Age], [Gender], [DepartmentId], [LanguageId]) VALUES (4, 'Daniel', 'Crayge', 37, 'Male', 2, 2)
INSERT INTO [dbo].[Employee] ([Id], [Name], [SurName], [Age], [Gender], [DepartmentId], [LanguageId]) VALUES (5, 'Bredly', 'Coper', 37, 'Male', 3, 3)
INSERT INTO [dbo].[Employee] ([Id], [Name], [SurName], [Age], [Gender], [DepartmentId], [LanguageId]) VALUES (6, 'Shepard', 'Shepard', 37, 'Male', 3, 3)
INSERT INTO [dbo].[Employee] ([Id], [Name], [SurName], [Age], [Gender], [DepartmentId], [LanguageId]) VALUES (7, 'Shon', 'Conor', 37, 'Male', 4, 4)
INSERT INTO [dbo].[Employee] ([Id], [Name], [SurName], [Age], [Gender], [DepartmentId], [LanguageId]) VALUES (8, 'Arnold', 'Schwarzenegger', 37, 'Male', 4, 4)
SET IDENTITY_INSERT [dbo].[Employee] OFF

SELECT * FROM [Employee]

SET IDENTITY_INSERT [dbo].[Department] ON
INSERT INTO [dbo].[Department] ([Id], [DepartmentName], [Floor]) VALUES (1, 'Movie', 1)
INSERT INTO [dbo].[Department] ([Id], [DepartmentName], [Floor]) VALUES (2, 'Spy', 1)
INSERT INTO [dbo].[Department] ([Id], [DepartmentName], [Floor]) VALUES (3, 'Space', 1)
INSERT INTO [dbo].[Department] ([Id], [DepartmentName], [Floor]) VALUES (4, 'Terminators', 1)
SET IDENTITY_INSERT [dbo].[Department] OFF

SELECT * FROM [Department]

SET IDENTITY_INSERT [dbo].[ProgrammingLanguage] ON
INSERT INTO [dbo].[ProgrammingLanguage] ([Id], [LanguageName]) VALUES (1, 'C#')
INSERT INTO [dbo].[ProgrammingLanguage] ([Id], [LanguageName]) VALUES (2, 'PHP')
INSERT INTO [dbo].[ProgrammingLanguage] ([Id], [LanguageName]) VALUES (3, 'Pyton')
INSERT INTO [dbo].[ProgrammingLanguage] ([Id], [LanguageName]) VALUES (4, 'Go')
SET IDENTITY_INSERT [dbo].[ProgrammingLanguage] OFF

SELECT * FROM [ProgrammingLanguage]

SELECT D."DepartmentName", D."Floor", PL."LanguageName"
FROM Department D
LEFT JOIN Employee E
	ON D.Id = E.DepartmentId
LEFT JOIN ProgrammingLanguage PL
	ON E.LanguageId = PL.Id
    ORDER BY E.Name;
