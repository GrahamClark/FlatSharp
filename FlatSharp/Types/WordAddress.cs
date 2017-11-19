namespace FlatSharp.Types
{
    public struct WordAddress
    {
        public WordAddress(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static implicit operator int(WordAddress address)
        {
            return address.Value;
        }

        public ByteAddress AddressOfHighByte()
        {
            return new ByteAddress(Value);
        }

        public ByteAddress AddressOfLowByte()
        {
            return new ByteAddress(Value + 1);
        }
    }
}
