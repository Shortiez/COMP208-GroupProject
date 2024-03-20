namespace GroupProject.Scripts.Questions;

public abstract class QuizSolver<T>
{
    /***
     * This method should be overridden to solve a question.
     * @param question the question to solve
     * @return the answer to the question
     */
    public abstract T Solve(QuizQuestion<T> question);
}