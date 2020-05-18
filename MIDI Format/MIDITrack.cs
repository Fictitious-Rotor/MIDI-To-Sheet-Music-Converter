using System.Collections.Generic;

namespace MIDI_To_Sheet_Music_Converter.MIDI_Format
{
    class MIDITrack
    {
        public HeaderChunk Header { get; }

        public List<TrackChunk> Tracks { get; }
    }
}
