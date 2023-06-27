using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.Graphics.XCF
{
    public static class XCFConstants
    {
        public const int TILE_SIZE = 64;

        public const int PROP_END = 0;
        public const int PROP_COLORMAP = 1;
        public const int PROP_ACTIVE_LAYER = 2;
        public const int PROP_ACTIVE_CHANNEL = 3;
        public const int PROP_SELECTION = 4;
        public const int PROP_FLOATING_SELECTION = 5;
        public const int PROP_OPACITY = 6;
        public const int PROP_MODE = 7;
        public const int PROP_VISIBLE = 8;
        public const int PROP_LINKED = 9;
        public const int PROP_LOCK_ALPHA = 10;
        public const int PROP_APPLY_MASK = 11;
        public const int PROP_EDIT_MASK = 12;
        public const int PROP_SHOW_MASK = 13;
        public const int PROP_SHOW_MASKED = 14;
        public const int PROP_OFFSETS = 15;
        public const int PROP_COLOR = 16;
        public const int PROP_COMPRESSION = 17;
        public const int PROP_GUIDES = 18;
        public const int PROP_RESOLUTION = 19;
        public const int PROP_TATTOO = 20;
        public const int PROP_PARASITES = 21;
        public const int PROP_UNIT = 22;
        public const int PROP_PATHS = 23;
        public const int PROP_USER_UNIT = 24;
        public const int PROP_VECTORS = 25;
        public const int PROP_TEXT_LAYER_FLAGS = 26;
        public const int PROP_OLD_SAMPLE_POINTS = 27;
        public const int PROP_LOCK_CONTENT = 28;
        public const int PROP_GROUP_ITEM = 29;
        public const int PROP_ITEM_PATH = 30;
        public const int PROP_GROUP_ITEM_FLAGS = 31;
        public const int PROP_LOCK_POSITION = 32;
        public const int PROP_FLOAT_OPACITY = 33;
        public const int PROP_COLOR_TAG = 34;
        public const int PROP_COMPOSITE_MODE = 35;
        public const int PROP_COMPOSITE_SPACE = 36;
        public const int PROP_BLEND_SPACE = 37;
        public const int PROP_FLOAT_COLOR = 38;
        public const int PROP_SAMPLE_POINTS = 39;
        public const int PROP_ITEM_SET = 40;
        public const int PROP_ITEM_SET_ITEM = 41;
        public const int PROP_LOCK_VISIBILITY = 42;
        public const int PROP_SELECTED_PATH = 43;

        public const int COMPRESS_NONE = 0;
        public const int COMPRESS_RLE = 1;
        public const int COMPRESS_ZLIB = 2;  /* unused */
        public const int COMPRESS_FRACTAL = 3;  /* unused */


        public const int ORIENTATION_HORIZONTAL = 1;
        public const int ORIENTATION_VERTICAL = 2;

        public const int STROKETYPE_STROKE = 0;
        public const int STROKETYPE_BEZIER_STROKE = 1;


    }
}
