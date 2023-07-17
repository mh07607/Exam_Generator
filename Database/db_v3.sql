
/****** Object:  User [Admin]    Script Date: 7/17/2023 12:24:23 PM ******/
CREATE USER [Admin] FOR LOGIN [Admin] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 7/17/2023 12:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[AdminID] [int] NOT NULL,
	[Password] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ADMIN] PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 7/17/2023 12:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[SubjectId] [int] NOT NULL,
	[BookName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BookID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BOOK] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book_Topic]    Script Date: 7/17/2023 12:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book_Topic](
	[BookId] [int] NOT NULL,
	[TopicId] [int] NOT NULL,
 CONSTRAINT [PK_BOOK_TOPIC] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MCQs]    Script Date: 7/17/2023 12:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MCQs](
	[MCQID] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) COLLATE Arabic_100_BIN NOT NULL,
	[Difficulty] [int] NOT NULL,
	[TopicId] [int] NOT NULL,
	[OptionA] [nvarchar](max) COLLATE Arabic_100_CI_AI_SC_UTF8 NOT NULL,
	[OptionB] [nvarchar](max) COLLATE Arabic_100_CI_AI_SC_UTF8 NOT NULL,
	[OptionC] [nvarchar](max) COLLATE Arabic_100_CI_AI_SC_UTF8 NOT NULL,
	[OptionD] [nvarchar](max) COLLATE Arabic_100_CI_AI_SC_UTF8 NOT NULL,
	[Answer] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_MCQS] PRIMARY KEY CLUSTERED 
(
	[MCQID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paper_MCQ]    Script Date: 7/17/2023 12:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paper_MCQ](
	[PaperID] [int] NOT NULL,
	[MCQID] [int] NOT NULL,
 CONSTRAINT [PK_PAPER_MCQ] PRIMARY KEY CLUSTERED 
(
	[PaperID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paper_Question]    Script Date: 7/17/2023 12:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paper_Question](
	[PaperID] [int] NOT NULL,
	[QuestionID] [int] NOT NULL,
 CONSTRAINT [PK_PAPER_QUESTION] PRIMARY KEY CLUSTERED 
(
	[PaperID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Past_Papers]    Script Date: 7/17/2023 12:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Past_Papers](
	[PaperID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_PAST_PAPERS] PRIMARY KEY CLUSTERED 
(
	[PaperID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 7/17/2023 12:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[TopicID] [int] NOT NULL,
	[Difficulty] [int] NOT NULL,
	[Type] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Content] [nvarchar](max) COLLATE Arabic_100_CI_AI_SC_UTF8 NOT NULL,
	[Diagram] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_QUESTIONS] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 7/17/2023 12:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectName] [nvarchar](max) COLLATE Arabic_100_CI_AI_SC_UTF8 NOT NULL,
 CONSTRAINT [PK_SUBJECT] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topic]    Script Date: 7/17/2023 12:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topic](
	[TopicId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectId] [int] NULL,
	[TopicName] [nvarchar](max) COLLATE Arabic_100_CI_AI_SC_UTF8 NOT NULL,
	[ShortQs_Weightage] [float] NOT NULL,
	[LongQs_Weightage] [float] NOT NULL,
	[MCQs_Weightage] [float] NOT NULL,
 CONSTRAINT [PK_TOPIC] PRIMARY KEY CLUSTERED 
(
	[TopicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Book] ON 
GO
INSERT [dbo].[Book] ([SubjectId], [BookName], [BookID]) VALUES (3, N'اسلام کا فلسفہ
', 1)
GO
SET IDENTITY_INSERT [dbo].[Book] OFF
GO
INSERT [dbo].[Book_Topic] ([BookId], [TopicId]) VALUES (1, 1)
GO
SET IDENTITY_INSERT [dbo].[MCQs] ON 
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (1013, N'کس علم کو علومِ رجال کے تحت پیش کیا جاتا ہے؟', 1, 6, N'علمِ تفسیر', N'علمِ فقہ', N'علمِ حدیث', N'علمِ تجوید', N'C')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (1014, N'قرآن مجید کی تشریعی تاریخ کیا ہے؟', 1, 4, N'1400 ہجری', N'1440 ہجری', N'1500 ہجری', N'1600 ہجری', N'B')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (1015, N'سنتِ نبوی صلی اللہ علیہ وسلم سے اخلاقی تربیت کی تعلیم میں کونسا علم شامل ہے؟', 1, 5, N'علم الحدیث', N'علم الفقہ', N'علم القرآن', N'علم السیرت', N'D')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (1016, N'مسلمانوں کی تعلیمِ اخلاق سنتِ نبوی صلی اللہ علیہ وسلم سے کس علم کے ذریعے ہوتی ہے؟', 1, 5, N'علم الحدیث', N'علم الفقہ', N'علم القرآن', N'علم السیرت', N'B')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (1017, N'کونسا علم اسلامی تجارتی نظام کو تعریف کرتا ہے؟', 1, 6, N'علم الاقتصاد', N'علم القرآن', N'علم الحدیث', N'علم الفقہ', N'A')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (1018, N'اعجازِ قرآن مجید کی تحقیق و تجزیہ کرنے والا علم کیا ہے؟', 1, 10, N'علم القرآن', N'علم الفقہ', N'علم الحدیث', N'علم السیرت', N'A')
GO
SET IDENTITY_INSERT [dbo].[MCQs] OFF
GO
SET IDENTITY_INSERT [dbo].[Questions] ON 
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [Difficulty], [Type], [Content], [Diagram]) VALUES (2, 1, 3, N'long', N'قرآن یہودیوں کے بارے میں کیا کہتا ہے؟
', NULL)
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [Difficulty], [Type], [Content], [Diagram]) VALUES (3, 2, 1, N'short', N'سنّتِ نبوی میں پڑھائی گئی جار و جیران کے حقوق کیا ہیں؟
', NULL)
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [Difficulty], [Type], [Content], [Diagram]) VALUES (4, 2, 2, N'short', N'مسلمان کو اپنے گھر کے باہر واقع جار و جیران کے ساتھ کس طرح پیش آنا چاہیے؟
', NULL)
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [Difficulty], [Type], [Content], [Diagram]) VALUES (5, 3, 3, N'long', N'ماذا يعني تربية الأخلاق وما هي أهمية سنّتِ نبوی في هذا الصدد؟
', NULL)
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [Difficulty], [Type], [Content], [Diagram]) VALUES (6, 3, 2, N'short', N'ما هي القيم الأخلاقية التي يجب أن يتحلى بها المسلم بناءً على سنّتِ نبوی؟
', NULL)
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [Difficulty], [Type], [Content], [Diagram]) VALUES (9, 4, 1, N'long', N'نبی کریم صلی اللہ علیہ وسلم کے زمانے میں آیات کی تحریر اور جمع کرنے والے کون تھے؟
', NULL)
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [Difficulty], [Type], [Content], [Diagram]) VALUES (11, 4, 1, N'short', N'قرآن مجید کی تحریر اور مصحف شریف کا جمع کب شروع ہوا؟
', NULL)
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [Difficulty], [Type], [Content], [Diagram]) VALUES (12, 1, 1, N'short', N'Define English.', NULL)
GO
SET IDENTITY_INSERT [dbo].[Questions] OFF
GO
SET IDENTITY_INSERT [dbo].[Subject] ON 
GO
INSERT [dbo].[Subject] ([SubjectId], [SubjectName]) VALUES (1, N'سنّتِ نبوی')
GO
INSERT [dbo].[Subject] ([SubjectId], [SubjectName]) VALUES (2, N'علوم الحديث')
GO
INSERT [dbo].[Subject] ([SubjectId], [SubjectName]) VALUES (3, N'قرآن مجید')
GO
SET IDENTITY_INSERT [dbo].[Subject] OFF
GO
SET IDENTITY_INSERT [dbo].[Topic] ON 
GO
INSERT [dbo].[Topic] ([TopicId], [SubjectId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (1, NULL, N'قرآن مجید کا مختصر تعارف', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [SubjectId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (2, NULL, N'حقوق الجار والجيران', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [SubjectId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (3, NULL, N'تربیتِ اخلاقی از سنتِ نبوی', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [SubjectId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (4, NULL, N'مصحفِ شریف کی تشریعی تاریخ', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [SubjectId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (5, NULL, N'تعلیمِ اخلاق از سنتِ نبوی', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [SubjectId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (6, NULL, N'نظامِ اقتصادِ اسلامی از سنتِ نبوی', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [SubjectId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (7, NULL, N'تدریسِ علوم الحدیث', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [SubjectId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (8, NULL, N'علومِ رجال', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [SubjectId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (9, NULL, N'ضابطہ بخاری و مسلم', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [SubjectId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (10, NULL, N'اعجازِ قرآن مجید', 10, 10, 10)
GO
SET IDENTITY_INSERT [dbo].[Topic] OFF
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [Book_fk0] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [Book_fk0]
GO
ALTER TABLE [dbo].[Book_Topic]  WITH CHECK ADD  CONSTRAINT [Book_Topic_fk0] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Book_Topic] CHECK CONSTRAINT [Book_Topic_fk0]
GO
ALTER TABLE [dbo].[Book_Topic]  WITH CHECK ADD  CONSTRAINT [Book_Topic_fk1] FOREIGN KEY([TopicId])
REFERENCES [dbo].[Topic] ([TopicId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Book_Topic] CHECK CONSTRAINT [Book_Topic_fk1]
GO
ALTER TABLE [dbo].[MCQs]  WITH CHECK ADD  CONSTRAINT [FK_MCQs_Topic] FOREIGN KEY([TopicId])
REFERENCES [dbo].[Topic] ([TopicId])
GO
ALTER TABLE [dbo].[MCQs] CHECK CONSTRAINT [FK_MCQs_Topic]
GO
ALTER TABLE [dbo].[Paper_MCQ]  WITH CHECK ADD  CONSTRAINT [Paper_MCQ_fk0] FOREIGN KEY([PaperID])
REFERENCES [dbo].[Past_Papers] ([PaperID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Paper_MCQ] CHECK CONSTRAINT [Paper_MCQ_fk0]
GO
ALTER TABLE [dbo].[Paper_MCQ]  WITH CHECK ADD  CONSTRAINT [Paper_MCQ_fk1] FOREIGN KEY([MCQID])
REFERENCES [dbo].[MCQs] ([MCQID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Paper_MCQ] CHECK CONSTRAINT [Paper_MCQ_fk1]
GO
ALTER TABLE [dbo].[Paper_Question]  WITH CHECK ADD  CONSTRAINT [Paper_Question_fk0] FOREIGN KEY([PaperID])
REFERENCES [dbo].[Past_Papers] ([PaperID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Paper_Question] CHECK CONSTRAINT [Paper_Question_fk0]
GO
ALTER TABLE [dbo].[Paper_Question]  WITH CHECK ADD  CONSTRAINT [Paper_Question_fk1] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Questions] ([QuestionID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Paper_Question] CHECK CONSTRAINT [Paper_Question_fk1]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [Questions_fk0] FOREIGN KEY([TopicID])
REFERENCES [dbo].[Topic] ([TopicId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [Questions_fk0]
GO
ALTER TABLE [dbo].[Topic]  WITH CHECK ADD  CONSTRAINT [FK_Topic_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[Topic] CHECK CONSTRAINT [FK_Topic_Subject]
GO
/****** Object:  StoredProcedure [dbo].[GetRandomQuestions]    Script Date: 7/17/2023 12:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* insert in subject */
/*CREATE PROCEDURE insert_subject @subject_name text
AS 
BEGIN
	INSERT INTO Subject (SubjectName)
	VALUES (@subject_name);
END
*/

/*Use these to get a given past papers contents*/
/*Create PROCEDURE GetPastPaperQuestions(@PaperId int)
AS
BEGIN
	select Questions.Content, Questions.[Type]
	from Questions, Paper_Question
	where Paper_Question.PaperID =@PaperId and Paper_Question.QuestionID = Questions.QuestionID
end;*/


/*Create PROCEDURE GetPastPaperMCQs(@PaperId int)
AS
BEGIN
	select MCQs.Content, MCQs.OptionA, MCQs.OptionB, MCQs.OptionC, MCQs.OptionD
	from MCQS, Paper_MCQ
	where Paper_MCQ.PaperID = @PaperId and Paper_MCQ.MCQID = MCQS.MCQID
end;
*/

/*Selects a given number of random MCQs*/
/*CREATE PROCEDURE GetRandomMCQs(@topicId int, @subjectName nvarchar(max), @number int, @difficulty int)
AS
BEGIN
	SELECT TOP (@number) *
	FROM MCQs
	WHERE Difficulty = @difficulty AND TopicId = @topicId AND SubjectId = (SELECT SubjectId
																			FROM [Subject]
																			WHERE SubjectName = @subjectName)
	ORDER BY NEWID()
END*/

CREATE PROCEDURE [dbo].[GetRandomQuestions](@numEasy int, 
@numMedium int, 
@numHard int, 
@type nvarchar(max))
AS
BEGIN
	SELECT * FROM (
	SELECT TOP (@numEasy) *
	FROM Questions
	WHERE Difficulty = 1 and [Type] = @type
	ORDER BY NEWID()
	) YEET

	UNION ALL

	SELECT * FROM (
	SELECT TOP (@numMedium) *
	FROM Questions
	WHERE Difficulty = 2 and [Type] = @type
	ORDER BY NEWID()
	) 
	DEFEAT
	UNION ALL

	SELECT * FROM (
	SELECT TOP (@numHard) *
	FROM Questions
	WHERE Difficulty = 3 and [Type] = @type
	ORDER BY NEWID()
	) 
	MEAT
	
END


/*Selects a given number of random short Questions*/
/*SELECT TOP 2 *
FROM Questions
WHERE Difficulty = 1 AND [Type] = 'short'
ORDER BY NEWID()*/

/*Selects a given number of random long Questions*/
/*SELECT TOP 2 *
FROM Questions
WHERE Difficulty = 3 AND [Type] = 'long'
ORDER BY NEWID()*/

/*CREATE PROCEDURE InsertQuestion (@TopicID int, @SubjectID int, @Difficulty int, @Type text, @Content nvarchar(max))
AS
BEGIN
	insert into Questions (TopicID, SubjectID, Difficulty, [Type], Content) values (@TopicID, @SubjectID, @Difficulty, @Type, @Content)
END


/*CREATE PROCEDURE InsertMCQ (@Content nvarchar(max), @Difficulty int, @TopicID int, @OptionA nvarchar(max), @OptionB nvarchar(max), @OptionC nvarchar(max), @OptionD nvarchar(max), @Answer varchar)
AS
BEGIN
	insert into MCQs (Content, Difficulty, TopicId, OptionA, OptionB, OptionC, OptionD, Answer) values (@Content, @Difficulty, @TopicID, @OptionA, @OptionB, @OptionC, @OptionD, @Answer)
END*/


/*Should fill more dummy data using ChatGPT and the above procedures, would probably need to show to chatGPT
the Topic and Subject table*/

/*SubjectId should be in Topic not in Questions and MCQs, should change schema*/

/*
EXEC GetRandomMCQs 4, N'سنّتِ نبوی', 5, 3;
*/

--EXEC InsertMCQ N'کس علم کو علومِ رجال کے تحت پیش کیا جاتا ہے؟', 1, 6, N'علمِ تفسیر', N'علمِ فقہ', N'علمِ حدیث', N'علمِ تجوید', 'C'

/*-- Call 1
EXEC InsertMCQ N'قرآن مجید کی تشریعی تاریخ کیا ہے؟', 1, 4, N'1400 ہجری', N'1440 ہجری', N'1500 ہجری', N'1600 ہجری', 'B'

-- Call 2
EXEC InsertMCQ N'سنتِ نبوی صلی اللہ علیہ وسلم سے اخلاقی تربیت کی تعلیم میں کونسا علم شامل ہے؟', 1, 5, N'علم الحدیث', N'علم الفقہ', N'علم القرآن', N'علم السیرت', 'D'

-- Call 3
EXEC InsertMCQ N'مسلمانوں کی تعلیمِ اخلاق سنتِ نبوی صلی اللہ علیہ وسلم سے کس علم کے ذریعے ہوتی ہے؟', 1, 5, N'علم الحدیث', N'علم الفقہ', N'علم القرآن', N'علم السیرت', 'B'

-- Call 4
EXEC InsertMCQ N'کونسا علم اسلامی تجارتی نظام کو تعریف کرتا ہے؟', 1, 6, N'علم الاقتصاد', N'علم القرآن', N'علم الحدیث', N'علم الفقہ', 'A'

-- Call 5
EXEC InsertMCQ N'اعجازِ قرآن مجید کی تحقیق و تجزیہ کرنے والا علم کیا ہے؟', 1, 10, N'علم القرآن', N'علم الفقہ', N'علم الحدیث', N'علم السیرت', 'A'

*/

select * from Topic


CREATE PROCEDURE GetSubjects ()
AS
BEGIN
	select SubjectID, SubjectName
	from Subject
END
*/

--EXEC GetRandomQuestions 2, 3, 'long' 
/*EXEC GetRandomQuestions 2, 
2, 
1, 
69, 
'short'
*/
GO
/****** Object:  StoredProcedure [dbo].[InsertMCQ]    Script Date: 7/17/2023 12:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/* insert in subject */
/*CREATE PROCEDURE insert_subject @subject_name text
AS 
BEGIN
	INSERT INTO Subject (SubjectName)
	VALUES (@subject_name);
END
*/

/*Use these to get a given past papers contents*/
/*Create PROCEDURE GetPastPaperQuestions(@PaperId int)
AS
BEGIN
	select Questions.Content, Questions.[Type]
	from Questions, Paper_Question
	where Paper_Question.PaperID =@PaperId and Paper_Question.QuestionID = Questions.QuestionID
end;
*/

/*Create PROCEDURE GetPastPaperMCQs(@PaperId int)
AS
BEGIN
	select MCQs.Content, MCQs.OptionA, MCQs.OptionB, MCQs.OptionC, MCQs.OptionD
	from MCQS, Paper_MCQ
	where Paper_MCQ.PaperID = @PaperId and Paper_MCQ.MCQID = MCQS.MCQID
end;
*/

/*Selects a given number of random MCQs*/
/*CREATE PROCEDURE GetRandomMCQs(@topicId int, @subjectName nvarchar(max), @number int, @difficulty int)
AS
BEGIN
	SELECT TOP (@number) *
	FROM MCQs
	WHERE Difficulty = @difficulty AND TopicId = @topicId AND SubjectId = (SELECT SubjectId
																			FROM [Subject]
																			WHERE SubjectName = @subjectName)
	ORDER BY NEWID()
END*/

/*Selects a given number of random short Questions*/
/*SELECT TOP 2 *
CREATE PROCEDURE GetQuestion(@TopicID int, @SubjectID int, @Difficulty int, @Type text, @number int)
AS
BEGIN
	SELECT TOP (@number) *
	FROM Questions
	WHERE Difficulty = @Difficulty AND [Type] = @Type and SubjectID = @SubjectId and TopicId = @TopicID 
	ORDER BY NEWID()
END*/

/*Selects a given number of random long Questions*/
/*SELECT TOP 2 *
FROM Questions
WHERE Difficulty = 3 AND [Type] = 'long'
ORDER BY NEWID()*/

/*CREATE PROCEDURE InsertQuestion (@TopicID int, @SubjectID int, @Difficulty int, @Type text, @Content nvarchar(max))
AS
BEGIN
	insert into Questions (TopicID, SubjectID, Difficulty, [Type], Content) values (@TopicID, @SubjectID, @Difficulty, @Type, @Content)
END */


CREATE PROCEDURE [dbo].[InsertMCQ] (@Content nvarchar(max), @Difficulty int, @TopicID int, @OptionA nvarchar(max), @OptionB nvarchar(max), @OptionC nvarchar(max), @OptionD nvarchar(max), @Answer varchar)
AS
BEGIN
	insert into MCQs (Content, Difficulty, TopicId, OptionA, OptionB, OptionC, OptionD, Answer) values (@Content, @Difficulty, @TopicID, @OptionA, @OptionB, @OptionC, @OptionD, @Answer)
END


/*Should fill more dummy data using ChatGPT and the above procedures, would probably need to show to chatGPT
the Topic and Subject table*/

/*SubjectId should be in Topic not in Questions and MCQs, should change schema*/

/*
EXEC GetRandomMCQs 4, N'سنّتِ نبوی', 5, 3;
*/

--EXEC InsertMCQ N'کس علم کو علومِ رجال کے تحت پیش کیا جاتا ہے؟', 1, 6, 2, N'علمِ تفسیر', N'علمِ فقہ', N'علمِ حدیث', N'علمِ تجوید', 'C'







GO
USE [master]
GO
ALTER DATABASE [khidmat_test1] SET  READ_WRITE 
GO
