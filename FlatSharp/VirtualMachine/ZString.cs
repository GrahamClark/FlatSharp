using System.Text;

using FlatSharp.Extensions;
using FlatSharp.Types;

namespace FlatSharp.VirtualMachine
{
    public class ZString
    {
        private static string[] _alphabetTable = {
            "_", "?", "?", "?", "?", "?", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j",
            "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
        };

        public static string DisplayBytes(Story story, ZStringAddress address)
        {
            var decoded = new StringBuilder();

            int isEnd;
            var currentAddress = new WordAddress(address.Value);
            do
            {
                var word = story.ReadWord(currentAddress);
                isEnd = word.FetchBits(BitNumber.Fifteen, BitSize.One);
                var zChar1 = word.FetchBits(BitNumber.Fourteen, BitSize.Five);
                var zChar2 = word.FetchBits(BitNumber.Nine, BitSize.Five);
                var zChar3 = word.FetchBits(BitNumber.Four, BitSize.Five);

                decoded.AppendFormat(
                    "{0:x2} {1} {2:x2} {3} {4:x2} {5} ",
                    zChar1,
                    _alphabetTable[zChar1],
                    zChar2,
                    _alphabetTable[zChar2],
                    zChar3,
                    _alphabetTable[zChar3]);

                currentAddress = currentAddress.Increment();
            } while (isEnd != 1);

            return decoded.ToString();
        }
    }
}
