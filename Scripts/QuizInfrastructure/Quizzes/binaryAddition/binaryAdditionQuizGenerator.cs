using System;
using Avalonia.Input;

namespace GroupProject.Scripts.Questions.Quizzes.binaryAddition;

public class binaryAdditionQuizGenerator : QuizGenerator<int>
{
    protected override QuizQuestion<int> GenerateQuestion()
    {
        binaryAdditionQuizQuestion newQuestion = new binaryAdditionQuizQuestion();

        Random random = new Random();

        int a = random.Next(1, 127);
        int b = random.Next(1, 127);
        newQuestion.QuestionInput.Add(a);
        newQuestion.QuestionInput.Add(b);
        
        string question = $"{Convert.ToString(a, 2)} + {Convert.ToString(b, 2)} = ?";
        newQuestion.SetTitle(question);

        int answer = new binaryAdditionQuizSolver().Solve(newQuestion);
        
        int[] options = new int[5];
        options[0] = int.Parse(Convert.ToString(answer, 2));
        for (int i = 1; i < options.Length; i++)
        {
            // Make sure the options are similar to the answer
            options[i] = int.Parse(Convert.ToString(GenerateOption(answer), 2));
        }
        
        // Shuffle the options so that the correct answer isn't always in the same place
        Shuffle(options);
        
        newQuestion.SetOptions(options);
        newQuestion.SetAnswer(answer);
        
        return newQuestion;
    }

    protected override int GenerateOption(int answer)
    {
        // generate options in index 1, 2, 3, 4
        return answer + Random.Next(answer - 5, answer + 5);
    }
}