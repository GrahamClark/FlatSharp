namespace FlatSharp.Types
{
    public struct AbbreviationTableBase
    {
        public AbbreviationTableBase(int number)
        {
            Value = number;
        }

        public int Value { get; }
    }
}
