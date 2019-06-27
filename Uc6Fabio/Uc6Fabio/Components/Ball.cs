﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Uc6Fabio.Components
{
    public class Ball : BaseObjects
    {
        int speed, damage;            
        public Rectangle frame;
        bool isVisible;

        public Vector2 PositionInitial { get ; set; }
        public Vector2 PositionEnd { get; set; }
        public Texture2D Texture { get; set  ; }
        public int Speed { get => speed; set => speed = value; }
        public int Damage { get => damage; set => damage = value; }
        public bool IsVisible { get => isVisible; set => isVisible = value; }

        public float Degree { get; set; }
        public Rectangle AxisRotation { get; set; }
        

        public Ball(ContentManager content , Vector2 posIni)
        {
            Texture = content.Load<Texture2D>("Images\\esfera3d");           
            PositionInitial = posIni;
            speed = 10;
            PositionEnd = new Vector2(35,2);
        }

        #region
         public void MovimentBall(GameTime gameTime)
         {
             float TimeExecution = (float)gameTime.ElapsedGameTime.TotalSeconds;//delta time
                                                                                       
             PositionInitial += speed * PositionEnd * TimeExecution;


         }
        
         public void Aceleration(GameTime gameTime)
         {
             float TimeExecution = (float)gameTime.ElapsedGameTime.TotalSeconds;//delta time
             PositionInitial *= speed * PositionEnd * TimeExecution;
         }

        public void DrawImage(SpriteBatch sprite)
        {
            frame = new Rectangle((int)PositionInitial.X,(int)PositionInitial.Y, 50, 50);
            AxisRotation = new Rectangle(0, 0, Texture.Width, Texture.Height);
            sprite.Draw((Texture), frame, Color.BlueViolet);
            //sprite.Draw(
            //    (Texture),
            //    PositionInitial,
            //    frame, 
            //    Color.BlueViolet,
            //    Degree,
            //    AxisRotation,
            //    );

        }

        public void ChangeOnTouchBorder(int xOuy)
        {
            if(xOuy == 0)
            speed *= -1;
            if (xOuy == 1)
                speed *= 1; //arumar
        }
        
        #endregion
        
        public bool Colide(Player outraSprite)
        {
            Rectangle spriteBox = new Rectangle((int)PositionInitial.X, (int)PositionInitial.Y, Texture.Width, Texture.Height);
            Rectangle outraSpriteBox = new Rectangle((int)outraSprite.PositionInitial.X, (int)outraSprite.PositionInitial.Y, outraSprite.Texture.Width, outraSprite.Texture.Height);

            return spriteBox.Intersects(outraSpriteBox);
        }
    }
}