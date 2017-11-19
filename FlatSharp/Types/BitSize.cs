namespace FlatSharp.Types
{
    public struct BitSize
    {
        private BitSize(int value)
        {
            Value = value;
        }

        public static BitSize One = new BitSize(1);
        public static BitSize Two = new BitSize(2);
        public static BitSize Three = new BitSize(3);
        public static BitSize Four = new BitSize(4);
        public static BitSize Five = new BitSize(5);
        public static BitSize Six = new BitSize(6);
        public static BitSize Seven = new BitSize(7);

        public int Value { get; }

        public static implicit operator int(BitSize bitSize)
        {
            return bitSize.Value;
        }
    }
}
