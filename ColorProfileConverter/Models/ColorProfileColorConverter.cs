using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorProfileConverter.Models;

namespace ColorProfileConverter.Models
{
    class ColorProfileColorConverter
    {
        public ColorProfileColorConverter(ColorProfile sourceColorProfile,
                                          ColorProfile targetColorProfile)
        {
        }

        public Color Convert(Color color)
        {
            return Color.FromArgb(255-color.R, 255-color.G, 255-color.B);
        }
    }

}
