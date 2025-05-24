/****** Object:  Table [Pricing].[BonusType]    Script Date: 24-05-2025 23:59:30 ******/
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
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Pricing].[Evaluate]    Script Date: 24-05-2025 23:59:30 ******/
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
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Pricing].[ExceptionInterest]    Script Date: 24-05-2025 23:59:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Pricing].[ExceptionInterest](
	[Id] [int] NULL,
	[Modelid] [int] NULL,
	[ProductId] [int] NULL,
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
	[IsActive] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Pricing].[Floating]    Script Date: 24-05-2025 23:59:30 ******/
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
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Pricing].[Models]    Script Date: 24-05-2025 23:59:30 ******/
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
PRIMARY KEY CLUSTERED 
(
	[Modelid] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Pricing].[Product]    Script Date: 24-05-2025 23:59:30 ******/
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
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [Pricing].[BonusType] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Rate', CAST(N'2025-05-24T23:00:53.790' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [Pricing].[BonusType] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Points', CAST(N'2025-05-24T23:00:53.790' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [Pricing].[Evaluate] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Yes', CAST(N'2025-05-24T23:00:45.500' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [Pricing].[Evaluate] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'No', CAST(N'2025-05-24T23:00:45.500' AS DateTime), NULL, NULL, NULL, 1)
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
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (12, N'Fed Funds Sweep', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.2750 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.2750 AS Decimal(18, 4)), N'0.00      ', CAST(1.4500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (19, N'Correspondent Investment Sweep', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.2500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.2500 AS Decimal(18, 4)), N'0.00      ', CAST(1.4000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (21, N'Public Fund NOW', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.2200 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.2200 AS Decimal(18, 4)), N'0.00      ', CAST(1.2500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (22, N'Non-Profit NOW - Wealth Mgmt', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.2000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.2000 AS Decimal(18, 4)), N'0.00      ', CAST(1.2000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (23, N'NON-Profit NOW', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.1800 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1800 AS Decimal(18, 4)), N'0.00      ', CAST(1.1000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (24, N'Insured Cash Sweep', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.3200 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.3200 AS Decimal(18, 4)), N'0.00      ', CAST(1.6000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (32, N'Certificate of Deposit - 6 Months', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.3500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.3500 AS Decimal(18, 4)), N'0.00      ', CAST(1.8000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (33, N'Certificate of Deposit - 3 Months', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.3000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.3000 AS Decimal(18, 4)), N'0.00      ', CAST(1.5000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (34, N'Liquidity Sweep', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.3100 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.3100 AS Decimal(18, 4)), N'0.00      ', CAST(1.5500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (35, N'Certificate of Deposit - 12 Months', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.4500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.4500 AS Decimal(18, 4)), N'0.00      ', CAST(2.1000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (36, N'Certificate of Deposit - 24 Months', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.5000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.5000 AS Decimal(18, 4)), N'0.00      ', CAST(2.2000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (37, N'Certificate of Deposit - 36 Months', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.5250 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.5250 AS Decimal(18, 4)), N'0.00      ', CAST(2.3000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (38, N'Certificate of Deposit - 48 Months', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.5500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.5500 AS Decimal(18, 4)), N'0.00      ', CAST(2.4000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (40, N'CDARS', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.2000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.2000 AS Decimal(18, 4)), N'0.00      ', CAST(1.2000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (45, N'Business Interest Checking', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.3500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.3500 AS Decimal(18, 4)), N'0.00      ', CAST(1.5000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (46, N'Business Investment Account', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.3750 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.3750 AS Decimal(18, 4)), N'0.00      ', CAST(1.6000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (48, N'Public Fund NOW RFP', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.2300 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.2300 AS Decimal(18, 4)), N'0.00      ', CAST(1.3000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (49, N'Public Fund Savings', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.2400 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.2400 AS Decimal(18, 4)), N'0.00      ', CAST(1.3500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (52, N'Escrow Interest Checking', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.2250 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.2250 AS Decimal(18, 4)), N'0.00      ', CAST(1.3000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (53, N'Business Savings', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.3500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.3500 AS Decimal(18, 4)), N'0.00      ', CAST(2.0000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (54, N'Business Money Market', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.4000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.4000 AS Decimal(18, 4)), N'0.00      ', CAST(1.7000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (55, N'Dedicated Money Market - Business', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.3750 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.3750 AS Decimal(18, 4)), N'0.00      ', CAST(1.7500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (62, N'Standard Capital Bank Earnings Credit', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.1000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1000 AS Decimal(18, 4)), N'0.00      ', CAST(0.7500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (63, N'Standard Correspondent Earnings Credit', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.1150 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1150 AS Decimal(18, 4)), N'0.00      ', CAST(0.7750 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (64, N'Standard Commercial Earnings Credit', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.1250 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1250 AS Decimal(18, 4)), N'0.00      ', CAST(0.8000 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
INSERT [Pricing].[Product] ([ID], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive], [StandardInteresetRate], [AnnualizedStandardInterestExpense], [CurrentInterestRate], [AnnualizedCurrentInterestExpense], [AppliedInterestRate], [AnnualizedInterestExpense]) VALUES (65, N'Standard Warehouse Lending Earnings Credit', CAST(N'2025-05-24T23:34:57.893' AS DateTime), N'system', NULL, NULL, 1, CAST(0.1300 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)), CAST(0.1300 AS Decimal(18, 4)), N'0.00      ', CAST(0.8500 AS Decimal(18, 4)), CAST(0.0000 AS Decimal(18, 4)))
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Floating__737584F6010FAD5C]    Script Date: 24-05-2025 23:59:30 ******/
ALTER TABLE [Pricing].[Floating] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [Pricing].[BonusType] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Pricing].[BonusType] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [Pricing].[Evaluate] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Pricing].[Evaluate] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [Pricing].[Floating] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Pricing].[Floating] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [Pricing].[Models] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [Pricing].[Models] ADD  DEFAULT ((1)) FOR [IsActive]
GO
