using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Uc6Fabio.Components;

namespace Uc6Fabio
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Background background;
        Ball ball;
        Player player0;
        Player player1;
        Player player2;
        Player player3;
        Player player4;
        Song _musica;
        SoundEffect _somColisao;
        SpriteFont placarFont;

        //Chronometer chronometer;
        int limitRightWidth = 928,limitLeftWidth = 320 ,limitUpHeight = 40,limitDownHeight = 648,limitBallRight = 955 ,limitBallDown = 720;
        int[] placar = new int[4];//placar[0] jg1, e assim por diante
            
        int chronometrer = 0;
        string MostrarCronometro;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "PingPong Com a Galera";
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            //Valor inicial dos placares
            placar[0] = 0; //Jg 1
            placar[1] = 0;
            placar[2] = 0;
            placar[3] = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _musica = Content.Load<Song>("Sons/Fringe");
            _somColisao = Content.Load<SoundEffect>("Sons/colisao");

            //Repete a musica
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_musica);

            placarFont = Content.Load<SpriteFont>(@"Font");
            background = new Background(Content, new Vector2(0,0));
            ball = new Ball(Content,new Vector2(530,400));

            player0 = new Player(Content, new Vector2(limitLeftWidth, 200), "Default");
            player1 = new Player(Content, new Vector2(limitLeftWidth, 200), "Nicolas");
            player2 = new Player(Content, new Vector2(limitRightWidth, 200), "Natanael");
            player3 = new Player(Content, new Vector2(graphics.PreferredBackBufferWidth / 2, limitUpHeight), "Ana");
            player4 = new Player(Content, new Vector2(graphics.PreferredBackBufferWidth / 2, limitDownHeight),"Joao");
  
        }

        protected override void UnloadContent()
        {
           
        }

        protected override void Update(GameTime gameTime)
        {
            //Verifica inputs gamepad e keyboard
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            chronometrer += gameTime.ElapsedGameTime.Seconds;
            MostrarCronometro = chronometrer.ToString();
            

            ball.MovimentBall(gameTime);
            //ball.Aceleration(gameTime);

            #region Player 1 Bounds
            if (player1.PositionInitial.Y <= limitUpHeight)
            {
                player1.PositionInitial.Y = limitUpHeight + 2;
            }
            else if (player1.PositionInitial.Y >= 550)
            {
                player1.PositionInitial.Y = 550 - 2;
            }
            else
            {
                player1.MovimentPlayer(gameTime, player1);
            }
            #endregion

            #region Player 2 Bounds
            if (player2.PositionInitial.Y <= limitUpHeight)
            {
                player2.PositionInitial.Y = limitUpHeight + 2;
            }
            else if (player2.PositionInitial.Y >= 550)
            {
                player2.PositionInitial.Y = 550 - 2;
            }
            else
            {
                player2.MovimentPlayer(gameTime, player2);
            }
            #endregion

            #region Player 3 Bounds
            if (player3.PositionInitial.X <= 348)
            {
                player3.PositionInitial.X = 348 + 5;
            }
            else if (player3.PositionInitial.X >= 800)
            {
                player3.PositionInitial.X = 800 - 2;
            }
            else
            {
                player3.MovimentPlayer(gameTime, player3);
            }
            #endregion

            #region Player 4 Bounds
            if (player4.PositionInitial.X <= 348)
            {
                player4.PositionInitial.X = 348 + 5;
            }
            else if (player4.PositionInitial.X >= 800)
            {
                player4.PositionInitial.X = 800 - 2;
            }
            else
            {
                player4.MovimentPlayer(gameTime, player4);
            }
            #endregion
            
            if (ball.PositionInitial.X + ball.frame.Width >= limitBallRight || ball.PositionInitial.X < limitLeftWidth)//arrumar limitBall
            {
                if(ball.ballColour == Color.AliceBlue)
                {
                    ball.PositionInitial = new Vector2(640, 320);
                    ball.ballColour = Color.AliceBlue;
                }
                if (ball.ballColour == Color.Green)
                {
                    placar[0] += 1;
                    ball.PositionInitial = new Vector2(640, 320);
                    ball.ballColour = Color.AliceBlue;
                }
                if (ball.ballColour == Color.Orange)
                {
                    placar[1] += 1;
                    ball.PositionInitial = new Vector2(640, 320);
                    ball.ballColour = Color.AliceBlue;
                }
                if (ball.ballColour == Color.Red)
                {
                    placar[2] += 1;
                    ball.PositionInitial = new Vector2(640, 320);
                    ball.ballColour = Color.AliceBlue;
                }
                if (ball.ballColour == Color.Violet)
                {
                    placar[3] += 1;
                    ball.PositionInitial = new Vector2(640, 320);
                    ball.ballColour = Color.AliceBlue;
                }

            }
            if (ball.PositionInitial.Y + ball.frame.Height >= limitBallDown || ball.PositionInitial.Y < limitUpHeight)//arrumar limitBall
            {
                if (ball.ballColour == Color.AliceBlue)
                {
                    ball.PositionInitial = new Vector2(640, 320);
                    ball.ballColour = Color.AliceBlue;
                }
                if (ball.ballColour == Color.Green)
                {
                    placar[0] += 1;
                    ball.PositionInitial = new Vector2(640, 320);
                    ball.ballColour = Color.AliceBlue;
                }
                if (ball.ballColour == Color.Orange)
                {
                    placar[1] += 1;
                    ball.PositionInitial = new Vector2(640, 320);
                    ball.ballColour = Color.AliceBlue;
                }
                if (ball.ballColour == Color.Red)
                {
                    placar[2] += 1;
                    ball.PositionInitial = new Vector2(640, 320);
                    ball.ballColour = Color.AliceBlue;
                }
                if (ball.ballColour == Color.Violet)
                {
                    placar[3] += 1;
                    ball.PositionInitial = new Vector2(640, 320);
                    ball.ballColour = Color.AliceBlue;
                }
                
            }


            if (player1.frame.Intersects(ball.frame))
            {
                _somColisao.Play();
                ball.ChangeOnTouchBorder(1);
                ball.Speed = 3;
                ball.PositionEnd = new Vector2(20, 10);
                
                //ball.Speed += 3;
            }
            if (player2.frame.Intersects(ball.frame))
            {
                _somColisao.Play();
                ball.ChangeOnTouchBorder(1);
                ball.Speed = -3;
                ball.PositionEnd = new Vector2(10, 20);
                
                //ball.Speed -= 3;
            }
            if (player3.frame.Intersects(ball.frame))
            {
                _somColisao.Play();
                ball.ChangeOnTouchBorder(1);
                ball.Speed = -3;
                ball.PositionEnd = new Vector2(20, -40);
                ball.ChangeColor(spriteBatch, player3);
                
                //ball.Speed -= 3;
            }
            if (player4.frame.Intersects(ball.frame))
            {
                _somColisao.Play();
                ball.ChangeOnTouchBorder(0);
                ball.Speed = -3;
                
                //ball.Speed -= 3;
            }           

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);       

            spriteBatch.Begin();
            background.DrawImage(spriteBatch);

            if (player1.frame.Intersects(ball.frame))
            {
                ball.DrawImage(spriteBatch, Color.Green);
            }
            if (player2.frame.Intersects(ball.frame))
            {
                ball.DrawImage(spriteBatch, Color.Orange);
            }
            if (player3.frame.Intersects(ball.frame))
            {
                ball.DrawImage(spriteBatch, Color.Red);
            }
            if (player4.frame.Intersects(ball.frame))
            {
                ball.DrawImage(spriteBatch, Color.Violet);
            }
            else
            {
                ball.DrawImage(spriteBatch, Color.AliceBlue);
            }        
            player1.DrawImage(spriteBatch);           
            player2.DrawImage(spriteBatch);
            player3.DrawImage(spriteBatch);
            player4.DrawImage(spriteBatch);

            if (player3.frame.Intersects(ball.frame))
            {
                ball.ChangeColor(spriteBatch, player3);
            }

            //Placares
            Vector2 placar1 = placarFont.MeasureString(placar[0].ToString("000"));
            Vector2 placar2 = placarFont.MeasureString(placar[1].ToString("000"));
            Vector2 placar3 = placarFont.MeasureString(placar[2].ToString("000"));
            Vector2 placar4 = placarFont.MeasureString(placar[3].ToString("000"));
            Vector2 chonometrerPlacar = placarFont.MeasureString(MostrarCronometro.ToString());

            //spriteBatch.DrawString(placarFont,(graphics.PreferredBackBufferWidth + "" + ball.PositionInitial.ToString()), new Vector2(400, 400), Color.White);
            //spriteBatch.DrawString(placarFont, (graphics.PreferredBackBufferHeight + " " + ball.PositionInitial.ToString()), new Vector2(400, 600), Color.White);

            base.Draw(gameTime);
            spriteBatch.DrawString(placarFont, MostrarCronometro.ToString(), new Vector2(1050, 250), Color.White);
            spriteBatch.DrawString(placarFont, placar[0].ToString("000"),new Vector2(190, 230),Color.White);
            spriteBatch.DrawString(placarFont, placar[1].ToString("000"), new Vector2(190, 315), Color.White);
            spriteBatch.DrawString(placarFont, placar[2].ToString("000"), new Vector2(190, 400), Color.White);
            spriteBatch.DrawString(placarFont, placar[3].ToString("000"), new Vector2(190, 485), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
