using System;
using Avalonia.Input;

namespace GroupProject.Scripts.Questions.Quizzes.Combinatorics;

public class CombQuizGenerator : QuizGenerator<int>
{
    private int answer = 0;
    protected override QuizQuestion<int> GenerateQuestion()
    {
        CombQuizQuestion newQuestion = new CombQuizQuestion();

        //b needs > than a

        int a = Random.Next(2, 5);
        int b = Random.Next(a + 2, 9);
        newQuestion.QuestionInput.Add(a);
        newQuestion.QuestionInput.Add(b);

        string[] questions = new string[5];
        questions[0] = "students for a prospectus photograph";
        questions[1] = "dishes for a dinner party";
        questions[2] = "toppings for a pizza";
        questions[3] = "songs for a playlist";
        questions[4] = "outfits for a trip";

        Random r = new Random();
        int num = r.Next(0,5);

        string question = $"How many ways are there to select {a} " + questions[num] + $" (order matters) from a group of {b}?";

        newQuestion.SetTitle(question);

        answer = new CombQuizSolver().Solve(newQuestion);

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
        int questionOption = Random.Next(3, answer + 20);

        while (questionOption == answer)
        {
            questionOption = Random.Next(3, answer + 20);
        }

        return questionOption;
    }
}