using System.Collections.Generic;

namespace GroupProject.Scripts.Questions;

public abstract class QuizQuestion<T>
{
    // The title of the question
    public virtual string QuestionTitle { get; protected set; } = "What is the answer to?";
    
    // The question itself ie "What is 1 + 1?" would have the question inputs of 1 and 1
    // The question input is the values that are used to generate the question
    // This is for reference only and should be used to verify the questions answer in the solver
    public List<T> QuestionInput { get; } = new List<T>();
    
    // The options for the question
    public T[] Options { get; private set; } = new T[5];
    
    // The answer to the question
    public T Answer { get; private set; } = default;

    /**
     * Default constructor for the QuizQuestion class
     */
    public QuizQuestion()
    {
        
    }
    
    /**
     * Constructor for the QuizQuestion class
     * 
     * @param question The title of the question
     * @param answer The answer to the question
     * @param options The options for the question
     */
    public QuizQuestion(string question, T answer, T[] options)
    {
        QuestionTitle = question;
        Answer = answer;
        Options = options;
    }
    
    public void SetTitle(string title)
    {
        QuestionTitle = title;
    }
    
    public void SetOptions(T[] options)
    {
        Options = options;
    }
    
    public void SetAnswer(T answer)
    {
        Answer = answer;
    }
}