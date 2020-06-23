using System;
using System.Drawing;

namespace PixelArtVectorize
{
    class EdgeTag
    {
        public bool Visible { get; set; }

        public Color ColorA { get; set; }
        public Color ColorB { get; set; }

        public override string ToString()
        {
            return Convert.ToString(ColorA) + "-" + Convert.ToString(ColorB) + "-" + Visible;
        }

        public EdgeTag(Color color1, Color color2, bool visible1)
        {
            ColorA = color1;
            ColorB = color2;
            Visible = visible1;
        }

        public EdgeTag()
        {
            Visible = true;
        }
    }
}