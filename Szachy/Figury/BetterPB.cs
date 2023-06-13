using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Szachy.Figury
{
    public class BetterPB:PictureBox,ICloneable
    {
        public IAFigury aFigura;
        public int x;
        public int y;
        public Color background;

        public object Clone()
        {
            var clone = (BetterPB)this.MemberwiseClone();
            return clone;
        }
    }
}
