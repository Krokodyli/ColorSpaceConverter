using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorProfileConverter.Models
{
    public interface ColorConverter
    {
        Color Convert(Color color);
    }
}
