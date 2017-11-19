namespace FlatSharp.Types
{
    public struct BitNumber
    {
        private BitNumber(int value)
        {
            Value = value;
        }

        public static BitNumber Zero = new BitNumber(0);
        public static BitNumber One = new BitNumber(1);
        public static BitNumber Two = new BitNumber(2);
        public static BitNumber Three = new BitNumber(3);
        public static BitNumber Four = new BitNumber(4);
        public static BitNumber Five = new BitNumber(5);
        public static BitNumber Six = new BitNumber(6);
        public static BitNumber Seven = new BitNumber(7);
        public static BitNumber Eight = new BitNumber(8);
        public static BitNumber Nine = new BitNumber(9);
        public static BitNumber Ten = new BitNumber(10);
        public static BitNumber Eleven = new BitNumber(11);
        public static BitNumber Twelve = new BitNumber(12);
        public static BitNumber Thirteen = new BitNumber(13);
        public static BitNumber Fourteen = new BitNumber(14);
        public static BitNumber Fifteen = new BitNumber(15);

        public int Value { get; }
    }
}
