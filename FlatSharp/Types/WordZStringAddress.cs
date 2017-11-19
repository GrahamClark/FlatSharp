namespace FlatSharp.Types
{
    public class WordZStringAddress
    {
        public WordZStringAddress(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public ZStringAddress DecodeWordAddress()
        {
            return new ZStringAddress(Value * 2);
        }
    }
}
