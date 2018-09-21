USE TimeTableDatabase
GO

INSERT INTO [dbo].ClassRooms([Name], [NumberOfPlaces], [Type], [IsPCavailable]) VALUES ('101','100','Srautinë','0')
INSERT INTO [dbo].ClassRooms([Name], [NumberOfPlaces], [Type], [IsPCavailable]) VALUES ('102','30','Praktinë','1')
INSERT INTO [dbo].ClassRooms([Name], [NumberOfPlaces], [Type], [IsPCavailable]) VALUES ('103','30','Praktinë','1')
INSERT INTO [dbo].ClassRooms([Name], [NumberOfPlaces], [Type], [IsPCavailable]) VALUES ('104','30','Praktinë','1')
INSERT INTO [dbo].ClassRooms([Name], [NumberOfPlaces], [Type], [IsPCavailable]) VALUES ('105','30','Praktinë','1')

INSERT INTO [dbo].Groups([Name], [StudentsCount]) VALUES ('IFF-5/1','30')
INSERT INTO [dbo].Groups([Name], [StudentsCount]) VALUES ('IFF-5/2','30')
INSERT INTO [dbo].Groups([Name], [StudentsCount]) VALUES ('IFF-5/3','30')
INSERT INTO [dbo].Groups([Name], [StudentsCount]) VALUES ('IFF-5/4','30')
INSERT INTO [dbo].Groups([Name], [StudentsCount]) VALUES ('IFF-5/5','30')

INSERT INTO [dbo].Subjects([Name], [Code]) VALUES ('Matematika 1','P175B200')
INSERT INTO [dbo].Subjects([Name], [Code]) VALUES ('Algoritmai','P175B420')

INSERT INTO [dbo].Teachers([FirstName], [LastName], [Module]) VALUES ('Aldona','Butkaitë','Matematika 1')
INSERT INTO [dbo].Teachers([FirstName], [LastName], [Module]) VALUES ('Petras','Petraitis','Algoritmai')
INSERT INTO [dbo].Teachers([FirstName], [LastName], [Module]) VALUES ('Rokas','Jurkus','Algoritmai')

INSERT INTO [dbo].Lectures([TeacherID], [GroupID], [ClassRoomID], [LectureTimeID], [WeekdayID], [SubjectID], [Type], [IsPcRequired]) VALUES ('1','1','1','2','2','1','Srautinë','0')
INSERT INTO [dbo].Lectures([TeacherID], [GroupID], [ClassRoomID], [LectureTimeID], [WeekdayID], [SubjectID], [Type], [IsPcRequired]) VALUES ('3','2','2','1','2','2','Praktinë','1')
INSERT INTO [dbo].Lectures([TeacherID], [GroupID], [ClassRoomID], [LectureTimeID], [WeekdayID], [SubjectID], [Type], [IsPcRequired]) VALUES ('2','3','3','1','4','2','Praktinë','1')
INSERT INTO [dbo].Lectures([TeacherID], [GroupID], [ClassRoomID], [LectureTimeID], [WeekdayID], [SubjectID], [Type], [IsPcRequired]) VALUES ('1','4','4','1','2','1','Praktinë','1')
INSERT INTO [dbo].Lectures([TeacherID], [GroupID], [ClassRoomID], [LectureTimeID], [WeekdayID], [SubjectID], [Type], [IsPcRequired]) VALUES ('2','5','5','1','2','2','Praktinë','1')
INSERT INTO [dbo].Lectures([TeacherID], [GroupID], [ClassRoomID], [LectureTimeID], [WeekdayID], [SubjectID], [Type], [IsPcRequired]) VALUES ('3','5','5','4','2','2','Praktinë','1')
INSERT INTO [dbo].Lectures([TeacherID], [GroupID], [ClassRoomID], [LectureTimeID], [WeekdayID], [SubjectID], [Type], [IsPcRequired]) VALUES ('2','3','4','4','2','2','Praktinë','1')

