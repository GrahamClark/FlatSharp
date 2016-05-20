namespace FlatSharp.Types
{
    public class ByteAddress
    {
        private ByteAddress(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static implicit operator int(ByteAddress address)
        {
            return address.Value;
        }

        public bool IsInRange(int size)
        {
            return 0 <= this && this < size;
        }

        public bool IsOutOfRange(int size)
        {
            return !IsInRange(size);
        }
    }
}
