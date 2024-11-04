using System;
namespace Calculator;

abstract class AstNode
{
	public abstract double Evaluate();
}

class NumberNode : AstNode
{
	public double Value { get; }
	public NumberNode(double value) => Value = value;
	public override double Evaluate() => Value;
}

class BinaryOperationNode : AstNode
{

	public char Operator { get; }
	public AstNode Left { get; }
	public AstNode Right { get; }

	public BinaryOperationNode(char op, AstNode left, AstNode right)
	{
		Operator = op;
		Left = left;
		Right = right;
	}

	public override double Evaluate()
	{
		double leftValue = Left.Evaluate();
		double rightValue = Right.Evaluate();
		return Operator switch
		{
			'+' => leftValue + rightValue,
			'-' => leftValue - rightValue,
			'*' => leftValue * rightValue,
			'/' => leftValue / rightValue
		};
	}

}

