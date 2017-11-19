using System;

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

                var zString = story.GetAbbreviationZStringAddress(new Abbreviation(0));
                var text = ZString.DisplayBytes(story, zString);
                Console.WriteLine(text);

                zString = story.GetAbbreviationZStringAddress(new Abbreviation(4));
                text = ZString.DisplayBytes(story, zString);
                Console.WriteLine(text);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
