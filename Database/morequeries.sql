/*
CREATE PROCEDURE InsertQuestion (@TopicID int, @Difficulty int, @Type text, @Content nvarchar(max))
AS
BEGIN
	insert into Questions (TopicID, Difficulty, [Type], Content) values (@TopicID, @SubjectID, @Difficulty, @Type, @Content)
END
*/

/*
CREATE PROCEDURE UpdateQuestion (@QuestionId int, @TopicID int, @Difficulty int, @Type text, @Content nvarchar(max))
AS
BEGIN
    UPDATE Question
    SET TopicID = @TopicID , Difficulty = @Difficulty, Type = @Type, Content = @Content
    WHERE QuestionID = @QuestionId;
END
*/

/*
CREATE PROCEDURE InsertMCQ (@Content nvarchar(max), @Difficulty int, @TopicID int, @OptionA nvarchar(max), @OptionB nvarchar(max), @OptionC nvarchar(max), @OptionD nvarchar(max), @Answer varchar)
AS
BEGIN
	insert into MCQs (Content, Difficulty, TopicId, OptionA, OptionB, OptionC, OptionD, Answer) values (@Content, @Difficulty, @TopicID, @OptionA, @OptionB, @OptionC, @OptionD, @Answer)
END
*/

/*
CREATE PROCEDURE UpdateMCQ (@QuestionId int, @Content nvarchar(max), @Difficulty int, @TopicID int, @OptionA nvarchar(max), @OptionB nvarchar(max), @OptionC nvarchar(max), @OptionD nvarchar(max), @Answer varchar)
AS
BEGIN
    UPDATE MCQs
    SET Content = @Content, Difficulty = @Difficulty, TopicID = @TopicID, OptionA = @OptionA, OptionB = @OptionB, OptionC = @OptionC, OptionD = @OptionD, Answer = @Answer
    WHERE QuestionId = @QuestionId;
END
*/

/*
CREATE PROCEDURE InsertTopic (@SubjectId int, @TopicName nvarchar(max))
AS
BEGIN
    insert into Topic (SubjectId, TopicName) values (@SubjectId, @TopicName)
END
*/

/*
CREATE PROCEDURE UpdateTopic (@TopicId int, @SubjectId int, @TopicName nvarchar(max))
AS
BEGIN
    UPDATE Topic
    SET SubjectId = @SubjectId, TopicName = @TopicName
    WHERE TopicId = @TopicId
END
*/

/*
CREATE PROCEDURE InsertSubject (@SubjectName nvarchar(max))
AS
BEGIN
    insert into Subject (SubjectName) values (@SubjectName)
END
*/

select * from Questions where Type = 'long' and TopicID in (select TopicID from Topic Where SubjectId = 1)