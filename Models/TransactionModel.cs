namespace GroupProject.Models;

public class TransactionModel
{
    public int TransactionNumber;
    public char Variable;
    public char Operation;
    public int ConflictIndex;

    public override string ToString()
    {
        return $"{Operation}{TransactionNumber}({Variable})";
    }
}