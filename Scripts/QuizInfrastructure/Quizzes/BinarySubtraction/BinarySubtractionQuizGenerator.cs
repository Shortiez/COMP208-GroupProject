using System;
using Avalonia.Input;
using Org.BouncyCastle.Asn1.Anssi;

namespace GroupProject.Scripts.Questions.Quizzes.BinarySubtraction;

public class BinarySubtractionQuizGenerator : QuizGenerator<int>
{
    protected override QuizQuestion<int> GenerateQuestion()
    {
        BinarySubtractionQuizQuestion newQuestion = new BinarySubtractionQuizQuestion();

        Random random = new Random();

        int a = random.Next(1, 127);
        int b = random.Next(1, 127);
        if(b > a){
            b = b + a;
            a = b - a;
            b = b - a;
        }

        newQuestion.QuestionInput.Add(a);
        newQuestion.QuestionInput.Add(b);
        
        string question = $"{Convert.ToString(a, 2)} - {Convert.ToString(b, 2)} = ?";
        newQuestion.SetTitle(question);

        int answer = new BinarySubtractionQuizSolver().Solve(newQuestion);
        
        int[] options = new int[5];
        options[0] = decimalToBinary(answer);

        // so options stay within the range 1 to 127, distributed around the answer
        if(answer < 5){
            options[1] = decimalToBinary(answer + random.Next(1, 2));
            options[2] = decimalToBinary(answer + random.Next(3, 4));
            options[3] = decimalToBinary(answer + random.Next(5, 6));
            options[4] = decimalToBinary(answer + random.Next(7, 8));
        }
        else if(answer > 123){
            options[1] = decimalToBinary(answer - random.Next(1, 2));
            options[2] = decimalToBinary(answer - random.Next(3, 4));
            options[3] = decimalToBinary(answer - random.Next(5, 6));
            options[4] = decimalToBinary(answer - random.Next(7, 8));
        }else{
            options[1] = decimalToBinary(answer + random.Next(3, 4));
            options[2] = decimalToBinary(answer + random.Next(1, 2));
            options[3] = decimalToBinary(answer - random.Next(1, 2));
            options[4] = decimalToBinary(answer - random.Next(3, 4));
        }

        // Shuffle the options so that the correct answer isn't always in the same place
        Shuffle(options);
        
        newQuestion.SetOptions(options);
        newQuestion.SetAnswer(decimalToBinary(answer));
        
        return newQuestion;
    }

    protected override int GenerateOption(int answer)
    {
        return -1;
    }

    protected int decimalToBinary(int num){
        return int.Parse(Convert.ToString(num, 2));
    }
}