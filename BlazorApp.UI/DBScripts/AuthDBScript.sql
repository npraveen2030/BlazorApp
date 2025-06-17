USE [AuthDB]
GO
/****** Object:  Table [Core].[AdminRoleAssociation]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[AdminRoleAssociation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_AdminRoleAssociation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Core].[Projects]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[Projects](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](100) NOT NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedBy] [int] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK__Tiles__7FCB7AE063243E74] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Core].[UserDetails]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[UserDetails](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ResetToken] [nvarchar](max) NULL,
	[ResetTokenExpiration] [datetime2](7) NULL,
 CONSTRAINT [PK__UserDeta__1788CC4C7AE0F18A] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Core].[UserProjectRoleAssociation]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[UserProjectRoleAssociation](
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
/****** Object:  Table [Core].[UserRoles]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Core].[UserRoles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedBy] [int] NULL,
	[isActive] [bit] NOT NULL,
	[RolePriority] [int] NOT NULL,
 CONSTRAINT [PK__UserRole__8AFACE1A1860925D] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Personalization].[MasterTabAssocaition]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Personalization].[MasterTabAssocaition](
	[MTAId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[TabId] [int] NOT NULL,
	[TabName] [nchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Personalization].[preferencesMasterTable]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Personalization].[preferencesMasterTable](
	[GroupId] [int] NOT NULL,
	[GroupName] [nchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Personalization].[UserGridAssoc]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Personalization].[UserGridAssoc](
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
/****** Object:  Table [Personalization].[UserTabAssoc]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Personalization].[UserTabAssoc](
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
/****** Object:  Table [Pricing].[BonusType]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Pricing].[BonusType](
	[ID] [int] NOT NULL,
	[Name] [varchar](10) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__BonusTyp__3214EC27C8112D90] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Pricing].[Evaluate]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Pricing].[Evaluate](
	[ID] [int] NOT NULL,
	[Name] [varchar](10) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__Evaluate__3214EC27D0121318] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Pricing].[ExceptionInterest]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Pricing].[ExceptionInterest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Modelid] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[AccountNumber] [varchar](50) NULL,
	[AverageCollectedBalance] [decimal](18, 4) NULL,
	[CampaignId] [int] NULL,
	[StandardInterestRate] [decimal](18, 4) NULL,
	[AnnualizedStandardIneterestExpense] [decimal](18, 4) NULL,
	[CurrentInteresetRate] [decimal](18, 4) NULL,
	[AnnualizedCurrentInteresetExpense] [decimal](18, 4) NULL,
	[FloatingId] [int] NULL,
	[FloatingRate] [decimal](18, 4) NULL,
	[BonusId] [int] NULL,
	[AppliedInterestExpires] [datetime] NULL,
	[AppliedInterestRate] [decimal](18, 4) NULL,
	[AnnualizedInterestExpense] [decimal](18, 4) NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_ExceptionInterest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Pricing].[Floating]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Pricing].[Floating](
	[ID] [int] NOT NULL,
	[Name] [varchar](10) NOT NULL,
	[Rate] [float] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__Floating__3214EC27CAC6CDAE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Pricing].[Models]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Pricing].[Models](
	[Modelid] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__Models__E8D4A544A0BEC966] PRIMARY KEY CLUSTERED 
(
	[Modelid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Pricing].[Product]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Pricing].[Product](
	[ID] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[StandardInteresetRate] [decimal](18, 4) NULL,
	[AnnualizedStandardInterestExpense] [decimal](18, 4) NULL,
	[CurrentInterestRate] [decimal](18, 4) NULL,
	[AnnualizedCurrentInterestExpense] [nchar](10) NULL,
	[AppliedInterestRate] [decimal](18, 4) NULL,
	[AnnualizedInterestExpense] [decimal](18, 4) NULL,
 CONSTRAINT [PK__Product__3214EC27AABDA778] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Pricing].[Tabs]    Script Date: 17-06-2025 17:37:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Pricing].[Tabs](
	[TabId] [int] NOT NULL,
	[TabName] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK__Tabs__80E37C1812CB7725] PRIMARY KEY CLUSTERED 
(
	[TabId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Core].[Projects] ON 
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (1, N'Assets', CAST(N'2024-04-22' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (2, N'CLEAR', CAST(N'2024-04-10' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (3, N'CSC', CAST(N'2024-10-05' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (4, N'Dashboards', CAST(N'2024-10-16' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (5, N'Data Dictionary', CAST(N'2024-10-08' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (6, N'Devices', CAST(N'2024-03-04' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (7, N'ETD', CAST(N'2024-11-12' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (8, N'LATS', CAST(N'2024-06-05' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (9, N'LRC', CAST(N'2024-12-15' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (10, N'PMT', CAST(N'2024-01-06' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (11, N'Projects', CAST(N'2024-01-10' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (12, N'Push', CAST(N'2024-11-14' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (13, N'Reports', CAST(N'2024-05-22' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (14, N'Storage', CAST(N'2024-12-09' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (15, N'Surveys', CAST(N'2024-04-19' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (16, N'VOEE', CAST(N'2024-04-14' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (17, N'Work Orders', CAST(N'2024-09-18' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (18, N'Workflow', CAST(N'2024-12-04' AS Date), 4, NULL, NULL, 1)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (19, N'AdminProject', CAST(N'2024-12-05' AS Date), 4, NULL, NULL, 0)
GO
INSERT [Core].[Projects] ([ProjectId], [ProjectName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive]) VALUES (20, N'Pricing', CAST(N'2025-06-17' AS Date), 1, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [Core].[Projects] OFF
GO
SET IDENTITY_INSERT [Core].[UserDetails] ON 
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (1, N'user1', N'WAD5pBWiLlLM1rhPYQEbvTWyZ5bi0bJ4u0QMDE3GDZxiy0L5TEV+c9FxRZHdVb5C', CAST(N'2025-05-08' AS Date), NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (2, N'user2', N'jLBeQzIu3osJBIOXxAFulkcD7HsbV962pjytAH90GKM7DbZemAAcux4r/kwU60AR', CAST(N'2025-05-08' AS Date), NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (3, N'user3', N'6iMMzR9Qoqmk/LLWjbFlyOCzxEmfMndib37mfgqaNCfh008gg4ZEiozZeWJEz7Jl', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (4, N'user4', N'Hi/kS840GCTpDghINdczC3AxKBsgKeX/1W0MokDuqeWnfBf9x77URSMRhZGJysYB', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (5, N'user5', N'xG9ftrM20p91HRGYKjTq2xDw+1MHeYSQ83t9pcWZQRiZvJg0sOuIrG5MSUrnu9WL', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (6, N'user6', N'QDOAJLWApgRi6Hjh+kWpbxJLAZjPayWer3RxTX7BJIpru5+xQN+xm6LGslrocyji', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (7, N'user7', N'oPqhVoMpTupTxYaVxaqQclT/ybN4xSkP5o+grRkH+meMctxd+VPvbW2ouQ/VtPwI', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (8, N'user8', N'DpCSC6sm8GnvL2LabW2VoIxK67mF2yQ6+gUeuLGFz5IEUC1L15vD4z68u3poSRKz', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (9, N'user9', N'hP4wFl0dwg66wJz80I2OivRiLtWugeo7cLBYJFqlKugBpXsbfcPkk6i8lJz+LWB/', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (10, N'user10', N'7RxKZBa/bYebDcO3ysgNU+kWMexzP/DsP1v0BqiqOAMFCQlCzD4Pzb0z0+4mJrHr', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (11, N'user11', N'6L0aVpvJEMM+t3l5mK9ZM+e2rY7rcunaxjPL47eT6EU3tKJ3Lpr3TdH1WmhEPhrL', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (12, N'user12', N'5G2cKi/BwDa5E5y9Z0UK9itnBV9/ImuQHykW3etWyiZFB5JO8j+wnJNXLUXAIUt1', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (13, N'user13', N'xS1XHvdwm6qE7dz1NBPneNtuP0i/TXcTkP4C8A/s/LpP0mYj/xVjraiamoHQgAj3', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (14, N'user14', N'OBXxQLA3AzR7Epl8AOYmff5DvFIn31v2c7kdiH9QIXjGQOnGBuQA9mvHmWwujCET', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (15, N'user15', N'k1XWtxKW/6FYuQO387PA8RP1/qFQZSNqfx8ZDSikwrFuBg4FWa/cPaYRRlCyWLHb', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (16, N'user16', N'BlS39MBZKnVaQ0hl0GXPQbOHbhkzN2t10ZyBY/6kdEPnhsPRY3yRWKu1zOmgrqDm', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (17, N'user17', N'lo7CWIVrAuJ2rF7gUZChl/+oKTGBDUPk3eSncyJUqu+kVfOeE+Qkqjfh9SF6e+Sa', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (18, N'user18', N'ojkFPLpxhr/BVQTT98pCLLCgoTFHdULHZOjWIRWDFk3mB/2cf9gyOJf1v6AkEqFR', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (19, N'user19', N'5AdbN5i/h6Qm/OvGOo0P6MWbrx/PCQ2T2SJEsK3KHYjVQUoflEQ/KvgfNTSEJD9W', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (20, N'user20', N'gIypetAKtCgnirSkM8XwKs32Hmi3uhtJ9SJgZ6qNrj7bohoh/Wq90725ya9+y0JB', CAST(N'2025-05-10' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (21, N'user1997', N'fkYCubKqJr95EqneYe+YRzImMAYrbrOKfs5Edv8OeZdk533+nDXLVnjtelAck9eG', CAST(N'2025-06-17' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
INSERT [Core].[UserDetails] ([UserId], [UserName], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [ResetToken], [ResetTokenExpiration]) VALUES (22, N'PricingPM', N'OOYhFtLwLN9h+tgzx2NwR/jZIoUId0ojPWNqHMV6j05G8+x0I/htytDJitq3bz/p', CAST(N'2025-06-17' AS Date), 1, NULL, NULL, 1, NULL, NULL)
GO
SET IDENTITY_INSERT [Core].[UserDetails] OFF
GO
SET IDENTITY_INSERT [Core].[UserProjectRoleAssociation] ON 
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (21, 1, 1, 19, CAST(N'2024-05-06' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (22, 2, 11, 1, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (23, 2, 11, 2, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (24, 2, 11, 3, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (25, 2, 11, 4, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (26, 5, 11, 5, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (27, 5, 11, 6, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (28, 5, 11, 7, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (29, 5, 11, 8, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (30, 6, 11, 9, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (31, 6, 11, 10, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (32, 7, 11, 11, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (33, 8, 11, 12, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (34, 7, 11, 13, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (35, 12, 11, 14, CAST(N'2025-05-20' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (36, 5, 12, 1, CAST(N'2025-05-20' AS Date), 2, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (37, 12, 12, 3, CAST(N'2025-05-20' AS Date), 2, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (38, 6, 12, 2, CAST(N'2025-05-20' AS Date), 2, NULL, NULL, 0)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (39, 11, 12, 6, CAST(N'2025-05-20' AS Date), 5, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (40, 10, 7, 1, CAST(N'2025-05-20' AS Date), 5, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (41, 14, 7, 1, CAST(N'2025-05-22' AS Date), 5, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (42, 9, 12, 7, CAST(N'2025-05-22' AS Date), 5, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (43, 12, 6, 4, CAST(N'2025-05-23' AS Date), 24, NULL, NULL, 0)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (44, 13, 6, 4, CAST(N'2025-05-23' AS Date), 24, NULL, NULL, 0)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (45, 22, 11, 20, CAST(N'2025-06-17' AS Date), 1, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (46, 14, 12, 2, CAST(N'2025-06-17' AS Date), 2, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (47, 10, 6, 2, CAST(N'2025-06-17' AS Date), 14, NULL, NULL, 1)
GO
INSERT [Core].[UserProjectRoleAssociation] ([upraId], [UserId], [RoleId], [ProjectId], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (48, 9, 4, 2, CAST(N'2025-06-17' AS Date), 14, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [Core].[UserProjectRoleAssociation] OFF
GO
SET IDENTITY_INSERT [Core].[UserRoles] ON 
GO
INSERT [Core].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (1, N'Admin', CAST(N'2022-10-15' AS Date), 2, CAST(N'2022-11-24' AS Date), 3, 1, 1)
GO
INSERT [Core].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (2, N'ProgramPE', CAST(N'2022-01-03' AS Date), 1, CAST(N'2022-02-20' AS Date), 1, 1, 4)
GO
INSERT [Core].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (3, N'ProjectPE', CAST(N'2022-03-26' AS Date), 4, CAST(N'2022-05-13' AS Date), 2, 1, 4)
GO
INSERT [Core].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (4, N'User', CAST(N'2022-10-13' AS Date), 5, CAST(N'2022-12-17' AS Date), 2, 1, 4)
GO
INSERT [Core].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (5, N'Guest', CAST(N'2022-09-20' AS Date), 3, CAST(N'2022-09-28' AS Date), 1, 1, 4)
GO
INSERT [Core].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (6, N'PTL', CAST(N'2022-07-08' AS Date), 2, CAST(N'2022-07-14' AS Date), 1, 1, 4)
GO
INSERT [Core].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (7, N'PMT', CAST(N'2022-04-26' AS Date), 5, CAST(N'2022-07-31' AS Date), 1, 1, 4)
GO
INSERT [Core].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (8, N'PPL', CAST(N'2022-11-02' AS Date), 1, CAST(N'2023-01-31' AS Date), 2, 1, 4)
GO
INSERT [Core].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (9, N'MTL', CAST(N'2022-02-11' AS Date), 1, CAST(N'2022-04-25' AS Date), 5, 1, 4)
GO
INSERT [Core].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (10, N'MSI', CAST(N'2022-06-16' AS Date), 4, CAST(N'2022-09-12' AS Date), 5, 1, 4)
GO
INSERT [Core].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (11, N'PM', CAST(N'2022-07-18' AS Date), 1, NULL, NULL, 1, 2)
GO
INSERT [Core].[UserRoles] ([RoleId], [RoleName], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [isActive], [RolePriority]) VALUES (12, N'PL', CAST(N'2022-08-05' AS Date), 1, NULL, NULL, 1, 3)
GO
SET IDENTITY_INSERT [Core].[UserRoles] OFF
GO
INSERT [Personalization].[MasterTabAssocaition] ([MTAId], [GroupId], [TabId], [TabName]) VALUES (1, 1, 1, N'UserManager                                       ')
GO
INSERT [Personalization].[MasterTabAssocaition] ([MTAId], [GroupId], [TabId], [TabName]) VALUES (2, 1, 2, N'Project Manager                                   ')
GO
INSERT [Personalization].[MasterTabAssocaition] ([MTAId], [GroupId], [TabId], [TabName]) VALUES (3, 1, 3, N'User Role Association                             ')
GO
INSERT [Personalization].[MasterTabAssocaition] ([MTAId], [GroupId], [TabId], [TabName]) VALUES (4, 2, 1, N'Project Manager Portal                            ')
GO
INSERT [Personalization].[MasterTabAssocaition] ([MTAId], [GroupId], [TabId], [TabName]) VALUES (5, 2, 2, N'Project Lead Portal                               ')
GO
INSERT [Personalization].[preferencesMasterTable] ([GroupId], [GroupName]) VALUES (1, N'AdminPortal                                       ')
GO
INSERT [Personalization].[preferencesMasterTable] ([GroupId], [GroupName]) VALUES (2, N'Associations Portal                               ')
GO
INSERT [Personalization].[preferencesMasterTable] ([GroupId], [GroupName]) VALUES (3, N'Login                                             ')
GO
SET IDENTITY_INSERT [Personalization].[UserGridAssoc] ON 
GO
INSERT [Personalization].[UserGridAssoc] ([UGAId], [UserId], [GroupId], [TabId], [Preferences]) VALUES (1, 5, 2, 2, N'{"UserName":{"Width":"734.6625366210938px","OrderIndex":0,"Visible":true},"RoleName":{"Width":"120px","OrderIndex":1,"Visible":true},"CreatedDate":{"Width":"120px","OrderIndex":2,"Visible":true},"CreatedBy":{"Width":"120px","OrderIndex":3,"Visible":true},"ModifiedDate":{"Width":"120px","OrderIndex":4,"Visible":true},"ModifiedBy":{"Width":"120px","OrderIndex":5,"Visible":true},"IsActive":{"Width":"120px","OrderIndex":6,"Visible":true}}')
GO
INSERT [Personalization].[UserGridAssoc] ([UGAId], [UserId], [GroupId], [TabId], [Preferences]) VALUES (2, 24, 2, 2, N'{"UserName":{"Width":"120px","OrderIndex":0,"Visible":true},"RoleName":{"Width":"120px","OrderIndex":1,"Visible":false},"CreatedDate":{"Width":"120px","OrderIndex":2,"Visible":true},"CreatedBy":{"Width":"120px","OrderIndex":3,"Visible":true},"ModifiedDate":{"Width":"120px","OrderIndex":4,"Visible":true},"ModifiedBy":{"Width":"120px","OrderIndex":5,"Visible":true},"IsActive":{"Width":"120px","OrderIndex":6,"Visible":true}}')
GO
SET IDENTITY_INSERT [Personalization].[UserGridAssoc] OFF
GO
SET IDENTITY_INSERT [Personalization].[UserTabAssoc] ON 
GO
INSERT [Personalization].[UserTabAssoc] ([UTAId], [UserId], [TabId], [GroupId]) VALUES (1, 1, 1, 1)
GO
SET IDENTITY_INSERT [Personalization].[UserTabAssoc] OFF
GO
INSERT [Pricing].[BonusType] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Rate', CAST(N'2025-05-24T23:00:53.790' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [Pricing].[BonusType] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Points', CAST(N'2025-05-24T23:00:53.790' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [Pricing].[Evaluate] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Yes', CAST(N'2025-05-24T23:00:45.500' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [Pricing].[Evaluate] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'No', CAST(N'2025-05-24T23:00:45.500' AS DateTime), NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [Pricing].[ExceptionInterest] ON 
GO
INSERT [Pricing].[ExceptionInterest] ([Id], [Modelid], [ProductId], [AccountNumber], [AverageCollectedBalance], [CampaignId], [StandardInterestRate], [AnnualizedStandardIneterestExpense], [CurrentInteresetRate], [AnnualizedCurrentInteresetExpense], [FloatingId], [FloatingRate], [BonusId], [AppliedInterestExpires], [AppliedInterestRate], [AnnualizedInterestExpense], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 1, 23, NULL, NULL, NULL, CAST(0.1800 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1800 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), NULL, NULL, NULL, NULL, CAST(1.1000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(N'2025-06-17' AS Date), NULL, NULL, NULL, 1)
GO
INSERT [Pricing].[ExceptionInterest] ([Id], [Modelid], [ProductId], [AccountNumber], [AverageCollectedBalance], [CampaignId], [StandardInterestRate], [AnnualizedStandardIneterestExpense], [CurrentInteresetRate], [AnnualizedCurrentInteresetExpense], [FloatingId], [FloatingRate], [BonusId], [AppliedInterestExpires], [AppliedInterestRate], [AnnualizedInterestExpense], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 1, 25, NULL, NULL, NULL, CAST(0.1500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), NULL, NULL, NULL, NULL, CAST(1.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(N'2025-06-17' AS Date), NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [Pricing].[ExceptionInterest] OFF
GO
INSERT [Pricing].[Floating] ([ID], [Name], [Rate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Yes', 4.2, CAST(N'2025-05-24T23:01:05.887' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [Pricing].[Floating] ([ID], [Name], [Rate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'No', 2.5, CAST(N'2025-05-24T23:01:05.887' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [Pricing].[Models] ([Modelid], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Base Pricing Model', CAST(N'2025-05-24T23:05:52.207' AS DateTime), N'admin', NULL, NULL, 1)
GO
INSERT [Pricing].[Models] ([Modelid], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Risk-Based Model', CAST(N'2025-05-24T23:05:52.207' AS DateTime), N'admin', NULL, NULL, 1)
GO
INSERT [Pricing].[Models] ([Modelid], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Tiered Interest Model', CAST(N'2025-05-24T23:05:52.207' AS DateTime), N'admin', NULL, NULL, 1)
GO
INSERT [Pricing].[Models] ([Modelid], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'Promotional Model', CAST(N'2025-05-24T23:05:52.207' AS DateTime), N'admin', NULL, NULL, 1)
GO
INSERT [Pricing].[Models] ([Modelid], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'Dynamic Rate Model', CAST(N'2025-05-24T23:05:52.207' AS DateTime), N'admin', NULL, NULL, 1)
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (12, N'Fed Funds Sweep', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.2750 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.2750 AS Decimal(18, 4)), N'0.0000    ', CAST(1.4500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (19, N'Correspondent Investment Sweep', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.2500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.2500 AS Decimal(18, 4)), N'0.0000    ', CAST(1.4000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (21, N'Public Fund NOW', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.2200 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.2200 AS Decimal(18, 4)), N'0.0000    ', CAST(1.2500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (22, N'Non-Profit NOW - Wealth Mgmt', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.2000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.2000 AS Decimal(18, 4)), N'0.0000    ', CAST(1.2000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (23, N'NON-Profit NOW', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.1800 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1800 AS Decimal(18, 4)), N'0.0000    ', CAST(1.1000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (24, N'NOW - Wealth Mgmt', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.1700 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1700 AS Decimal(18, 4)), N'0.0000    ', CAST(1.0500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (25, N'NOW', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.1500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1500 AS Decimal(18, 4)), N'0.0000    ', CAST(1.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (26, N'Analysis', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), N'0.0000    ', CAST(0.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (27, N'Commercial MMA', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.1500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1500 AS Decimal(18, 4)), N'0.0000    ', CAST(0.8000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (28, N'Commercial MMA - Wealth Mgmt', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.1500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1500 AS Decimal(18, 4)), N'0.0000    ', CAST(0.8500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (29, N'Business MMA', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.1000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1000 AS Decimal(18, 4)), N'0.0000    ', CAST(0.7500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (30, N'Business MMA - Wealth Mgmt', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.1000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1000 AS Decimal(18, 4)), N'0.0000    ', CAST(0.7500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (31, N'Public Fund MMA', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.0500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0500 AS Decimal(18, 4)), N'0.0000    ', CAST(0.7000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (32, N'Public Fund MMA - Wealth Mgmt', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.0500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0500 AS Decimal(18, 4)), N'0.0000    ', CAST(0.7000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (33, N'Non-Profit MMA', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.0300 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0300 AS Decimal(18, 4)), N'0.0000    ', CAST(0.6500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (34, N'Non-Profit MMA - Wealth Mgmt', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.0300 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0300 AS Decimal(18, 4)), N'0.0000    ', CAST(0.6500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (35, N'Business Savings', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.0200 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0200 AS Decimal(18, 4)), N'0.0000    ', CAST(0.5500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (36, N'Public Fund Savings', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.0200 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0200 AS Decimal(18, 4)), N'0.0000    ', CAST(0.5500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (37, N'Public Fund Savings - Wealth Mgmt', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.0200 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0200 AS Decimal(18, 4)), N'0.0000    ', CAST(0.5500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (38, N'Non-Profit Savings', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.0100 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0100 AS Decimal(18, 4)), N'0.0000    ', CAST(0.5000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (39, N'Non-Profit Savings - Wealth Mgmt', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.0100 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.0100 AS Decimal(18, 4)), N'0.0000    ', CAST(0.5000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Tabs] ([TabId], [TabName], [IsActive]) VALUES (1, N'Models', 1)
GO
INSERT [Pricing].[Tabs] ([TabId], [TabName], [IsActive]) VALUES (2, N'Demographic Data', 1)
GO
INSERT [Pricing].[Tabs] ([TabId], [TabName], [IsActive]) VALUES (3, N'Exception Interest', 1)
GO
INSERT [Pricing].[Tabs] ([TabId], [TabName], [IsActive]) VALUES (4, N'Pricing Analysis', 1)
GO
ALTER TABLE [Pricing].[BonusType] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Pricing].[BonusType] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
ALTER TABLE [Pricing].[Evaluate] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Pricing].[Evaluate] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
ALTER TABLE [Pricing].[Floating] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Pricing].[Floating] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
ALTER TABLE [Pricing].[Models] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Pricing].[Models] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
ALTER TABLE [Core].[AdminRoleAssociation]  WITH CHECK ADD  CONSTRAINT [FK_AdminRoleAssociation_UserDetails] FOREIGN KEY([UserId])
REFERENCES [Core].[UserDetails] ([UserId])
GO
ALTER TABLE [Core].[AdminRoleAssociation] CHECK CONSTRAINT [FK_AdminRoleAssociation_UserDetails]
GO
ALTER TABLE [Core].[AdminRoleAssociation]  WITH CHECK ADD  CONSTRAINT [FK_AdminRoleAssociation_UserRoles] FOREIGN KEY([RoleId])
REFERENCES [Core].[UserRoles] ([RoleId])
GO
ALTER TABLE [Core].[AdminRoleAssociation] CHECK CONSTRAINT [FK_AdminRoleAssociation_UserRoles]
GO
ALTER TABLE [Core].[UserProjectRoleAssociation]  WITH CHECK ADD  CONSTRAINT [FK_UserProjectRoleAssociation_Projects] FOREIGN KEY([ProjectId])
REFERENCES [Core].[Projects] ([ProjectId])
GO
ALTER TABLE [Core].[UserProjectRoleAssociation] CHECK CONSTRAINT [FK_UserProjectRoleAssociation_Projects]
GO
ALTER TABLE [Core].[UserProjectRoleAssociation]  WITH CHECK ADD  CONSTRAINT [FK_UserProjectRoleAssociation_UserDetails] FOREIGN KEY([UserId])
REFERENCES [Core].[UserDetails] ([UserId])
GO
ALTER TABLE [Core].[UserProjectRoleAssociation] CHECK CONSTRAINT [FK_UserProjectRoleAssociation_UserDetails]
GO
ALTER TABLE [Core].[UserProjectRoleAssociation]  WITH CHECK ADD  CONSTRAINT [FK_UserProjectRoleAssociation_UserRoles] FOREIGN KEY([RoleId])
REFERENCES [Core].[UserRoles] ([RoleId])
GO
ALTER TABLE [Core].[UserProjectRoleAssociation] CHECK CONSTRAINT [FK_UserProjectRoleAssociation_UserRoles]
GO
ALTER TABLE [Pricing].[ExceptionInterest]  WITH CHECK ADD  CONSTRAINT [FK_ExceptionInterest_Models] FOREIGN KEY([Modelid])
REFERENCES [Pricing].[Models] ([Modelid])
GO
ALTER TABLE [Pricing].[ExceptionInterest] CHECK CONSTRAINT [FK_ExceptionInterest_Models]
GO
ALTER TABLE [Pricing].[ExceptionInterest]  WITH CHECK ADD  CONSTRAINT [FK_ExceptionInterest_Product] FOREIGN KEY([ProductId])
REFERENCES [Pricing].[Product] ([ID])
GO
ALTER TABLE [Pricing].[ExceptionInterest] CHECK CONSTRAINT [FK_ExceptionInterest_Product]
GO
