namespace MIDI_To_Sheet_Music_Converter.MIDI_Format
{
    class TrackChunk
    {
        public const string HEADER_CHUNK_IDENTIFIER = "MTrk";

        public int Length { get; }

        public ChunkData ChunkData { get; }
    }
}
