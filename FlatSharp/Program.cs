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
                var story = Story.Load("minizork.z3");
                var version = story.ReadByte(new ByteAddress(0));

                Console.WriteLine(version);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
