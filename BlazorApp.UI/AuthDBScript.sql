USE [AuthDB]
GO
/****** Object:  Table [dbo].[MasterTabAssocaition]    Script Date: 23-05-2025 15:29:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterTabAssocaition](
	[MTAId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[TabId] [int] NOT NULL,
	[TabName] [nchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[preferencesMasterTable]    Script Date: 23-05-2025 15:29:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[preferencesMasterTable](
	[GroupId] [int] NOT NULL,
	[GroupName] [nchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 23-05-2025 15:29:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](100) NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedBy] [int] NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 23-05-2025 15:29:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	[ResetToken] [nvarchar](max) NULL,
	[ResetTokenExpiration] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGridAssoc]    Script Date: 23-05-2025 15:29:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGridAssoc](
	[UGAId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[TabId] [int] NOT NULL,
	[Preferences] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserGridAssoc] PRIMARY KEY CLUSTERED 
(
	[UGAId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProjectRoleAssociation]    Script Date: 23-05-2025 15:29:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProjectRoleAssociation](
	[upraId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserProjectRoleAssociation] PRIMARY KEY CLUSTERED 
(
	[upraId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 23-05-2025 15:29:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	[RolePriority] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTabAssoc]    Script Date: 23-05-2025 15:29:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTabAssoc](
	[UTAId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TabId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_UserTabAssoc] PRIMARY KEY CLUSTERED 
(
	[UTAId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[MasterTabAssocaition] ([MTAId], [GroupId], [TabId], [TabName]) VALUES (1, 1, 1, N'UserManager                        User Manager   ')
GO
INSERT [dbo].[MasterTabAssocaition] ([MTAId], [GroupId], [TabId], [TabName]) VALUES (2, 1, 2, N'Project Manager                                   ')
GO
INSERT [dbo].[MasterTabAssocaition] ([MTAId], [GroupId], [TabId], [TabName]) VALUES (3, 1, 3, N'User Role Association                             ')
GO
INSERT [dbo].[MasterTabAssocaition] ([MTAId], [GroupId], [TabId], [TabName]) VALUES (4, 2, 1, N'Project Manager Portal                            ')
GO
INSERT [dbo].[MasterTabAssocaition] ([MTAId], [GroupId], [TabId], [TabName]) VALUES (5, 2, 2, N'Project Lead Portal                               ')
GO
INSERT [dbo].[preferencesMasterTable] ([GroupId], [GroupName]) VALUES (1, N'AdminPortal                                       ')
GO
INSERT [dbo].[preferencesMasterTable] ([GroupId], [GroupName]) VALUES (2, N'Associations Portal                               ')
GO
INSERT [dbo].[preferencesMasterTable] ([GroupId], [GroupName]) VALUES (3, N'Login                                             ')
GO
SET IDENTITY_INSERT [dbo].[Projects] ON 
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (1, N'Assets', CAST(N'2024-04-22' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (2, N'CLEAR', CAST(N'2024-04-10' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (3, N'CSC', CAST(N'2024-10-05' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (4, N'Dashboards', CAST(N'2024-10-16' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (5, N'Data Dictionary', CAST(N'2024-10-08' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (6, N'Devices', CAST(N'2024-03-04' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (7, N'ETD', CAST(N'2024-11-12' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (8, N'LATS', CAST(N'2024-06-05' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (9, N'LRC', CAST(N'2024-12-15' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (10, N'PMT', CAST(N'2024-01-06' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (11, N'Projects', CAST(N'2024-01-10' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (12, N'Push', CAST(N'2024-11-14' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (13, N'Reports', CAST(N'2024-05-22' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (14, N'Storage', CAST(N'2024-12-09' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (15, N'Surveys', CAST(N'2024-04-19' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (16, N'VOEE', CAST(N'2024-04-14' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (17, N'Work Orders', CAST(N'2024-09-18' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (18, N'Workflow', CAST(N'2024-12-04' AS Date), 4, NULL, NULL, 1)
GO
INSERT [dbo].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (20, N'AdminProject', CAST(N'2024-12-05' AS Date), 4, NULL, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[Projects] OFF
GO
SET IDENTITY_INSERT [dbo].[UserDetails] ON 
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (1, N'user1', N'WAD5pBWiLlLM1rhPYQEbvTWyZ5bi0bJ4u0QMDE3GDZxiy0L5TEV+c9FxRZHdVb5C', CAST(N'2025-05-08' AS Date), NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (2, N'user2', N'jLBeQzIu3osJBIOXxAFulkcD7HsbV962pjytAH90GKM7DbZemAAcux4r/kwU60AR', CAST(N'2025-05-08' AS Date), NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (5, N'user3', N'6iMMzR9Qoqmk/LLWjbFlyOCzxEmfMndib37mfgqaNCfh008gg4ZEiozZeWJEz7Jl', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, N'bc7cf7d3-5370-4166-be88-47a0a4ef01f3', CAST(N'2025-05-23T10:38:45.3929075' AS DateTime2))
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (6, N'user4', N'Hi/kS840GCTpDghINdczC3AxKBsgKeX/1W0MokDuqeWnfBf9x77URSMRhZGJysYB', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (7, N'user5', N'xG9ftrM20p91HRGYKjTq2xDw+1MHeYSQ83t9pcWZQRiZvJg0sOuIrG5MSUrnu9WL', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (8, N'user6', N'QDOAJLWApgRi6Hjh+kWpbxJLAZjPayWer3RxTX7BJIpru5+xQN+xm6LGslrocyji', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (9, N'user7', N'oPqhVoMpTupTxYaVxaqQclT/ybN4xSkP5o+grRkH+meMctxd+VPvbW2ouQ/VtPwI', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (10, N'user8', N'DpCSC6sm8GnvL2LabW2VoIxK67mF2yQ6+gUeuLGFz5IEUC1L15vD4z68u3poSRKz', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (11, N'user9', N'hP4wFl0dwg66wJz80I2OivRiLtWugeo7cLBYJFqlKugBpXsbfcPkk6i8lJz+LWB/', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (12, N'user10', N'7RxKZBa/bYebDcO3ysgNU+kWMexzP/DsP1v0BqiqOAMFCQlCzD4Pzb0z0+4mJrHr', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (13, N'user11', N'6L0aVpvJEMM+t3l5mK9ZM+e2rY7rcunaxjPL47eT6EU3tKJ3Lpr3TdH1WmhEPhrL', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (14, N'user12', N'5G2cKi/BwDa5E5y9Z0UK9itnBV9/ImuQHykW3etWyiZFB5JO8j+wnJNXLUXAIUt1', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (15, N'user13', N'xS1XHvdwm6qE7dz1NBPneNtuP0i/TXcTkP4C8A/s/LpP0mYj/xVjraiamoHQgAj3', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (16, N'user14', N'OBXxQLA3AzR7Epl8AOYmff5DvFIn31v2c7kdiH9QIXjGQOnGBuQA9mvHmWwujCET', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (17, N'user15', N'k1XWtxKW/6FYuQO387PA8RP1/qFQZSNqfx8ZDSikwrFuBg4FWa/cPaYRRlCyWLHb', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (18, N'user16', N'BlS39MBZKnVaQ0hl0GXPQbOHbhkzN2t10ZyBY/6kdEPnhsPRY3yRWKu1zOmgrqDm', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (19, N'user17', N'lo7CWIVrAuJ2rF7gUZChl/+oKTGBDUPk3eSncyJUqu+kVfOeE+Qkqjfh9SF6e+Sa', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (20, N'user18', N'ojkFPLpxhr/BVQTT98pCLLCgoTFHdULHZOjWIRWDFk3mB/2cf9gyOJf1v6AkEqFR', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (21, N'user19', N'5AdbN5i/h6Qm/OvGOo0P6MWbrx/PCQ2T2SJEsK3KHYjVQUoflEQ/KvgfNTSEJD9W', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [ResetToken], [ResetTokenExpiration]) VALUES (24, N'user20', N'gIypetAKtCgnirSkM8XwKs32Hmi3uhtJ9SJgZ6qNrj7bohoh/Wq90725ya9+y0JB', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[UserDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[UserGridAssoc] ON 
GO
INSERT [dbo].[UserGridAssoc] ([UGAId], [UserId], [GroupId], [TabId], [Preferences]) VALUES (5, 5, 2, 2, N'{"UserName":{"Width":"734.6625366210938px","OrderIndex":0,"Visible":true},"RoleName":{"Width":"120px","OrderIndex":1,"Visible":true},"CreatedDate":{"Width":"120px","OrderIndex":2,"Visible":true},"CreatedBy":{"Width":"120px","OrderIndex":3,"Visible":true},"ModifiedDate":{"Width":"120px","OrderIndex":4,"Visible":true},"ModifiedBy":{"Width":"120px","OrderIndex":5,"Visible":true},"IsActive":{"Width":"120px","OrderIndex":6,"Visible":true}}')
GO
INSERT [dbo].[UserGridAssoc] ([UGAId], [UserId], [GroupId], [TabId], [Preferences]) VALUES (6, 24, 2, 2, N'{"UserName":{"Width":"120px","OrderIndex":0,"Visible":true},"RoleName":{"Width":"120px","OrderIndex":1,"Visible":false},"CreatedDate":{"Width":"120px","OrderIndex":2,"Visible":true},"CreatedBy":{"Width":"120px","OrderIndex":3,"Visible":true},"ModifiedDate":{"Width":"120px","OrderIndex":4,"Visible":true},"ModifiedBy":{"Width":"120px","OrderIndex":5,"Visible":true},"IsActive":{"Width":"120px","OrderIndex":6,"Visible":true}}')
GO
SET IDENTITY_INSERT [dbo].[UserGridAssoc] OFF
GO
SET IDENTITY_INSERT [dbo].[UserProjectRoleAssociation] ON 
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (25, 1, 1, 20, CAST(N'2024-05-06' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (26, 2, 11, 1, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (27, 2, 11, 2, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (28, 2, 11, 3, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (29, 2, 11, 4, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (30, 5, 11, 5, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (31, 5, 11, 6, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (32, 5, 11, 7, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (33, 5, 11, 8, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (34, 6, 11, 9, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (35, 6, 11, 10, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (36, 7, 11, 11, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (37, 8, 11, 12, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (38, 7, 11, 13, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (39, 12, 11, 14, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (40, 5, 12, 1, CAST(N'2025-05-20' AS Date), 2, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (41, 12, 12, 3, CAST(N'2025-05-20' AS Date), 2, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (42, 6, 12, 2, CAST(N'2025-05-20' AS Date), 2, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (43, 24, 12, 4, CAST(N'2025-05-20' AS Date), 2, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (44, 11, 12, 6, CAST(N'2025-05-20' AS Date), 5, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (45, 10, 7, 1, CAST(N'2025-05-20' AS Date), 5, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (46, 14, 7, 1, CAST(N'2025-05-22' AS Date), 5, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (47, 9, 12, 7, CAST(N'2025-05-22' AS Date), 5, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (48, 21, 6, 7, CAST(N'2025-05-22' AS Date), 9, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (49, 12, 6, 4, CAST(N'2025-05-23' AS Date), 24, NULL, NULL, 1)
GO
INSERT [dbo].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (50, 13, 6, 4, CAST(N'2025-05-23' AS Date), 24, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[UserProjectRoleAssociation] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (1, N'Admin', CAST(N'2022-10-15' AS Date), 2, CAST(N'2022-11-24' AS Date), 3, 1, 1)
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (2, N'ProgramPE', CAST(N'2022-01-03' AS Date), 1, CAST(N'2022-02-20' AS Date), 1, 1, 4)
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (3, N'ProjectPE', CAST(N'2022-03-26' AS Date), 4, CAST(N'2022-05-13' AS Date), 2, 1, 4)
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (4, N'User', CAST(N'2022-10-13' AS Date), 5, CAST(N'2022-12-17' AS Date), 2, 1, 4)
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (5, N'Guest', CAST(N'2022-09-20' AS Date), 3, CAST(N'2022-09-28' AS Date), 1, 1, 4)
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (6, N'PTL', CAST(N'2022-07-08' AS Date), 2, CAST(N'2022-07-14' AS Date), 1, 1, 4)
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (7, N'PMT', CAST(N'2022-04-26' AS Date), 5, CAST(N'2022-07-31' AS Date), 1, 1, 4)
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (8, N'PPL', CAST(N'2022-11-02' AS Date), 1, CAST(N'2023-01-31' AS Date), 2, 1, 4)
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (9, N'MTL', CAST(N'2022-02-11' AS Date), 1, CAST(N'2022-04-25' AS Date), 5, 1, 4)
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (10, N'MSI', CAST(N'2022-06-16' AS Date), 4, CAST(N'2022-09-12' AS Date), 5, 1, 4)
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (11, N'PM', CAST(N'2022-07-18' AS Date), 1, NULL, NULL, 1, 2)
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (12, N'PL', CAST(N'2022-08-05' AS Date), 1, NULL, NULL, 1, 3)
GO
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[UserTabAssoc] ON 
GO
INSERT [dbo].[UserTabAssoc] ([UTAId], [UserId], [TabId], [GroupId]) VALUES (1, 1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[UserTabAssoc] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserDetails]    Script Date: 23-05-2025 15:29:14 ******/
ALTER TABLE [dbo].[UserDetails] ADD  CONSTRAINT [IX_UserDetails] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserProjectRoleAssociation]  WITH CHECK ADD  CONSTRAINT [FK_UserProjectRoleAssociation_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([ProjectId])
GO
ALTER TABLE [dbo].[UserProjectRoleAssociation] CHECK CONSTRAINT [FK_UserProjectRoleAssociation_Projects]
GO
ALTER TABLE [dbo].[UserProjectRoleAssociation]  WITH CHECK ADD  CONSTRAINT [FK_UserProjectRoleAssociation_UserDetails] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserDetails] ([UserId])
GO
ALTER TABLE [dbo].[UserProjectRoleAssociation] CHECK CONSTRAINT [FK_UserProjectRoleAssociation_UserDetails]
GO
ALTER TABLE [dbo].[UserProjectRoleAssociation]  WITH CHECK ADD  CONSTRAINT [FK_UserProjectRoleAssociation_UserRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[UserRoles] ([RoleId])
GO
ALTER TABLE [dbo].[UserProjectRoleAssociation] CHECK CONSTRAINT [FK_UserProjectRoleAssociation_UserRoles]
GO
