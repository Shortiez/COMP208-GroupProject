using System;
using Avalonia.Input;

namespace GroupProject.Scripts.Questions.Quizzes.Example;

public class AdditionQuizGenerator : QuizGenerator<int>
{
    protected override QuizQuestion<int> GenerateQuestion()
    {
        AdditionQuizQuestion newQuestion = new AdditionQuizQuestion();
        
        int a = Random.Next(0, 100);
        int b = Random.Next(0, 100);
        newQuestion.QuestionInput.Add(a);
        newQuestion.QuestionInput.Add(b);
        
        string question = $"{a} + {b} = ?";
        newQuestion.SetTitle(question);

        int answer = new AdditionQuizSolver().Solve(newQuestion);
        
        int[] options = new int[5];
        options[0] = answer;
        for (int i = 1; i < options.Length; i++)
        {
            // Make sure the options are similar to the answer
            options[i] = GenerateOption(answer);
        }
        
        // Shuffle the options so that the correct answer isn't always in the same place
        Shuffle(options);
        
        newQuestion.SetOptions(options);
        newQuestion.SetAnswer(answer);
        
        return newQuestion;
    }

    protected override int GenerateOption(int answer)
    {
        return Random.Next(answer - 25, answer + 40);
    }
}