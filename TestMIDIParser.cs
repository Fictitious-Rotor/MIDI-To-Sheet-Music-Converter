using MIDI_To_Sheet_Music_Converter.MIDI_Format;
using MIDI_To_Sheet_Music_Converter.Util;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MIDI_To_Sheet_Music_Converter
{
    public class Tests
    {
        ushort ToUShortHigh(uint source) => (ushort)(source >> 16);
        ushort ToUShortLow(uint source) => (ushort)(source & 0xffff);

        byte[] ToMidiHeaderBytes(uint chunkId, uint chunkSize, ushort formatType, ushort trackCount, ushort timeDivision)
        {
            return new List<ushort> {
                ToUShortHigh(chunkId),
                ToUShortLow(chunkId),
                ToUShortHigh(chunkSize),
                ToUShortLow(chunkSize),
                formatType,
                trackCount,
                timeDivision
            }.SelectMany(Bits.ParseBig.GetBytes).ToArray();
        }

        [SetUp] public void Setup() { }

        [Test]
        public void ValidMIDIHeaderParsed()
        {
            uint chunkId = 0x4D_54_68_64;
            uint chunkSize = 0x00_00_00_06;
            ushort formatType = 0x00_00;
            ushort trackCount = 0x00_01;
            ushort timeDivision = 0x00_80;

            byte[] headerBytes = ToMidiHeaderBytes(chunkId, chunkSize, formatType, trackCount, timeDivision);
            (ReportMessage report, HeaderChunk parsed) = HeaderChunk.FromBytes(headerBytes);

            Assert.IsFalse(report.IsError, "Header chunk parsing error: '{0}'", report.Message);
            Assert.AreEqual(new HeaderChunk(0, 1, 128), parsed);
        }

        [Test]
        public void MIDILoadedFromFile()
        {
            Assert.Pass();
        }

        [Test]
        public void MIDILoadedFromEmptyFile()
        {
            Assert.Pass();
        }
    }
}