USE [ProjectB]
GO
/****** Object:  Table [dbo].[Assessment]    Script Date: 03/05/2019 11:00:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assessment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[TotalMarks] [int] NOT NULL,
	[TotalWeightage] [int] NOT NULL,
 CONSTRAINT [PK_Assessment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AssessmentComponent]    Script Date: 03/05/2019 11:00:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssessmentComponent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RubricId] [int] NOT NULL,
	[TotalMarks] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
	[AssessmentId] [int] NOT NULL,
 CONSTRAINT [PK_AssessmentRubric] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ClassAttendance]    Script Date: 03/05/2019 11:00:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassAttendance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AttendanceDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ClassAttendance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Clo]    Script Date: 03/05/2019 11:00:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_Clo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Lookup]    Script Date: 03/05/2019 11:00:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lookup](
	[LookupId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED 
(
	[LookupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rubric]    Script Date: 03/05/2019 11:00:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rubric](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Details] [nvarchar](max) NOT NULL,
	[CloId] [int] NOT NULL,
 CONSTRAINT [PK_Rubric] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RubricLevel]    Script Date: 03/05/2019 11:00:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RubricLevel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RubricId] [int] NOT NULL,
	[Details] [nvarchar](max) NOT NULL,
	[MeasurementLevel] [int] NOT NULL,
 CONSTRAINT [PK_RubricLevel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student]    Script Date: 03/05/2019 11:00:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[Contact] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[RegistrationNumber] [nvarchar](20) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StudentAttendance]    Script Date: 03/05/2019 11:00:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentAttendance](
	[AttendanceId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[AttendanceStatus] [int] NOT NULL,
 CONSTRAINT [PK_StudentAttendance] PRIMARY KEY CLUSTERED 
(
	[AttendanceId] ASC,
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StudentResult]    Script Date: 03/05/2019 11:00:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentResult](
	[StudentId] [int] NOT NULL,
	[AssessmentComponentId] [int] NOT NULL,
	[RubricMeasurementId] [int] NOT NULL,
	[EvaluationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StudentResult] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[AssessmentComponentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Assessment] ON 

INSERT [dbo].[Assessment] ([Id], [Title], [DateCreated], [TotalMarks], [TotalWeightage]) VALUES (1, N'lab1', CAST(0x0000AA2000000000 AS DateTime), 100, 10)
INSERT [dbo].[Assessment] ([Id], [Title], [DateCreated], [TotalMarks], [TotalWeightage]) VALUES (4, N'ass', CAST(0x0000AA2000000000 AS DateTime), 100, 10)
INSERT [dbo].[Assessment] ([Id], [Title], [DateCreated], [TotalMarks], [TotalWeightage]) VALUES (5, N'lab2', CAST(0x0000AA2100000000 AS DateTime), 20, 10)
INSERT [dbo].[Assessment] ([Id], [Title], [DateCreated], [TotalMarks], [TotalWeightage]) VALUES (7, N'lab3', CAST(0x0000A5BE00000000 AS DateTime), 25, 10)
INSERT [dbo].[Assessment] ([Id], [Title], [DateCreated], [TotalMarks], [TotalWeightage]) VALUES (8, N'lab3', CAST(0x0000A5BE00000000 AS DateTime), 250, 10)
INSERT [dbo].[Assessment] ([Id], [Title], [DateCreated], [TotalMarks], [TotalWeightage]) VALUES (10, N'assignment5', CAST(0x0000AA3A00000000 AS DateTime), 10, 10)
SET IDENTITY_INSERT [dbo].[Assessment] OFF
SET IDENTITY_INSERT [dbo].[AssessmentComponent] ON 

INSERT [dbo].[AssessmentComponent] ([Id], [Name], [RubricId], [TotalMarks], [DateCreated], [DateUpdated], [AssessmentId]) VALUES (1, N'Question1', 1, 3, CAST(0x0000AA2000000000 AS DateTime), CAST(0x0000AA2000000000 AS DateTime), 1)
INSERT [dbo].[AssessmentComponent] ([Id], [Name], [RubricId], [TotalMarks], [DateCreated], [DateUpdated], [AssessmentId]) VALUES (2, N'Question 2', 2, 4, CAST(0x0000AA2000000000 AS DateTime), CAST(0x0000AA2000000000 AS DateTime), 1)
INSERT [dbo].[AssessmentComponent] ([Id], [Name], [RubricId], [TotalMarks], [DateCreated], [DateUpdated], [AssessmentId]) VALUES (3, N'Question 3', 3, 3, CAST(0x0000AA2000000000 AS DateTime), CAST(0x0000AA2000000000 AS DateTime), 1)
INSERT [dbo].[AssessmentComponent] ([Id], [Name], [RubricId], [TotalMarks], [DateCreated], [DateUpdated], [AssessmentId]) VALUES (13, N'comp', 2, 20, CAST(0x0000AA2100000000 AS DateTime), CAST(0x0000AA2100000000 AS DateTime), 4)
INSERT [dbo].[AssessmentComponent] ([Id], [Name], [RubricId], [TotalMarks], [DateCreated], [DateUpdated], [AssessmentId]) VALUES (14, N'q1', 1, 10, CAST(0x0000AA3A00000000 AS DateTime), CAST(0x0000AA3A00000000 AS DateTime), 10)
INSERT [dbo].[AssessmentComponent] ([Id], [Name], [RubricId], [TotalMarks], [DateCreated], [DateUpdated], [AssessmentId]) VALUES (15, N'q2', 1, 5, CAST(0x0000AA3A00000000 AS DateTime), CAST(0x0000AA3A00000000 AS DateTime), 10)
SET IDENTITY_INSERT [dbo].[AssessmentComponent] OFF
SET IDENTITY_INSERT [dbo].[ClassAttendance] ON 

INSERT [dbo].[ClassAttendance] ([Id], [AttendanceDate]) VALUES (1, CAST(0x0000AA2000000000 AS DateTime))
INSERT [dbo].[ClassAttendance] ([Id], [AttendanceDate]) VALUES (2, CAST(0x0000AA2000000000 AS DateTime))
INSERT [dbo].[ClassAttendance] ([Id], [AttendanceDate]) VALUES (3, CAST(0x0000AA2100000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[ClassAttendance] OFF
SET IDENTITY_INSERT [dbo].[Clo] ON 

INSERT [dbo].[Clo] ([Id], [Name], [DateCreated], [DateUpdated]) VALUES (2, N'clooo', CAST(0x0000AA2000000000 AS DateTime), CAST(0x0000AA2000000000 AS DateTime))
INSERT [dbo].[Clo] ([Id], [Name], [DateCreated], [DateUpdated]) VALUES (4, N'cloooooooooooo', CAST(0x0000AA3A00000000 AS DateTime), CAST(0x0000AA3A00000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Clo] OFF
SET IDENTITY_INSERT [dbo].[Lookup] ON 

INSERT [dbo].[Lookup] ([LookupId], [Name], [Category]) VALUES (1, N'Present', N'ATTENDANCE_STATUS')
INSERT [dbo].[Lookup] ([LookupId], [Name], [Category]) VALUES (2, N'Absent', N'ATTENDANCE_STATUS')
INSERT [dbo].[Lookup] ([LookupId], [Name], [Category]) VALUES (3, N'Leave', N'ATTENDANCE_STATUS')
INSERT [dbo].[Lookup] ([LookupId], [Name], [Category]) VALUES (4, N'Late', N'ATTENDANCE_STATUS')
INSERT [dbo].[Lookup] ([LookupId], [Name], [Category]) VALUES (5, N'Active', N'STUDENT_STATUS')
INSERT [dbo].[Lookup] ([LookupId], [Name], [Category]) VALUES (6, N'InActive', N'STUDENT_STATUS')
SET IDENTITY_INSERT [dbo].[Lookup] OFF
SET IDENTITY_INSERT [dbo].[Rubric] ON 

INSERT [dbo].[Rubric] ([Id], [Details], [CloId]) VALUES (1, N'Design', 2)
INSERT [dbo].[Rubric] ([Id], [Details], [CloId]) VALUES (2, N'Execution', 2)
INSERT [dbo].[Rubric] ([Id], [Details], [CloId]) VALUES (3, N'Testing', 2)
SET IDENTITY_INSERT [dbo].[Rubric] OFF
SET IDENTITY_INSERT [dbo].[RubricLevel] ON 

INSERT [dbo].[RubricLevel] ([Id], [RubricId], [Details], [MeasurementLevel]) VALUES (1, 1, N'l1', 1)
INSERT [dbo].[RubricLevel] ([Id], [RubricId], [Details], [MeasurementLevel]) VALUES (2, 1, N'l2', 2)
INSERT [dbo].[RubricLevel] ([Id], [RubricId], [Details], [MeasurementLevel]) VALUES (3, 1, N'l3', 3)
INSERT [dbo].[RubricLevel] ([Id], [RubricId], [Details], [MeasurementLevel]) VALUES (4, 2, N'll1', 1)
INSERT [dbo].[RubricLevel] ([Id], [RubricId], [Details], [MeasurementLevel]) VALUES (5, 2, N'll2', 2)
INSERT [dbo].[RubricLevel] ([Id], [RubricId], [Details], [MeasurementLevel]) VALUES (6, 2, N'll3', 3)
INSERT [dbo].[RubricLevel] ([Id], [RubricId], [Details], [MeasurementLevel]) VALUES (7, 3, N'lll1', 1)
INSERT [dbo].[RubricLevel] ([Id], [RubricId], [Details], [MeasurementLevel]) VALUES (8, 3, N'lll2', 2)
INSERT [dbo].[RubricLevel] ([Id], [RubricId], [Details], [MeasurementLevel]) VALUES (9, 3, N'lll3', 3)
SET IDENTITY_INSERT [dbo].[RubricLevel] OFF
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([Id], [FirstName], [LastName], [Contact], [Email], [RegistrationNumber], [Status]) VALUES (1, N'Khadija', N'Zahoor', N'12334', N'k@gmail.com', N'2016cs259', 5)
INSERT [dbo].[Student] ([Id], [FirstName], [LastName], [Contact], [Email], [RegistrationNumber], [Status]) VALUES (2, N'Rahma', N'Tahir', N'23456', N'r@gmail.com', N'2016cs264', 5)
INSERT [dbo].[Student] ([Id], [FirstName], [LastName], [Contact], [Email], [RegistrationNumber], [Status]) VALUES (3, N'Javeria', N'Riaz', N'12345', N'j@gmail.com', N'2016cs251', 6)
SET IDENTITY_INSERT [dbo].[Student] OFF
INSERT [dbo].[StudentAttendance] ([AttendanceId], [StudentId], [AttendanceStatus]) VALUES (1, 1, 1)
INSERT [dbo].[StudentAttendance] ([AttendanceId], [StudentId], [AttendanceStatus]) VALUES (1, 2, 2)
INSERT [dbo].[StudentAttendance] ([AttendanceId], [StudentId], [AttendanceStatus]) VALUES (2, 1, 3)
INSERT [dbo].[StudentAttendance] ([AttendanceId], [StudentId], [AttendanceStatus]) VALUES (2, 2, 4)
INSERT [dbo].[StudentAttendance] ([AttendanceId], [StudentId], [AttendanceStatus]) VALUES (3, 1, 2)
INSERT [dbo].[StudentAttendance] ([AttendanceId], [StudentId], [AttendanceStatus]) VALUES (3, 2, 4)
INSERT [dbo].[StudentResult] ([StudentId], [AssessmentComponentId], [RubricMeasurementId], [EvaluationDate]) VALUES (1, 1, 2, CAST(0x0000AA2000000000 AS DateTime))
INSERT [dbo].[StudentResult] ([StudentId], [AssessmentComponentId], [RubricMeasurementId], [EvaluationDate]) VALUES (1, 2, 6, CAST(0x0000AA2000000000 AS DateTime))
INSERT [dbo].[StudentResult] ([StudentId], [AssessmentComponentId], [RubricMeasurementId], [EvaluationDate]) VALUES (1, 3, 9, CAST(0x0000AA2000000000 AS DateTime))
INSERT [dbo].[StudentResult] ([StudentId], [AssessmentComponentId], [RubricMeasurementId], [EvaluationDate]) VALUES (1, 13, 4, CAST(0x0000AA2100000000 AS DateTime))
INSERT [dbo].[StudentResult] ([StudentId], [AssessmentComponentId], [RubricMeasurementId], [EvaluationDate]) VALUES (2, 1, 3, CAST(0x0000AA2100000000 AS DateTime))
INSERT [dbo].[StudentResult] ([StudentId], [AssessmentComponentId], [RubricMeasurementId], [EvaluationDate]) VALUES (2, 2, 5, CAST(0x0000AA2100000000 AS DateTime))
INSERT [dbo].[StudentResult] ([StudentId], [AssessmentComponentId], [RubricMeasurementId], [EvaluationDate]) VALUES (2, 3, 7, CAST(0x0000AA2100000000 AS DateTime))
INSERT [dbo].[StudentResult] ([StudentId], [AssessmentComponentId], [RubricMeasurementId], [EvaluationDate]) VALUES (2, 13, 1, CAST(0x0000AA3A00000000 AS DateTime))
ALTER TABLE [dbo].[AssessmentComponent]  WITH CHECK ADD  CONSTRAINT [FK_AssessmentComponent_Assessment] FOREIGN KEY([AssessmentId])
REFERENCES [dbo].[Assessment] ([Id])
GO
ALTER TABLE [dbo].[AssessmentComponent] CHECK CONSTRAINT [FK_AssessmentComponent_Assessment]
GO
ALTER TABLE [dbo].[AssessmentComponent]  WITH CHECK ADD  CONSTRAINT [FK_AssessmentComponent_Rubric] FOREIGN KEY([RubricId])
REFERENCES [dbo].[Rubric] ([Id])
GO
ALTER TABLE [dbo].[AssessmentComponent] CHECK CONSTRAINT [FK_AssessmentComponent_Rubric]
GO
ALTER TABLE [dbo].[Rubric]  WITH CHECK ADD  CONSTRAINT [FK_Rubric_Clo] FOREIGN KEY([CloId])
REFERENCES [dbo].[Clo] ([Id])
GO
ALTER TABLE [dbo].[Rubric] CHECK CONSTRAINT [FK_Rubric_Clo]
GO
ALTER TABLE [dbo].[RubricLevel]  WITH CHECK ADD  CONSTRAINT [FK_RubricLevel_Rubric] FOREIGN KEY([RubricId])
REFERENCES [dbo].[Rubric] ([Id])
GO
ALTER TABLE [dbo].[RubricLevel] CHECK CONSTRAINT [FK_RubricLevel_Rubric]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Lookup] FOREIGN KEY([Status])
REFERENCES [dbo].[Lookup] ([LookupId])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Lookup]
GO
ALTER TABLE [dbo].[StudentAttendance]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendance_ClassAttendance] FOREIGN KEY([AttendanceId])
REFERENCES [dbo].[ClassAttendance] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendance] CHECK CONSTRAINT [FK_StudentAttendance_ClassAttendance]
GO
ALTER TABLE [dbo].[StudentAttendance]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendance_Lookup] FOREIGN KEY([AttendanceStatus])
REFERENCES [dbo].[Lookup] ([LookupId])
GO
ALTER TABLE [dbo].[StudentAttendance] CHECK CONSTRAINT [FK_StudentAttendance_Lookup]
GO
ALTER TABLE [dbo].[StudentAttendance]  WITH CHECK ADD  CONSTRAINT [FK_StudentAttendance_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[StudentAttendance] CHECK CONSTRAINT [FK_StudentAttendance_Student]
GO
ALTER TABLE [dbo].[StudentResult]  WITH CHECK ADD  CONSTRAINT [FK_StudentResult_AssessmentComponent] FOREIGN KEY([AssessmentComponentId])
REFERENCES [dbo].[AssessmentComponent] ([Id])
GO
ALTER TABLE [dbo].[StudentResult] CHECK CONSTRAINT [FK_StudentResult_AssessmentComponent]
GO
ALTER TABLE [dbo].[StudentResult]  WITH CHECK ADD  CONSTRAINT [FK_StudentResult_RubricLevel] FOREIGN KEY([RubricMeasurementId])
REFERENCES [dbo].[RubricLevel] ([Id])
GO
ALTER TABLE [dbo].[StudentResult] CHECK CONSTRAINT [FK_StudentResult_RubricLevel]
GO
ALTER TABLE [dbo].[StudentResult]  WITH CHECK ADD  CONSTRAINT [FK_StudentResult_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[StudentResult] CHECK CONSTRAINT [FK_StudentResult_Student]
GO
