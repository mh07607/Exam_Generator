
/****** Object:  Table [dbo].[Admin]    Script Date: 7/1/2023 4:14:13 PM ******/
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
/****** Object:  Table [dbo].[Book]    Script Date: 7/1/2023 4:14:13 PM ******/
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
/****** Object:  Table [dbo].[Book_Topic]    Script Date: 7/1/2023 4:14:13 PM ******/
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
/****** Object:  Table [dbo].[MCQs]    Script Date: 7/1/2023 4:14:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MCQs](
	[MCQID] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) COLLATE Arabic_100_BIN NOT NULL,
	[Difficulty] [int] NOT NULL,
	[TopicId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
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
/****** Object:  Table [dbo].[Paper_MCQ]    Script Date: 7/1/2023 4:14:13 PM ******/
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
/****** Object:  Table [dbo].[Paper_Question]    Script Date: 7/1/2023 4:14:13 PM ******/
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
/****** Object:  Table [dbo].[Past_Papers]    Script Date: 7/1/2023 4:14:13 PM ******/
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
/****** Object:  Table [dbo].[Questions]    Script Date: 7/1/2023 4:14:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[TopicID] [int] NOT NULL,
	[SubjectID] [int] NOT NULL,
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
/****** Object:  Table [dbo].[Subject]    Script Date: 7/1/2023 4:14:13 PM ******/
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
/****** Object:  Table [dbo].[Topic]    Script Date: 7/1/2023 4:14:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topic](
	[TopicId] [int] IDENTITY(1,1) NOT NULL,
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
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [SubjectId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (1, N'سنّتِ نبوی کے مطابق، جار و جیران کے حقوق کی تشریع کب ہوئی؟
', 2, 2, 1, N'فتحہ مکہ کے بعد', N'ہجرت کے بعد', N'غزوہ خیبر کے بعد', N'کربلا کے بعد', N'A')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [SubjectId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (2, N'سنّتِ نبوی میں گزارش کی گئی تقریر "من لا یؤمن بجاریہ، فلیس منا" کے مطابق کس کی تشریع ہوئی؟
', 3, 2, 1, N' حضرت علی (رضی اللہ عنہ)', N'حضرت عمر بن الخطاب (رضی اللہ عنہ)', N'حضرت عثمان بن عفان (رضی اللہ عنہ)', N'حضرت ابو بکر صدیق (رضی اللہ عنہ)', N'B')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [SubjectId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (3, N'ما هي القيمة الأخلاقية التي أكد عليها النبي صلى الله عليه وسلم بالتصريح قائلاً "إنما بعثت لأتمم مكارم الأخلاق"؟', 1, 3, 1, N'الأمانة', N'الرفق واللين', N'الصدق والصداقة', N'العدل والإنصاف', N'A')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [SubjectId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (4, N'ما هي الخصلة الأخلاقية التي عظمها النبي صلى الله عليه وسلم وحث على ترسيخها في النفوس؟', 2, 3, 1, N'الصبر', N'الشجاعة', N'الصداقة', N'الرحمة', N'C')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [SubjectId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (5, N'من الخلفاء الراشدين الذي أمر بتجميع المصحف الشريف على شكل كتاب واحد؟
', 3, 4, 3, N'بو بكر الصديق رضي الله عنه', N'عمر بن الخطاب رضي الله عنه', N'عثمان بن عفان رضي الله عنه', N'علي بن أبي طالب رضي الله عنه', N'D')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [SubjectId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (6, N'ما هو اسم الصحابي الذي كان مسؤولًا عن تدوين القرآن في عهد النبي صلى الله عليه وسلم وعند أبو بكر الصديق رضي الله عنه؟', 2, 4, 3, N'زيد بن ثابت رضي الله عنه', N'عبد الله بن مسعود رضي الله عنه', N'عبد الله بن عمر رضي الله عنه', N'أبو هريرة رضي الله عنه', N'C')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [SubjectId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (7, N'نبی کریم صلی اللہ علیہ وسلم نے اصحاب کو کس خصوصی اخلاقی خصلت کی تعلیم کی ضرورت اور اہمیت پر زور دیا؟', 3, 5, 1, N'صبر', N'صدق', N'عدل', N'رحمت', N'B')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [SubjectId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (8, N'نبی کریم صلی اللہ علیہ وسلم نے اصحاب کو اخلاقی تعلیم کیسے دیتے تھے؟
', 2, 5, 1, N'خطابات اور تقریرات کے ذریعے', N'تمثیلی کہانیوں کے ذریعے', N'اقوال اور حدیثوں کے ذریعے', N'اپنے عمل اور تمثیل کے ذریعے', N'A')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [SubjectId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (9, N'کس علم کو علومِ رجال کے تحت پیش کیا جاتا ہے؟
', 1, 6, 2, N'علمِ تفسیر', N'علمِ فقہ', N'علمِ حدیث', N'علمِ تجوید', N'C')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [SubjectId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (10, N'علومِ رجال کی مدد سے کیا جانا جاتا ہے؟', 2, 6, 2, N'احادیث کی صحت و ضعف', N'قرآن کی ترجمہ و تفسیر', N'سنتِ نبوی صلی اللہ علیہ وسلم کی سند', N'تشریعی مسائل کا حل', N'A')
GO
INSERT [dbo].[MCQs] ([MCQID], [Content], [Difficulty], [TopicId], [SubjectId], [OptionA], [OptionB], [OptionC], [OptionD], [Answer]) VALUES (12, N'علومِ رجال میں استعمال ہونے والے اصطلاح "رجال" سے کیا مراد ہوتا ہے؟', 3, 6, 2, N'حدیث کا نقل کرنے والے افراد', N'حدیث کی مختلف روایات', N'حدیث کی مطلوبہ خصوصیات', N'حدیث کے تشریعی احکام', N'D')
GO
SET IDENTITY_INSERT [dbo].[MCQs] OFF
GO
SET IDENTITY_INSERT [dbo].[Questions] ON 
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [SubjectID], [Difficulty], [Type], [Content], [Diagram]) VALUES (2, 1, 3, 3, N'long', N'قرآن یہودیوں کے بارے میں کیا کہتا ہے؟
', NULL)
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [SubjectID], [Difficulty], [Type], [Content], [Diagram]) VALUES (3, 2, 1, 1, N'short', N'سنّتِ نبوی میں پڑھائی گئی جار و جیران کے حقوق کیا ہیں؟
', NULL)
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [SubjectID], [Difficulty], [Type], [Content], [Diagram]) VALUES (4, 2, 1, 2, N'short', N'مسلمان کو اپنے گھر کے باہر واقع جار و جیران کے ساتھ کس طرح پیش آنا چاہیے؟
', NULL)
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [SubjectID], [Difficulty], [Type], [Content], [Diagram]) VALUES (5, 3, 1, 3, N'long', N'ماذا يعني تربية الأخلاق وما هي أهمية سنّتِ نبوی في هذا الصدد؟
', NULL)
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [SubjectID], [Difficulty], [Type], [Content], [Diagram]) VALUES (6, 3, 1, 2, N'short', N'ما هي القيم الأخلاقية التي يجب أن يتحلى بها المسلم بناءً على سنّتِ نبوی؟
', NULL)
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [SubjectID], [Difficulty], [Type], [Content], [Diagram]) VALUES (9, 4, 3, 1, N'long', N'نبی کریم صلی اللہ علیہ وسلم کے زمانے میں آیات کی تحریر اور جمع کرنے والے کون تھے؟
', NULL)
GO
INSERT [dbo].[Questions] ([QuestionID], [TopicID], [SubjectID], [Difficulty], [Type], [Content], [Diagram]) VALUES (11, 4, 3, 1, N'short', N'قرآن مجید کی تحریر اور مصحف شریف کا جمع کب شروع ہوا؟
', NULL)
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
INSERT [dbo].[Topic] ([TopicId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (1, N'قرآن مجید کا مختصر تعارف', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (2, N'حقوق الجار والجيران', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (3, N'تربیتِ اخلاقی از سنتِ نبوی', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (4, N'مصحفِ شریف کی تشریعی تاریخ', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (5, N'تعلیمِ اخلاق از سنتِ نبوی', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (6, N'نظامِ اقتصادِ اسلامی از سنتِ نبوی', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (7, N'تدریسِ علوم الحدیث', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (8, N'علومِ رجال', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (9, N'ضابطہ بخاری و مسلم', 10, 10, 10)
GO
INSERT [dbo].[Topic] ([TopicId], [TopicName], [ShortQs_Weightage], [LongQs_Weightage], [MCQs_Weightage]) VALUES (10, N'اعجازِ قرآن مجید', 10, 10, 10)
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
ALTER TABLE [dbo].[MCQs]  WITH CHECK ADD  CONSTRAINT [FK_MCQs_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[MCQs] CHECK CONSTRAINT [FK_MCQs_Subject]
GO
ALTER TABLE [dbo].[MCQs]  WITH CHECK ADD  CONSTRAINT [FK_MCQs_Subject1] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[MCQs] CHECK CONSTRAINT [FK_MCQs_Subject1]
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
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [Questions_fk1] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([SubjectId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [Questions_fk1]
GO
/****** Object:  StoredProcedure [dbo].[insert_subject]    Script Date: 7/1/2023 4:14:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[insert_subject] @subject_name text
AS 
BEGIN
	INSERT INTO Subject (SubjectName)
	VALUES (@subject_name);
END

