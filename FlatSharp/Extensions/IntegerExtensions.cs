using FlatSharp.Types;

namespace FlatSharp.Extensions
{
    public static class IntegerExtensions
    {
        public static int FetchBits(this int word, BitNumber high, BitSize length)
        {
            var mask = ~(-1 << length.Value);
            return word >> (high - length + 1) & mask;
        }

        public static string ToHex(this int number)
        {
            return number.ToString("X");
        }
    }
}
