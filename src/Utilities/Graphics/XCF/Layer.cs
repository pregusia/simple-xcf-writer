using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.Graphics.XCF
{
    /// <summary>
    /// Layer in XCF file
    /// Holds name and bitmap
    /// </summary>
    public class Layer
    {
        /// <summary>
        /// Name of layer
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Bitmap of layer
        /// </summary>
        public System.Drawing.Bitmap Bitmap { get; private set; }

        public Layer(string name, System.Drawing.Bitmap bitmap)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Empty layer name");
            if (bitmap == null) throw new ArgumentNullException("Null bitmap");

            this.Name = name;
            this.Bitmap = bitmap;
        }
    }
}
