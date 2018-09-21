USE [TimeTableDatabase]
GO
SET IDENTITY_INSERT [dbo].[LectureTimes] ON 

INSERT [dbo].[LectureTimes] ([ID], [Start], [End]) VALUES (1, CAST(N'08:30:00' AS Time), CAST(N'10:00:00' AS Time))
INSERT [dbo].[LectureTimes] ([ID], [Start], [End]) VALUES (2, CAST(N'10:30:00' AS Time), CAST(N'12:00:00' AS Time))
INSERT [dbo].[LectureTimes] ([ID], [Start], [End]) VALUES (3, CAST(N'13:00:00' AS Time), CAST(N'14:30:00' AS Time))
INSERT [dbo].[LectureTimes] ([ID], [Start], [End]) VALUES (4, CAST(N'15:00:00' AS Time), CAST(N'16:30:00' AS Time))
SET IDENTITY_INSERT [dbo].[LectureTimes] OFF
