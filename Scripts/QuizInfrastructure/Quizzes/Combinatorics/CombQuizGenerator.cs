using System;
using Avalonia.Input;

namespace GroupProject.Scripts.Questions.Quizzes.Combinatorics;

public class CombQuizGenerator : QuizGenerator<int>
{
    protected override QuizQuestion<int> GenerateQuestion()
    {
        CombQuizQuestion newQuestion = new CombQuizQuestion();

        //b needs > than a

        int a = Random.Next(1, 4);
        int b = Random.Next(a + 1, 8);
        newQuestion.QuestionInput.Add(a);
        newQuestion.QuestionInput.Add(b);

        string question = $"How many ways are there to select {a} students for a prospectus photograph(order matters) from a group of {b}?";
        newQuestion.SetTitle(question);

        int answer = new CombQuizSolver().Solve(newQuestion);

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