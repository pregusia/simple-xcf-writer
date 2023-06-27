using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.Graphics.XCF
{
    /// <summary>
    /// Utility class for holding pointer to some place in stream
    /// </summary>
    public class StreamPointer
    {
        public System.IO.BinaryWriter Writer { get; private set; }
        public int Position { get; private set; }

        public StreamPointer(System.IO.BinaryWriter writer)
        {
            this.Writer = writer;
            this.Position = (int)writer.BaseStream.Position;
        }

        /// <summary>
        /// Writes back current stream position to pointed location
        /// </summary>
        public void WriteCurrent()
        {
            this.Writer.Flush();
            var curr = (int)this.Writer.BaseStream.Position;
            this.Writer.Seek(this.Position, System.IO.SeekOrigin.Begin);
            this.Writer.Write32BE(curr);
            this.Writer.Flush();
            this.Writer.Seek(curr, System.IO.SeekOrigin.Begin);
            this.Writer.Flush();
        }
    }
}
