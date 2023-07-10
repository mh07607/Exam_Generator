

/* insert in subject */
/*CREATE PROCEDURE insert_subject @subject_name text
AS 
BEGIN
	INSERT INTO Subject (SubjectName)
	VALUES (@subject_name);
END
*/

/*Use these to get a given past papers contents*/
Create PROCEDURE GetPastPaperQuestions(@PaperId int)
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
FROM Questions
WHERE Difficulty = 1 AND [Type] = 'short'
ORDER BY NEWID()*/

/*Selects a given number of random long Questions*/
/*SELECT TOP 2 *
FROM Questions
WHERE Difficulty = 3 AND [Type] = 'long'
ORDER BY NEWID()*/

CREATE PROCEDURE InsertQuestion (@TopicID int, @SubjectID int, @Difficulty int, @Type text, @Content text)
AS
BEGIN
	insert into Questions (TopicID, SubjectID, Difficulty, [Type], Content) values (@TopicID, @SubjectID, @Difficulty, @Type, @Content)
END

CREATE PROCEDURE InsertMCQ (@Content text, @Difficulty int, @ChapterID int, @SubjectID int, @OptionA text, @OptionB text, @OptionC text, @OptionD text, @Answer varchar)
AS
BEGIN
	insert into MCQs (Content, Difficulty, ChapterID, SubjectID, OptionA, OptionB, OptionC, OptionD, Answer) values (@Content, @Difficulty, @ChapterID, @SubjectID, @OptionA, @OptionB, @OptionC, @OptionD, @Answer)
END

/*Should fill more dummy data using ChatGPT and the above procedures, would probably need to show to chatGPT
the Topic and Subject table*/

/*SubjectId should be in Topic not in Questions and MCQs, should change schema*/

/*
EXEC GetRandomMCQs 4, N'سنّتِ نبوی', 5, 3;
*/

