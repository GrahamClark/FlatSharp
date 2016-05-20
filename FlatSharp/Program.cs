using System;

namespace FlatSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var word = 0xBEEF;
                Console.WriteLine(((word >> 12) & (~(-1 << 4))).ToString("X"));
                Console.WriteLine(FetchBits(word, 15, 4).ToString("X"));
                Console.WriteLine(word.FetchBits(BitNumber.Fifteen, BitSize.Four).ToHex());

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                Console.ReadKey();
            }
        }

        private static int FetchBits(int word, int high, int length)
        {
            var mask = ~(-1 << length);
            return word >> (high - length + 1) & mask;
        }
    }
}
