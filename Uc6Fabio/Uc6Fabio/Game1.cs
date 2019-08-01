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
        Ball ball;
        Player player1;
        Player player2;
        Player player3;
        Player player4;

        SpriteFont placarFont;
        int[] placar = new int[4];//placar[0] jg1, e assim por diante
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

            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 800;
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

            ball = new Ball(Content,new Vector2(730,1));

            player1 = new Player(Content, Vector2.One, "Nicolas");
            placarFont = Content.Load<SpriteFont>(@"Font");

            player2 = new Player(Content, new Vector2(990, 200), "Natanael");

            player3 = new Player(Content, new Vector2(graphics.PreferredBackBufferWidth / 2, 0), "Ana");
            player4 = new Player(Content, new Vector2(graphics.PreferredBackBufferWidth / 2, 700),"Joao");
                  
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

            
            ball.MovimentBall(gameTime);
            //ball.Aceleration(gameTime);

            player1.MovimentPlayer(gameTime,player1);
            player2.MovimentPlayer(gameTime,player2);

            int xOuy;

            if (ball.PositionInitial.X + ball.frame.Width 
                >= graphics.PreferredBackBufferWidth || ball.PositionInitial.X <= 0)
            {
                xOuy = 0;
                touchBorder = false;
                ball.ChangeOnTouchBorder(xOuy);
            }
            if (ball.PositionInitial.Y + ball.frame.Height
                >= graphics.PreferredBackBufferHeight || ball.PositionInitial.Y <= 0)
            {
                xOuy = 1;
                touchBorder = false;
                ball.ChangeOnTouchBorder(xOuy);
            }

            //if (ball.Colide(player2) == true)
            //{
            //    ball.Speed = 10;
            //    ball.PositionInitial = new Vector2(player1.PositionInitial.X, player1.PositionInitial.Y);
            //}

            if (player2.frame.Intersects(ball.frame))
            {
                ball.Speed += -10;
            }

            if (player1.frame.Intersects(ball.frame))
            {
                ball.Speed += 20;
            }
            
            //Verifica se houve uma pontuação para o jogador

            if(player1.PositionInitial.X + player1.Texture.Width > graphics.PreferredBackBufferWidth)
            {
                placar[0] += 1;
            }

            if (player2.PositionInitial.X + player2.Texture.Width < 0)
            {
                placar[1] += 1;
            }

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
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here           

            spriteBatch.Begin();
            ball.DrawImage(spriteBatch);          
            player1.DrawImage(spriteBatch);           
            player2.DrawImage(spriteBatch);
            player3.DrawImage(spriteBatch);
            player4.DrawImage(spriteBatch);

            //Placar 1
            Vector2 placar1 = placarFont.MeasureString(placar[0].ToString("000"));
            Vector2 placar2 = placarFont.MeasureString(placar[1].ToString("000"));
            Vector2 placar3 = placarFont.MeasureString(placar[2].ToString("000"));
            Vector2 placar4 = placarFont.MeasureString(placar[3].ToString("000"));

            spriteBatch.DrawString(placarFont, placar[0].ToString("P1: " + "000"),new Vector2(0, 730),Color.White);
            spriteBatch.DrawString(placarFont, placar[1].ToString("P2: " + "000"), new Vector2(300, 730), Color.White);
            spriteBatch.DrawString(placarFont, placar[2].ToString("P3: " + "000"), new Vector2(600, 730), Color.White);
            spriteBatch.DrawString(placarFont, placar[3].ToString("P4: " + "000"), new Vector2(870, 730), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
