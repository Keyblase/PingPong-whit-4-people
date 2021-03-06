﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Uc6Fabio.Components
{
    public class Player : BaseObjects
    {
        string NamePlayer, life, damage;
        int speed;
        public string NamePlayer1 { get => NamePlayer; set => NamePlayer = value; }
        public string Life1 { get => life; set => life = value; }
        public string Damage1 { get => damage; set => damage = value; }
        public Vector2 PositionInitial;
        public Vector2 PositionEnd { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle frame;
        MouseState MouseState;

        public Player(ContentManager content, Vector2 posIni, string namePlayer)
        {
            NamePlayer1 = namePlayer;

            if(NamePlayer1 == "Nicolas" || NamePlayer1 == "Natanael")
            {
                Texture = content.Load<Texture2D>("Images\\jogador");
            }
            else
            {
                Texture = content.Load<Texture2D>("Images\\jogadorHor");
            }

            PositionInitial = posIni;
            speed = 5;
            PositionEnd = new Vector2(1, 1);
           
        }

        public void MovimentPlayer(GameTime gameTime, Player playerRecebido)
        {
            float TimeExecution = (float)gameTime.ElapsedGameTime.TotalSeconds;//delta time
            MouseState mouseState = Mouse.GetState();
            bool upArrow = false, downArrow = false, rightArrow = false,leftArrow = false;
        
             if(playerRecebido.NamePlayer1 == "Nicolas")
                 {

                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        upArrow = true;
                       playerRecebido.PositionInitial.Y -= speed * 35 * TimeExecution;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        downArrow = true;
                       playerRecebido.PositionInitial.Y += speed * 35 * TimeExecution;
                    }
                    if(upArrow ^ downArrow)
                    {
                       playerRecebido.PositionInitial.Y += speed * 0 * TimeExecution;
                    }
            }

            if (playerRecebido.NamePlayer1 == "Natanael")
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    upArrow = true;
                    playerRecebido.PositionInitial.Y -= speed * 35 * TimeExecution;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    downArrow = true;
                    playerRecebido.PositionInitial.Y += speed * 35 * TimeExecution;
                }
                if (upArrow ^ downArrow)
                {
                    playerRecebido.PositionInitial.Y += speed * 0 * TimeExecution;
                }
            }
            if (playerRecebido.NamePlayer1 == "Ana")
            {
                if (Mouse.GetState().Position.X > 665)
                {
                    rightArrow = true;
                    playerRecebido.PositionInitial.X += speed * 35 * TimeExecution;
                }
                if (Mouse.GetState().Position.X < 665)
                {
                    leftArrow = true;
                    playerRecebido.PositionInitial.X -= speed * 35 * TimeExecution;
                }
                if (leftArrow ^ rightArrow)
                {
                    playerRecebido.PositionInitial.X += speed * 0 * TimeExecution;
                }
            }
            #region Player 3 Teclado Movimentation
            //Backup com teclado
            //if (playerRecebido.NamePlayer1 == "Ana")
            //{
            //    if (Keyboard.GetState().IsKeyDown(Keys.D))
            //    {
            //        rightArrow = true;
            //        playerRecebido.PositionInitial.X += speed * 35 * TimeExecution;
            //    }
            //    if (Keyboard.GetState().IsKeyDown(Keys.A))
            //    {
            //        leftArrow = true;
            //        playerRecebido.PositionInitial.X -= speed * 35 * TimeExecution;
            //    }
            //    if (leftArrow ^ rightArrow)
            //    {
            //        playerRecebido.PositionInitial.X += speed * 0 * TimeExecution;
            //    }
            //}
            #endregion

            if (playerRecebido.NamePlayer1 == "Joao")
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    rightArrow = true;
                    playerRecebido.PositionInitial.X += speed * 35 * TimeExecution;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    leftArrow = true;
                    playerRecebido.PositionInitial.X -= speed * 35 * TimeExecution;
                }
                if (leftArrow ^ rightArrow)
                {
                    playerRecebido.PositionInitial.X += speed * 0 * TimeExecution;
                }
            }           
        }


        public void DrawImage(SpriteBatch sprite)
        {
            if (NamePlayer1 == "Nicolas" )
            {
                frame = new Rectangle((int)PositionInitial.X, (int)PositionInitial.Y, Texture.Width, Texture.Height);
                sprite.Draw(Texture, frame, Color.AliceBlue);
            }
            else if( NamePlayer1 == "Natanael")
            {
                frame = new Rectangle((int)PositionInitial.X, (int)PositionInitial.Y, Texture.Width, Texture.Height);
                sprite.Draw(Texture, frame, Color.Orange);
            }
            else if(NamePlayer1 == "Ana")
            {
                frame = new Rectangle((int)PositionInitial.X, (int)PositionInitial.Y, Texture.Width, Texture.Height);
                sprite.Draw(Texture, frame, Color.Red);
            }
            else
            {
                frame = new Rectangle((int)PositionInitial.X, (int)PositionInitial.Y, Texture.Width, Texture.Height);
                sprite.Draw(Texture, frame, Color.Violet);
            }
        }      
    }
}
