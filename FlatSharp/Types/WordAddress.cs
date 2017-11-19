namespace FlatSharp.Types
{
    public struct WordAddress
    {
        private const int _wordSize = 2; 

        public WordAddress(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static WordAddress FromAbbreviationTableBase(AbbreviationTableBase tableBase)
        {
            return new WordAddress(tableBase.Value);
        }

        public ByteAddress AddressOfHighByte()
        {
            return new ByteAddress(Value);
        }

        public ByteAddress AddressOfLowByte()
        {
            return new ByteAddress(Value + 1);
        }

        public WordAddress IncrementBy(int offset)
        {
            return new WordAddress(Value + offset * _wordSize);
        }

        public WordAddress Increment()
        {
            return IncrementBy(1);
        }
    }
}
