using System;
using System.Collections.Immutable;

namespace FlatSharp.Types
{
    public struct ByteAddress
    {
        public ByteAddress(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static implicit operator int(ByteAddress address)
        {
            return address.Value;
        }

        public bool IsInRange(int size)
        {
            return 0 <= this && this < size;
        }

        public bool IsOutOfRange(int size)
        {
            return !IsInRange(size);
        }

        public ByteAddress IncrementBy(int offset)
        {
            return new ByteAddress(Value + offset);
        }

        public ByteAddress DecrementBy(int offset)
        {
            return IncrementBy(0 - offset);
        }

        public byte DereferenceBytes(ImmutableArray<byte> bytes)
        {
            if (IsOutOfRange(bytes.Length))
            {
                throw new InvalidOperationException("out of range");
            }

            return bytes[Value];
        }

        public byte DereferenceBytes(byte[] bytes)
        {
            if (IsOutOfRange(bytes.Length))
            {
                throw new InvalidOperationException("out of range");
            }

            return bytes[Value];
        }
    }
}
