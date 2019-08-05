using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uc6Fabio.Components
{
    class Background
    {
        public Rectangle frame;

        public Vector2 PositionInitial { get; set; }
        public Texture2D Texture { get; set; }

        public Background(ContentManager content, Vector2 posIni)
        {
            Texture = content.Load<Texture2D>("Images\\PingPong");
            PositionInitial = posIni;
        }

        #region

        public void DrawImage(SpriteBatch sprite)
        {
            frame = new Rectangle((int)PositionInitial.X, (int)PositionInitial.Y, 1280, 720);
            sprite.Draw((Texture), frame, Color.White);

        }
        #endregion
    }
}
