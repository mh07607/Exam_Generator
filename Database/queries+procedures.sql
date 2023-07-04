


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
	select Questions.Content Questions.[Type]
	from Questions, Paper_Question
	where Paper_Question.PaperID =@PaperId and Paper_Question.QuestionID = Questions.QuestionID
end;

Create PROCEDURE GetPastPaperMCQs(@PaperId int)
AS
BEGIN
	select MCQs.Content, MCQs.OptionA, MCQs.OptionB, MCQs.OptionC, MCQs.OptionD
	from MCQS, Paper_MCQ
	where Paper_MCQ.PaperID = @PaperId and Paper_MCQ.MCQID = MCQS.MCQID
end;
