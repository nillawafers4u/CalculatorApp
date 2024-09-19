using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Token
    {
        private enum TokenType
        {
            Nothing,
            Number,
            Operator,

        };

        // For all tokens
        TokenType tokenType = TokenType.Nothing;

        // If it's a number
        double value = 0.0;

        //If it's an operator
        char symbol = ' ';

        private Token() { }

        public static Token StringToToken(string str)
        {
            Token t = new Token();

            if (double.TryParse(str, out t.value))
                t.tokenType = TokenType.Number;
            else
            {
                switch (str)
                {
                    case "+":
                        t.tokenType = TokenType.Operator;
                        t.symbol = '+';
                        break;

                    case "-":
                        t.tokenType = TokenType.Operator;
                        t.symbol = '-';
                        break;

                    case "*":
                        t.tokenType = TokenType.Operator;
                        t.symbol = '*';
                        break;

                    case "/":
                        t.tokenType = TokenType.Operator;
                        t.symbol = '/';
                        break;

                    default:
                        t.tokenType = TokenType.Nothing;
                        break;
                }
            }
            return t;
        }


    }
}
