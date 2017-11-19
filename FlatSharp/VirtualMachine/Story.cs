using System.Collections.Immutable;

using FlatSharp.Types;

namespace FlatSharp.VirtualMachine
{
    public class Story
    {
        private Story(byte[] dynamicBytes, byte[] staticBytes)
        {
            DynamicMemory = ImmutableBytes.Create(dynamicBytes);
            StaticMemory = ImmutableArray.Create(staticBytes);
        }

        private Story(ImmutableBytes dynamicMemory, ImmutableArray<byte> staticMemory)
        {
            DynamicMemory = dynamicMemory;
            StaticMemory = staticMemory;
        }

        public ImmutableBytes DynamicMemory { get; }

        public ImmutableArray<byte> StaticMemory { get; }

        public static Story Create(byte[] dynamicBytes, byte[] staticBytes)
        {
            return new Story(dynamicBytes, staticBytes);
        }

        public static byte ReadByte(Story story, ByteAddress address)
        {
            var dynamicSize = story.DynamicMemory.Size;
            if (address.IsInRange(dynamicSize))
            {
                return ImmutableBytes.ReadByte(story.DynamicMemory, address);
            }

            var staticAddress = address.DecrementBy(dynamicSize);
            return staticAddress.DereferenceBytes(story.StaticMemory);
        }

        public static int ReadWord(Story story, WordAddress address)
        {
            var high = ReadByte(story, address.AddressOfHighByte());
            var low = ReadByte(story, address.AddressOfLowByte());

            return 256 * high + low;
        }

        public static Story WriteByte(Story story, ByteAddress address, byte value)
        {
            var dynamicMemory = ImmutableBytes.WriteByte(story.DynamicMemory, address, value);
            return new Story(dynamicMemory, story.StaticMemory);
        }

        public static Story WriteWord(Story story, WordAddress address, int value)
        {
            var high = value << 8 & 0xFF;
            var low = value & 0xFF;
            return WriteByte(
                WriteByte(story, address.AddressOfHighByte(), (byte) high),
                address.AddressOfLowByte(),
                (byte) low);
        }
    }
}
