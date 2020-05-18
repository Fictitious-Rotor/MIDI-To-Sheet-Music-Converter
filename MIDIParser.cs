using MIDI_To_Sheet_Music_Converter.MIDI_Format;
using System;

namespace MIDI_To_Sheet_Music_Converter
{
    class MIDIParser
    {
        /**
         * START
         * If header tag then
         * extract header
         * else return something symbolising a dodgy file (not an exception!)
         * 
         * based on format type, delegate to a parser (have an interface or something)
         * 
         * I'm tempted to have a queue instead of an array so that I can pop items off the top when I pass it around.
         * Likely what I'll do instead is make an immutable array (I hope that's possible [turns out it's not]) and pass the pointer around instead.
         * Should I delegate the parsing to the classes themselves?
         * What I could potentially do is do like a constructor that accepts the bytes or something clever like that.
         * 
         */

        void Parse(byte[] midiBytes)
        {
            (ReportMessage report, HeaderChunk parsed) = HeaderChunk.FromBytes(midiBytes[..HeaderChunk.HEADER_LENGTH]);

            if (report.IsError) { 
                Console.Error.WriteLine("Unable to parse midi header: {0}", report.Message); 
                return;
            }

        }
    }
}
