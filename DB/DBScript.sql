USE [EmployeeDB]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 16-08-2018 21:05:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](25) NOT NULL,
	[LastName] [nvarchar](25) NOT NULL,
	[Designation] [nvarchar](50) NULL,
	[Age] [tinyint] NOT NULL,
	[Salary] [money] NOT NULL CONSTRAINT [DF_Employee_Salary]  DEFAULT ((0)),
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [Designation], [Age], [Salary]) VALUES (1, N'Yogesh', N'Mahajan', N'TL', 35, 10000.0000)
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [Designation], [Age], [Salary]) VALUES (2, N'Aaryan', N'Mahajan', N'SE', 35, 2000.0000)
SET IDENTITY_INSERT [dbo].[Employee] OFF
