using MIDI_To_Sheet_Music_Converter.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MIDI_To_Sheet_Music_Converter.MIDI_Format
{
    class HeaderChunk
    {
        private static readonly byte[] HEADER_CHUNK_IDENTIFIER = { 0x4d, 0x54, 0x68, 0x64 };
        private const uint CHUNK_SIZE = 0x00000006;

        public const int HEADER_LENGTH = 14;
        public static readonly HeaderChunk Empty = new HeaderChunk(0, 0, 0);

        private const string NOT_ENOUGH_BYTES = "Not enough bytes to parse a header";
        private const string WRONG_CHUNK_IDENTIFIER = "Chunk identifier is invalid";
        private const string WRONG_CHUNK_SIZE = "Chunk size must be 6 to parse correctly";

        private static readonly Dictionary<Func<byte[], bool>, string> HeaderChecks = new Dictionary<Func<byte[], bool>, string>(3) {
            { IsLongEnough, NOT_ENOUGH_BYTES },
            { IsHeaderValid, WRONG_CHUNK_IDENTIFIER },
            { IsChunkSizeCorrect, WRONG_CHUNK_SIZE }
        };

        public ushort Format { get; }

        public uint Tracks { get; }

        public ushort Division { get; }

        public HeaderChunk(ushort format, uint tracks, ushort division)
        {
            Format = format;
            Tracks = tracks;
            Division = division;
        }

        private static bool IsLongEnough(byte[] midiBytes) => midiBytes.Length >= HEADER_LENGTH;
        private static bool IsHeaderValid(byte[] midiBytes) => midiBytes[..4].SequenceEqual(HEADER_CHUNK_IDENTIFIER);
        private static bool IsChunkSizeCorrect(byte[] midiBytes) => Bits.ParseBig.ToUInt32(midiBytes[4..8], 0) == CHUNK_SIZE;

        public static (ReportMessage report, HeaderChunk parsed) FromBytes(byte[] midiBytes)
        {
            var failedCheckMessage = HeaderChecks.FirstOrDefault(pair => pair.Key.Invoke(midiBytes) == false).Value;

            if (failedCheckMessage != null) { 
                return (new ReportMessage(failedCheckMessage), HeaderChunk.Empty); 
            }

            ushort getShort(int offset) => 
                Bits.ParseBig.ToUInt16(midiBytes, offset);

            return (ReportMessage.NoError, new HeaderChunk(getShort(8), getShort(10), getShort(12)));
        }

        public override bool Equals(object obj)
        {
            return obj is HeaderChunk chunk &&
                   Format == chunk.Format &&
                   Tracks == chunk.Tracks &&
                   Division == chunk.Division;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Format, Tracks, Division);
        }
    }
}
