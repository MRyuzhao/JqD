USE [JqD]
GO
/****** Object:  Table [dbo].[SystemUser]    Script Date: 02/28/2018 16:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[LastLoginDate] [datetime] NULL,
	[CreateUser] [nvarchar](20) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditUser] [nvarchar](20) NULL,
	[EditDate] [datetime] NULL,
	[IsLogin] [int] NULL,
 CONSTRAINT [PK_SystemUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[LoginName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperationLogs]    Script Date: 02/28/2018 16:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Operation] [nvarchar](50) NOT NULL,
	[OperatorId] [nvarchar](50) NOT NULL,
	[ModelType] [nvarchar](200) NOT NULL,
	[ModelId] [int] NOT NULL,
	[ModelJson] [nvarchar](max) NOT NULL,
	[OperationTime] [datetime] NOT NULL,
	[OperationSQL] [nvarchar](max) NULL,
 CONSTRAINT [PK_OperationLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogArticle]    Script Date: 02/28/2018 16:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogArticle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Category] [int] NOT NULL,
	[Content] [text] NOT NULL,
	[Remark] [nvarchar](max) NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[EditUser] [nvarchar](50) NULL,
	[EditDate] [datetime] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.BlogArticle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
