using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
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
        SoundEffect _somColisao;
        SpriteFont placarFont;
        Texture2D intro = null;
        Texture2D gameover = null;
        Texture2D gameWinP1 = null;
        Texture2D gameWinP2 = null;
        Texture2D gameWinP3 = null;
        Texture2D gameWinP4 = null;
        Player player0;
        Player player1;
        Player player2;
        Player player3;
        Player player4;
        Song _musica;
        Ball ball;
        MouseState mouseState;
        //Chronometer chronometer;
        int limitRightWidth = 928,limitLeftWidth = 320 ,limitUpHeight = 40,limitDownHeight = 648,limitBallRight = 955 ,limitBallDown = 720,
            state = (int)PongState.PameState.IntroScreen;
        int[] placar = new int[4];//placar[0] jg1, e assim por diante
        float chronometrer = 0, ballSpeedTang = 30;       
        // Pontução máxima do jogo
        const int MaxPontuation = 10;

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
            #region Valor inicial dos placares
            placar[0] = 0; //Jg 1
            placar[1] = 0;
            placar[2] = 0;
            placar[3] = 0;
            #endregion
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _musica = Content.Load<Song>("Sons/Fringe");
            _somColisao = Content.Load<SoundEffect>("Sons/toc_2");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_musica);

            placarFont = Content.Load<SpriteFont>(@"Font");
            intro = Content.Load<Texture2D>("Images\\intro");
            gameover = Content.Load<Texture2D>("Images\\gameover");
            gameWinP1 = Content.Load<Texture2D>("Images\\win1F");
            gameWinP2 = Content.Load<Texture2D>("Images\\win2");
            gameWinP3 = Content.Load<Texture2D>("Images\\win3");
            gameWinP4 = Content.Load<Texture2D>("Images\\win4");
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

            mouseState = Mouse.GetState();
            chronometrer += (float)gameTime.ElapsedGameTime.Seconds;

            switch (state)
            {
                case 0://intro
                    if (Keyboard.GetState().IsKeyDown(Keys.D1))//Se esta pressionado tlecla 1
                    {
                        state = 1;
                        RestartGame();
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D2))//Se esta pressionado tlecla 2
                    {
                        state = 2;
                        RestartGame();
                    }
                    break;
                case 1://single
                case 2://multi
                    if (state == 1)
                    {
                        // Se o jogo for para single player o computador é atualizado automáticamente
                        MoveBastaoComputador();
                    }
                    //Verifica qual jogador ganhou
                    if (placar[0] >= MaxPontuation)
                    {
                        state = 4;
                    }                   
                    if (placar[1] >= MaxPontuation)
                    {
                        state = 5;
                    }
                    if (placar[2] >= MaxPontuation)
                    {
                        state = 6;
                    }
                    // Se o jogador 2 ganhou
                    if (placar[3] >= MaxPontuation)
                    {
                        state = 7;
                    }
                    break;
                case 3:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter)) // Se pressionar ENTER
                    {
                        state = 0;
                    }
                    break;
            }


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
                    ball.Speed = 3;
                }
                if (ball.ballColour == Color.Orange)
                {
                    placar[1] += 1;
                    ball.PositionInitial = new Vector2(640, 320);
                    ball.ballColour = Color.AliceBlue;
                    ball.Speed = 3;
                }
                if (ball.ballColour == Color.Red)
                {
                    placar[2] += 1;
                    ball.PositionInitial = new Vector2(640, 320);
                    ball.ballColour = Color.AliceBlue;
                    ball.Speed = 3;
                }
                if (ball.ballColour == Color.Violet)
                {
                    placar[3] += 1;
                    ball.PositionInitial = new Vector2(640, 320);
                    ball.ballColour = Color.AliceBlue;
                    ball.Speed = 3;
                }
                
            }


            if (player1.frame.Intersects(ball.frame))
            {
                _somColisao.Play();
                ball.ChangeOnTouchBorder(1);
                ball.Speed += 1;
                double angDir = Math.Atan2(ball.frame.Y - player1.frame.Y, ball.frame.X - player1.frame.X);
                //Ok a tang
                ball.PositionEnd = new Vector2((float)Math.Cos(angDir) * ballSpeedTang, (float)Math.Sin(angDir) * ballSpeedTang);
                              
                //ball.Speed += 3;
            }
            if (player2.frame.Intersects(ball.frame))
            {
                _somColisao.Play();
                ball.ChangeOnTouchBorder(1);
                ball.Speed += -1 ;
                double angDir = Math.Atan2(ball.frame.Y - player2.frame.Y, ball.frame.X - player2.frame.X);
                //Errado ver se ta certo
                ball.PositionEnd = new Vector2((float)-Math.Cos(angDir) * ballSpeedTang, (float)Math.Sin(angDir) * ballSpeedTang);

                //ball.Speed = -3;
            }
            if (player3.frame.Intersects(ball.frame))
            {
                _somColisao.Play();
                ball.ChangeOnTouchBorder(1);
                ball.Speed += -1;
                //duvida
                double angDir = Math.Atan2(ball.frame.Y - player3.frame.Y, ball.frame.X - player3.frame.X);
                ball.PositionEnd = new Vector2((float)Math.Cos(angDir) * ballSpeedTang, (float)-Math.Sin(angDir) * ballSpeedTang);
                
                //ball.Speed -= 3;
            }
            if (player4.frame.Intersects(ball.frame))
            {
                _somColisao.Play();
                ball.ChangeOnTouchBorder(0);
                ball.Speed += -1;
                double angDir = Math.Atan2(ball.frame.Y - player4.frame.Y, ball.frame.X - player4.frame.X);
                //ok a tang
                ball.PositionEnd = new Vector2((float)Math.Cos(angDir) * ballSpeedTang, (float)-Math.Sin(angDir) * ballSpeedTang);
                //ball.Speed -= 3;
            }           

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);       

            spriteBatch.Begin();
            switch (state)
            {
                case 0:
                    // Desenha a tela de apresentação (tela inicial do jogo)
                    spriteBatch.Draw(intro, Vector2.Zero, Color.White);
                    break;

                case 1:
                case 2:
                    // Desenha a imagem de fundo
                    background.DrawImage(spriteBatch);
                    player1.DrawImage(spriteBatch);
                    player2.DrawImage(spriteBatch);
                    player3.DrawImage(spriteBatch);
                    player4.DrawImage(spriteBatch);

                    //Placares
                    Vector2 placar1 = placarFont.MeasureString(placar[0].ToString("000"));
                    Vector2 placar2 = placarFont.MeasureString(placar[1].ToString("000"));
                    Vector2 placar3 = placarFont.MeasureString(placar[2].ToString("000"));
                    Vector2 placar4 = placarFont.MeasureString(placar[3].ToString("000"));

                    spriteBatch.DrawString(placarFont, chronometrer.ToString(), new Vector2(1050, 250), Color.White);
                    spriteBatch.DrawString(placarFont, ball.Speed.ToString(), new Vector2(1050, 650), Color.White);
                    spriteBatch.DrawString(placarFont, placar[0].ToString("000"), new Vector2(190, 230), Color.White);
                    spriteBatch.DrawString(placarFont, placar[1].ToString("000"), new Vector2(190, 315), Color.White);
                    spriteBatch.DrawString(placarFont, placar[2].ToString("000"), new Vector2(190, 400), Color.White);
                    spriteBatch.DrawString(placarFont, placar[3].ToString("000"), new Vector2(190, 485), Color.White);

                    #region Muda Cor Bola com o Player
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
                    #endregion

                    break;
                case 3:
                    // Desenha a tela de game over
                    spriteBatch.Draw(gameover, Vector2.Zero, Color.White);
                    break;
                case 4:
                    spriteBatch.Draw(gameWinP1, Vector2.Zero, Color.White);
                    break;
                case 5:
                    spriteBatch.Draw(gameWinP2, Vector2.Zero, Color.White);
                    break;
                case 6:
                    spriteBatch.Draw(gameWinP3, Vector2.Zero, Color.White);
                    break;
                case 7:
                    spriteBatch.Draw(gameWinP4, Vector2.Zero, Color.White);
                    break;
            }
                    
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void RestartGame()
        {
            // Inicializando o objeto bola
            ball.PositionInitial = new Vector2(530, 400);
            // Inicializando o objeto dos Jogadores
            player1.PositionInitial = new Vector2(limitLeftWidth, 200);
            player2.PositionInitial = new Vector2(limitRightWidth, 200);
            player3.PositionInitial = new Vector2(graphics.PreferredBackBufferWidth / 2, limitUpHeight);
            player4.PositionInitial = new Vector2(graphics.PreferredBackBufferWidth / 2, limitDownHeight);
            // Inicializando o score dos jogadores
            placar[0] = 0; //Jg 1
            placar[1] = 0;
            placar[2] = 0;
            placar[3] = 0;
        }
        public void MoveBastaoComputador()
        {
            // Bola indo para direita
            if (ball.PositionInitial.X > limitLeftWidth)
            {
                if (player2.PositionInitial.Y < ball.PositionInitial.Y)
                    player2.PositionInitial = new Vector2(limitRightWidth, 35);
                else if (player2.PositionInitial.Y > ball.PositionInitial.Y)
                    player2.PositionInitial = new Vector2(limitRightWidth, 600);
                else
                    player2.PositionInitial = new Vector2(limitRightWidth, 200);
            }
            else
            {
                player2.PositionInitial = new Vector2(limitRightWidth, 200);
            }
        }
    }
}
