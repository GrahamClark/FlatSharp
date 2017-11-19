using System;

namespace FlatSharp.Types
{
    public struct Abbreviation
    {
        private const int _minValue = 0;
        private const int _maxValue = 95;

        public Abbreviation(int number)
        {
            if (number < _minValue || number > _maxValue)
            {
                throw new InvalidOperationException(
                    $"Abbreviation must be between {_minValue} and {_maxValue}");
            }

            Value = number;
        }
        
        public int Value { get; }
    }
}
