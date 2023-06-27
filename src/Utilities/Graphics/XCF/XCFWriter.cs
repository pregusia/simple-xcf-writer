using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.Graphics.XCF
{
    /// <summary>
    /// Allow to write simple, uncompressed!! XCF RGBA file
    /// </summary>
    public class XCFWriter
    {


        public List<Layer> Layers { get; private set; }

        public int CanvasWidth { get; private set; }
        public int CanvasHeight { get; private set; }

        public System.Drawing.Size CanvasSize
        {
            get { return new System.Drawing.Size(this.CanvasWidth, this.CanvasHeight); }
        }


        public XCFWriter()
        {
            this.Layers = new List<Layer>();
            this.CanvasHeight = 0;
            this.CanvasWidth = 0;
        }

        /// <summary>
        /// Adds new layer to output file
        /// </summary>
        /// <param name="name">Name of layer</param>
        /// <param name="image">Image of layer; Not going to be disposed or anything</param>
        public void AddLayer(string name, System.Drawing.Bitmap bitmap)
        {
            this.Layers.Add(new Layer(name, bitmap));

            this.CanvasWidth = Math.Max(this.CanvasWidth, bitmap.Width);
            this.CanvasHeight = Math.Max(this.CanvasHeight, bitmap.Height);
        }
        
        /// <summary>
        /// Writes XCF data to file
        /// </summary>
        /// <param name="path"></param>
        public void WriteToFile(string path)
        {
            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Create))
            {
                this.WriteToStream(stream);
            }
        }

        /// <summary>
        /// Writes XCF data to given stream
        /// Stream must be seekable
        /// </summary>
        /// <param name="stream"></param>
        public void WriteToStream(System.IO.Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("Null stream");
            using (var writer = new System.IO.BinaryWriter(stream))
            {
                this.WriteToWriter(writer);
            }
        }

        /// <summary>
        /// Writes XCF data to writer
        /// Underlying stream must be seekable
        /// </summary>
        /// <param name="writer"></param>
        public void WriteToWriter(System.IO.BinaryWriter writer)
        {
            if (writer == null) throw new ArgumentNullException("Null writer");

            writer.Write(Encoding.ASCII.GetBytes("gimp xcf "));
            writer.Write(Encoding.ASCII.GetBytes("v003"));
            writer.Write((byte)0);

            // canvas width+heiht
            writer.Write32BE(this.CanvasWidth);
            writer.Write32BE(this.CanvasHeight);

            writer.Write32BE(0); // RGB color

            // image properties
            if (true)
            {
                // prop compression
                writer.Write32BE(XCFConstants.PROP_COMPRESSION);
                writer.Write32BE(1);
                writer.Write((byte)XCFConstants.COMPRESS_NONE);

                // props end
                writer.Write32BE(XCFConstants.PROP_END);
                writer.Write32BE(0);
            }

            StreamPointer[] layersPointers = new StreamPointer[this.Layers.Count];
            for (int i = 0; i < this.Layers.Count; ++i)
            {
                layersPointers[i] = writer.WritePointer();
            }

            writer.Write32BE(0); // layers end
            writer.Write32BE(0); // channels end
            writer.Write32BE(0); // paths end

            for (int i = 0; i < this.Layers.Count; ++i)
            {
                layersPointers[i].WriteCurrent();
                this.WriteLayer(writer, this.Layers[i]);
            }

        }

        private void WriteLayer(System.IO.BinaryWriter writer, Layer layer)
        {

            // width + height
            writer.Write32BE(layer.Bitmap.Width);
            writer.Write32BE(layer.Bitmap.Height);

            writer.Write32BE(1); // color mode (rgb + alpha)
            writer.WriteXCFString(layer.Name);

            // props
            if (true)
            {
                // prop for mode
                writer.Write32BE(XCFConstants.PROP_MODE);
                writer.Write32BE(4);
                writer.Write32BE(0); // normal

                // prop for offset
                writer.Write32BE(XCFConstants.PROP_OFFSETS);
                writer.Write32BE(8);
                writer.Write32BE(0);
                writer.Write32BE(0);

                // props end
                writer.Write32BE(XCFConstants.PROP_END);
                writer.Write32BE(0);
            }

            var hptr = writer.WritePointer();
            writer.Write32BE(0); // mask pointer

            // now the h structure
            hptr.WriteCurrent();
            WriteHStructures(writer, layer.Bitmap);
        }

        private void WriteHStructures(System.IO.BinaryWriter writer, System.Drawing.Bitmap bitmap)
        {
            // width + height
            writer.Write32BE(bitmap.Width);
            writer.Write32BE(bitmap.Height);
            writer.Write32BE(4); // color mode (rgb + alpha)

            var lptr = writer.WritePointer();
            writer.Write32BE(0); // ptr end

            // now level info
            lptr.WriteCurrent();

            writer.Write32BE(bitmap.Width);
            writer.Write32BE(bitmap.Height);


            int tilesWidth = (int)Math.Ceiling((float)bitmap.Width / (float)XCFConstants.TILE_SIZE);
            int tilesHeight = (int)Math.Ceiling((float)bitmap.Height / (float)XCFConstants.TILE_SIZE);

            StreamPointer[] tilesPointers = new StreamPointer[tilesWidth * tilesHeight];

            for (int i = 0; i < tilesWidth * tilesHeight; ++i)
            {
                tilesPointers[i] = writer.WritePointer();
            }

            writer.Write32BE(0); // tiles pointers end

            // now tiles
            int ptrIdx = 0;
            for (int tileY = 0; tileY < tilesHeight; ++tileY)
            {
                for (int tileX = 0; tileX < tilesWidth; ++tileX)
                {
                    var ptr = tilesPointers[ptrIdx++];

                    ptr.WriteCurrent();
                    for (int ry = 0; ry < XCFConstants.TILE_SIZE; ++ry)
                    {
                        for (int rx = 0; rx < XCFConstants.TILE_SIZE; ++rx)
                        {
                            int imgX = tileX * XCFConstants.TILE_SIZE + rx;
                            int imgY = tileY * XCFConstants.TILE_SIZE + ry;

                            if (imgX < bitmap.Width && imgY < bitmap.Height)
                            {
                                var color = bitmap.GetPixel(imgX, imgY);
                                writer.Write((byte)color.R);
                                writer.Write((byte)color.G);
                                writer.Write((byte)color.B);
                                writer.Write((byte)color.A);
                            }
                        }
                    }

                }

            }
        }

    }
}
