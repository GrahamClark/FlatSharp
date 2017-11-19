using System;
using System.Text;

using FlatSharp.Types;
using FlatSharp.VirtualMachine;

namespace FlatSharp
{
    class Program
    {
        static void Main()
        {
            try
            {
                var address1 = new ByteAddress(1);
                var bytesA = ImmutableBytes.Create(Encoding.ASCII.GetBytes("Hello world"));
                var bytesB = ImmutableBytes.WriteByte(bytesA, address1, 65);
                var firstA = ImmutableBytes.ReadByte(bytesA, address1);
                var firstB = ImmutableBytes.ReadByte(bytesB, address1);

                Console.WriteLine($"{firstA} {firstB}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
