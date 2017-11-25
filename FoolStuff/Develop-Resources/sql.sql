USE [FoolStaffDB]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 25/11/2017 12:08:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataCreazione] [datetime] NOT NULL,
	[DataOperazione] [datetime] NULL,
	[DataChiusura] [datetime] NULL,
	[Stato] [nvarchar](15) NOT NULL,
	[Titolo] [nvarchar](100) NOT NULL,
	[Descrizione] [nvarchar](max) NOT NULL,
	[Priorita] [smallint] NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tesoreria]    Script Date: 25/11/2017 12:08:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tesoreria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataOperazione] [datetime] NOT NULL,
	[Operazione] [nvarchar](20) NOT NULL,
	[Totale] [decimal](18, 0) NOT NULL,
	[Note] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Tesoreria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_Tesoreria]    Script Date: 25/11/2017 12:08:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Tesoreria](
	[Versamento] [decimal](18, 0) NOT NULL,
	[TesoreriaId] [int] NOT NULL,
	[UserInfoId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_User_Tesoreria] PRIMARY KEY CLUSTERED 
(
	[TesoreriaId] ASC,
	[UserInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 25/11/2017 12:08:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[Id] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInfoTask]    Script Date: 25/11/2017 12:08:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfoTask](
	[UserInfoId] [nvarchar](128) NOT NULL,
	[TaskId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserInfoId] ASC,
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[User_Tesoreria]  WITH CHECK ADD  CONSTRAINT [FK_TesoreriaToUser_Tesoreria] FOREIGN KEY([TesoreriaId])
REFERENCES [dbo].[Tesoreria] ([Id])
GO
ALTER TABLE [dbo].[User_Tesoreria] CHECK CONSTRAINT [FK_TesoreriaToUser_Tesoreria]
GO
ALTER TABLE [dbo].[User_Tesoreria]  WITH CHECK ADD  CONSTRAINT [FK_UserInfoToUser_Tesoreria] FOREIGN KEY([UserInfoId])
REFERENCES [dbo].[UserInfo] ([Id])
GO
ALTER TABLE [dbo].[User_Tesoreria] CHECK CONSTRAINT [FK_UserInfoToUser_Tesoreria]
GO
ALTER TABLE [dbo].[UserInfoTask]  WITH CHECK ADD FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([Id])
GO
ALTER TABLE [dbo].[UserInfoTask]  WITH CHECK ADD FOREIGN KEY([UserInfoId])
REFERENCES [dbo].[UserInfo] ([Id])
GO
