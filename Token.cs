using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace Calculator;


public enum TokenType
{
    Number, // NUMBER UNIT
    Operator, // any operator
    EOF // end of an input
}

public readonly record struct Token(
    TokenType Type,
    ReadOnlyMemory<char> Value
);