using System;
using Avalonia.Input;

namespace GroupProject.Scripts.Questions.Quizzes.binaryAddition;

public class binaryAdditionQuizGenerator : QuizGenerator<int>
{
    protected override QuizQuestion<int> GenerateQuestion()
    {
        binaryAdditionQuizQuestion newQuestion = new binaryAdditionQuizQuestion();
        
        int a = Random.Next(1, 127);
        int b = Random.Next(1, 127);
        newQuestion.QuestionInput.Add(int.Parse(Convert.ToString(a, 2)));
        newQuestion.QuestionInput.Add(int.Parse(Convert.ToString(b, 2)));
        
        string question = $"{a} + {b} = ?";
        newQuestion.SetTitle(question);

        int answer = new binaryAdditionQuizSolver().Solve(newQuestion);
        
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
        return Random.Next(answer - 25, answer - 26);
    }
}