namespace GroupProject.Scripts.Questions.Quizzes.Combinatorics;

public class CombQuizQuestion : QuizQuestion<int>
{
    public override string QuestionTitle { get; protected set; } = "How many ways are there to select {a} students for a prospectus photograph(order matters) from a group of {b}?";

    public CombQuizQuestion()
    {

    }

    public CombQuizQuestion(string question, int answer, int[] options) : base(question, answer, options)
    {

    }
}