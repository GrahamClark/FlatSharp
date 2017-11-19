using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

using FlatSharp.Types;

namespace FlatSharp.VirtualMachine
{
    public class Story
    {
        private const int _headerSize = 64;

        private static WordAddress _staticMemoryBaseOffset = new WordAddress(14);

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

        public static Story Load(string fileName)
        {
            var file = GetFile(fileName);
            var length = file.Length;

            if (length < _headerSize)
            {
                throw new InvalidOperationException($"{fileName} is not a valid story file");
            }

            var high = _staticMemoryBaseOffset.AddressOfHighByte().DereferenceBytes(file);
            var low = _staticMemoryBaseOffset.AddressOfLowByte().DereferenceBytes(file);
            var dynamicLength = high * 256 + low;

            if (dynamicLength > length)
            {
                throw new InvalidOperationException($"{fileName} is not a valid story file");
            }

            var dynamicMemory = file.Take(dynamicLength).ToArray();
            var staticMemory = file.Skip(dynamicLength).Take(length - dynamicLength).ToArray();

            return new Story(dynamicMemory, staticMemory);
        }

        public byte ReadByte(ByteAddress address)
        {
            var dynamicSize = DynamicMemory.Size;
            if (address.IsInRange(dynamicSize))
            {
                return ImmutableBytes.ReadByte(DynamicMemory, address);
            }

            var staticAddress = address.DecrementBy(dynamicSize);
            return staticAddress.DereferenceBytes(StaticMemory);
        }

        public int ReadWord(WordAddress address)
        {
            var high = ReadByte(address.AddressOfHighByte());
            var low = ReadByte(address.AddressOfLowByte());

            return 256 * high + low;
        }

        public Story WriteByte(ByteAddress address, byte value)
        {
            var dynamicMemory = ImmutableBytes.WriteByte(DynamicMemory, address, value);
            return new Story(dynamicMemory, StaticMemory);
        }

        public Story WriteWord(WordAddress address, int value)
        {
            var high = (value << 8) & 0xFF;
            var low = value & 0xFF;

            return WriteByte(address.AddressOfHighByte(), (byte)high)
                .WriteByte(address.AddressOfLowByte(), (byte)low);
        }

        public AbbreviationTableBase GetAbbreviationsTableBase()
        {
            var offset = new WordAddress(24);
            return new AbbreviationTableBase(ReadWord(offset));
        }

        public ZStringAddress GetAbbreviationZStringAddress(Abbreviation a)
        {
            var baseAddress = WordAddress.FromAbbreviationTableBase(GetAbbreviationsTableBase());
            var abbreviationAddress = baseAddress.IncrementBy(a.Value);
            var wordAddress = new WordZStringAddress(ReadWord(abbreviationAddress));

            return wordAddress.DecodeWordAddress();
        }

        private static byte[] GetFile(string fileName)
        {
            return File.ReadAllBytes(fileName);
        }
    }
}
