

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


