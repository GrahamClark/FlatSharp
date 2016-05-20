using System.Collections.Immutable;

namespace FlatSharp.VirtualMachine
{
    public class StoryState
    {
        public StoryState(byte[] bytes)
        {
            OriginalBytes = new ImmutableArray<byte>().AddRange(bytes);
            Edits = ImmutableDictionary<int, char>.Empty;
        }

        public ImmutableArray<byte> OriginalBytes { get; }

        public ImmutableDictionary<int, char> Edits { get; }
    }
}
