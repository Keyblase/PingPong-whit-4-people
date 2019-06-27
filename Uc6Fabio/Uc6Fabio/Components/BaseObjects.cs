using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Uc6Fabio.Components
{
    public interface BaseObjects
    {
               
        Vector2 PositionEnd { get; set; }
        Texture2D Texture { get; set; }
        
        

    }
}
