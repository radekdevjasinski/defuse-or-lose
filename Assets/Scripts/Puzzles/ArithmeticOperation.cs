using System;

namespace DefuseOrLose
{
    public readonly struct ArithmeticOperation
    {
        private readonly char symbol;
        private readonly int operand;

        public ArithmeticOperation(char symbol, int operand)
        {
            this.symbol = symbol;
            this.operand = operand;
        }

        public float Apply(float value)
        {
            switch (symbol)
            {
                case '+':
                    return value + operand;
                case '-':
                    return value - operand;
                case '*':
                    return value * operand;
                case '/':
                    return value / operand;
                default:
                    throw new InvalidOperationException($"Unknown arithmetic operator '{symbol}'.");
            }
        }
    }
}
