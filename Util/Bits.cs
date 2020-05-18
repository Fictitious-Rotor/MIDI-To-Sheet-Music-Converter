using MiscUtil.Conversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace MIDI_To_Sheet_Music_Converter.Util
{
    static class Bits
    {
        public static BigEndianBitConverter ParseBig { get; } = new BigEndianBitConverter();

        public static LittleEndianBitConverter ParseLittle { get; } = new LittleEndianBitConverter();
    }
}
