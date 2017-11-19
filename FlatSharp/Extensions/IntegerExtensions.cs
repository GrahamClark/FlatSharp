using FlatSharp.Types;

namespace FlatSharp.Extensions
{
    public static class IntegerExtensions
    {
        public static int FetchBits(this int word, BitNumber high, BitSize length)
        {
            var mask = ~(-1 << length.Value);
            return word >> (high.Value - length.Value + 1) & mask;
        }
    }
}
