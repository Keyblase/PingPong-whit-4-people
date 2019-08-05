using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Player player1;
        Player player2;
        Player player3;
        Player player4;
        Chronometer chronometer;
        int limitRightWidth = 928,limitLeftWidth = 320 ,limitUpHeight = 40,limitDownHeight = 648,limitBallRight = 955 ,limitBallDown = 720;
        int[] placar = new int[4];//placar[0] jg1, e assim por diante
        SpriteFont placarFont;        
        bool touchBorder = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            var sprite = Content.Load<Texture2D>("Parado");
            Color[] newColor = new Color[sprite.Width * sprite.Height];
            sprite.GetData(newColor);

            Color newColor1 = Color.AliceBlue;
            sprite.SetData<Color>(newColor);

            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            //Valor inicial dos placares
            placar[0] = 0; //Jg 1
            placar[1] = 1;
            placar[2] = 2;
            placar[3] = 3;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            chronometer = new Chronometer(Services);

            background = new Background(Content, new Vector2(0,0));//
            ball = new Ball(Content,new Vector2(530,400));

            player1 = new Player(Content, new Vector2(limitLeftWidth, 200), "Nicolas");
            placarFont = Content.Load<SpriteFont>(@"Font");

            player2 = new Player(Content, new Vector2(limitRightWidth, 200), "Natanael");

            player3 = new Player(Content, new Vector2(graphics.PreferredBackBufferWidth / 2, limitUpHeight), "Ana");
            player4 = new Player(Content, new Vector2(graphics.PreferredBackBufferWidth / 2, limitDownHeight),"Joao");
                  
            // TODO: use this.Content to load your game content here


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            //Verifica inputs gamepad e keyboard
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }


            chronometer.Update(gameTime);

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
                touchBorder = false;
                ball.PositionInitial = new Vector2(400, 400);
                //ball.ChangeOnTouchBorder(0);
            }
            if (ball.PositionInitial.Y + ball.frame.Height >= limitBallDown || ball.PositionInitial.Y < limitUpHeight)//arrumar limitBall
            {
                touchBorder = false;
                //ball.ChangeOnTouchBorder(1);
                ball.PositionInitial = new Vector2(400, 400);
            }

            //if (ball.Colide(player2) == true)
            //{
            //    ball.Speed = 10;
            //    ball.PositionInitial = new Vector2(player1.PositionInitial.X, player1.PositionInitial.Y);
            //}

            if (player1.frame.Intersects(ball.frame))
            {
                ball.ChangeOnTouchBorder(1);
                ball.Speed = 3;
                ball.PositionEnd = new Vector2(20, 10);
                //ball.Speed += 3;
            }
            if (player2.frame.Intersects(ball.frame))
            {
                ball.ChangeOnTouchBorder(1);
                ball.Speed = -3;
                ball.PositionEnd = new Vector2(10, 20);
                //ball.Speed -= 3;
            }
            if (player3.frame.Intersects(ball.frame))
            {
                ball.ChangeOnTouchBorder(1);
                ball.Speed = -3;
                ball.PositionEnd = new Vector2(20, -40);

                //ball.ChangeColor(spriteBatch, player3);
                //ball.Speed -= 3;
            }
            if (player4.frame.Intersects(ball.frame))
            {
                ball.ChangeOnTouchBorder(1);
                ball.Speed = -3;

                //ball.Speed -= 3;
            }

            //Verifica se houve uma pontuação para o jogador

            //if(player1.PositionInitial.X + player1.Texture.Width > graphics.PreferredBackBufferWidth)
            //{
            //    placar[0] += 1;
            //}
            //
            //if (player2.PositionInitial.X + player2.Texture.Width < 0)
            //{
            //    placar[1] += 1;
            //}
            //
            //if (player3.PositionInitial.X + player3.Texture.Height > graphics.PreferredBackBufferHeight)
            //{
            //    placar[0] += 1;
            //}
            //
            //if (player4.PositionInitial.X + player4.Texture.Height < 0)
            //{
            //    placar[1] += 1;
            //}

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here           

            spriteBatch.Begin();
            chronometer.Draw(spriteBatch, new Vector2(400, 400));
            background.DrawImage(spriteBatch);
            ball.DrawImage(spriteBatch);          
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
            spriteBatch.DrawString(placarFont,(graphics.PreferredBackBufferWidth + "" + ball.PositionInitial.ToString()), new Vector2(400, 400), Color.White);
            spriteBatch.DrawString(placarFont, (graphics.PreferredBackBufferHeight + " " + ball.PositionInitial.ToString()), new Vector2(400, 600), Color.White);
            spriteBatch.DrawString(placarFont, placar[0].ToString("000"),new Vector2(190, 230),Color.White);
            spriteBatch.DrawString(placarFont, placar[1].ToString("000"), new Vector2(190, 315), Color.White);
            spriteBatch.DrawString(placarFont, placar[2].ToString("000"), new Vector2(190, 400), Color.White);
            spriteBatch.DrawString(placarFont, placar[3].ToString("000"), new Vector2(190, 485), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
