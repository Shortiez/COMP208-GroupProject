using System;
using System.Collections.ObjectModel;

namespace GroupProject.Scripts.Questions;

public class QuizPresenter<T>
{
    public Action OnPresentNewQuestion;
    public Action<bool> OnAnswerSubmitted;
    public QuizQuestion<T> CurrentQuestion { get; private set; }
    public bool AutoPresentNewQuestion { get; set; } = false;
    
    private QuizGenerator<T> _quizGenerator;
    

    public QuizPresenter(QuizGenerator<T> quizGenerator)
    {
        _quizGenerator = quizGenerator;
    }
    
    public QuizPresenter(QuizGenerator<T> quizGenerator, bool autoPresentNewQuestion)
    {
        _quizGenerator = quizGenerator;
        AutoPresentNewQuestion = autoPresentNewQuestion;
    }

    public QuizQuestion<T> PresentNewQuestion()
    {
        CurrentQuestion = _quizGenerator.NewQuestion();
        
        OnPresentNewQuestion?.Invoke();

        return CurrentQuestion;
    }
    
    public void SubmitAnswer(T selectedOption)
    {
        if(selectedOption == null){
            return;
        }

        OnAnswerSubmitted?.Invoke(selectedOption.Equals(CurrentQuestion.Answer));

        if(AutoPresentNewQuestion)
        {
            PresentNewQuestion();
        }
    }
}