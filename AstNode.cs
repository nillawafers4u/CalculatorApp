using System;
namespace Calculator;

// Abstract base record for AST nodes
public abstract record AstNode
{
    public abstract double Evaluate();
}

// Number node as a record
public record NumberNode(double Value) : AstNode
{
    public override double Evaluate() => Value;
}

// Binary operation node as a record
public record BinaryOperationNode(char Operator, AstNode Left, AstNode Right) : AstNode
{
    public override double Evaluate()
    {
        double leftValue = Left.Evaluate();
        double rightValue = Right.Evaluate();

        return Operator switch
        {
            '+' => leftValue + rightValue,
            '-' => leftValue - rightValue,
            '*' => leftValue * rightValue,
            '/' => leftValue / rightValue,
            _ => throw new ArgumentException($"Unsupported operator: {Operator}")
        };
    }
}

