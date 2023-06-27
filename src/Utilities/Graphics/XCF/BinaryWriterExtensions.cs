using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.Graphics.XCF
{
    public static class BinaryWriterExtensions
    {

        public static void Write32BE(this System.IO.BinaryWriter writer, uint value)
        {
            // XXX: this could be done better way
            var bytes = BitConverter.GetBytes(value);
            writer.Write(bytes[3]);
            writer.Write(bytes[2]);
            writer.Write(bytes[1]);
            writer.Write(bytes[0]);
        }

        public static void Write32BE(this System.IO.BinaryWriter writer, int value)
        {
            // XXX: this could be done better way

            var bytes = BitConverter.GetBytes(value);
            writer.Write(bytes[3]);
            writer.Write(bytes[2]);
            writer.Write(bytes[1]);
            writer.Write(bytes[0]);
        }


        public static void WriteXCFString(this System.IO.BinaryWriter writer, string value)
        {
            if (value.Length == 0)
            {
                writer.Write32BE(0);
            }
            else
            {
                writer.Write32BE(value.Length + 1);
                writer.Write(Encoding.UTF8.GetBytes(value));
                writer.Write((byte)0);
            }
        }

        public static StreamPointer WritePointer(this System.IO.BinaryWriter writer)
        {
            var ptr = new StreamPointer(writer);
            writer.Write32BE(0);
            return ptr;
        }

    }
}
