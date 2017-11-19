using System;
using System.Collections.Immutable;

using FlatSharp.Types;

namespace FlatSharp.VirtualMachine
{
    public class ImmutableBytes
    {
        private ImmutableBytes(byte[] bytes)
        {
            OriginalBytes = ImmutableArray.Create(bytes);
            Edits = ImmutableDictionary<int, byte>.Empty;
        }

        private ImmutableBytes(
            ImmutableArray<byte> originalBytes,
            ImmutableDictionary<int, byte> edits)
        {
            OriginalBytes = originalBytes;
            Edits = edits;
        }

        public ImmutableArray<byte> OriginalBytes { get; }

        public ImmutableDictionary<int, byte> Edits { get; }

        public int Size => OriginalBytes.Length;

        public static ImmutableBytes Create(byte[] bytes)
        {
            return new ImmutableBytes(bytes);
        }

        public static byte ReadByte(ImmutableBytes bytes, ByteAddress address)
        {
            if (address.IsOutOfRange(bytes.Size))
            {
                throw new InvalidOperationException("out of range");
            }

            if (bytes.Edits.TryGetValue(address.Value, out var recordedEdit))
            {
                return recordedEdit;
            }

            return bytes.OriginalBytes[address.Value];
        }

        public static ImmutableBytes WriteByte(
            ImmutableBytes bytes,
            ByteAddress address,
            byte value)
        {
            if (address.IsOutOfRange(bytes.Size))
            {
                throw new InvalidOperationException("out of range");
            }

            return new ImmutableBytes(bytes.OriginalBytes, bytes.Edits.Add(address.Value, value));
        }
    }
}
