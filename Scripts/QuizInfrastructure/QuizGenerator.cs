using System;

namespace GroupProject.Scripts.Questions;

/***
 * This class is responsible for generating quiz questions.
 * You should extend this class to generate questions for your specific topic.
 * A quiz question should be a subclass of QuizQuestion.
 * Example:
 * public class BinaryAdditionQuizQuestion : QuizQuestion
 *
 *
 * Help:
 * Any Questions message Ben
 */
public abstract class QuizGenerator<T>
{
    protected static Random Random = new Random();
    
    public QuizQuestion<T> NewQuestion() => GenerateQuestion();

    /***
     * This method should be overridden to generate a new question.
     * @return a new quiz question
     */
    protected abstract QuizQuestion<T> GenerateQuestion();
    
    /***
     * This method should be overridden to generate a new option for a question.
     * @param answer the correct answer to the question
     * @return a new option for the question
     */
    protected abstract T GenerateOption(T answer);
    
    /***
     * This method shuffles the elements of an array.
     * @param array the array to shuffle
     */
    protected void Shuffle(T[] array)
    {
        for (var i = 0; i < array.Length; i++)
        {
            var randomIndex = Random.Next(i, array.Length);
            (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
        }
    }
}