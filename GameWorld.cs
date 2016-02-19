using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace MummyDispair
{
    enum State
    {
        Hover,
        Up,
        Released,
        Down
    }
    
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class GameWorld : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        private SoundEffectInstance musicEngine;

        private SpriteFont font;

        /// <summary>
        /// Credits: http:// www.alecjacobsen.com/weblog/?p=539.
        /// This menu has been made with inspiration from the link above.
        /// </summary>

        //Only menu related
        private Texture2D backgroundTexture;

        //decides the number of buttons 
        const int numberOfButtons = 3,
        //Button indexes
        playButtonIndex = 0,
        helpButtonIndex = 1,
        quitButtonIndex = 2,
        buttonHeight = 50,
        buttonWidth = 100;

        private Color backgroundColor;
        private Color[] buttonColor = new Color[numberOfButtons];
        private Rectangle[] buttonRectangle = new Rectangle[numberOfButtons];
        private State[] buttonState = new State[numberOfButtons];
        private Texture2D[] buttonTexture = new Texture2D[numberOfButtons];
        private double[] buttonTimer = new double[numberOfButtons];
        //mouse pressed and mouse just pressed
        private bool mousePressed, mousePressedNow = false;
        //The mouse's location in the screen window
        private int mouseX, mouseY;

        private bool gameStarted;

        //Others
        private PlayerCamera camera;

        public int ScreenWidth { get; } = 780;
        public int ScreenHeight { get; } = 500;

        public List<Collider> Colliders { get; } = new List<Collider>();
        public List<GameObject> Objects { get; } = new List<GameObject>();

        private LevelCreator levelCreator;
        private bool nextLevel;
        private bool youWon;
        private bool youDied;

        private float deltaTime;

        public float DeltaTime
        {
            get
            {
                return deltaTime;
            }
        }

        //Singleton.
        private static GameWorld instance;

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        private GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            gameStarted = false;
            nextLevel = false;
            youWon = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            font = Content.Load<SpriteFont>("WinScreen");

            SoundEffect effect = Content.Load<SoundEffect>("Sound/PharaohMusic");
            musicEngine = effect.CreateInstance();
            musicEngine.IsLooped = true;
            musicEngine.Volume = 0.4f;
            musicEngine.Play();

            if (!gameStarted)
            {
                // TODO: Add your initialization logic here
                graphics.PreferredBackBufferWidth = ScreenWidth;  // set this value to the desired width of your window
                graphics.PreferredBackBufferHeight = ScreenHeight;   // set this value to the desired height of your window
                graphics.ApplyChanges();

                // starting x and y locations to stack buttons 
                // vertically in the middle of the screen
                int x = Window.ClientBounds.Width / 2 - buttonWidth / 2;
                int y = Window.ClientBounds.Height / 2 -
                    numberOfButtons / 2 * buttonHeight -
                    (numberOfButtons % 2) * buttonHeight / 2;

                for (int i = 0; i < numberOfButtons; i++)
                {
                    buttonState[i] = State.Up;
                    buttonColor[i] = Color.White;
                    buttonTimer[i] = 0.0;
                    buttonRectangle[i] = new Rectangle(x, y, buttonWidth, buttonHeight);
                    y += buttonHeight;
                }
                IsMouseVisible = true;
                backgroundColor = Color.Gray;
            }

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

            if (!gameStarted)
            {
                // TODO: use this.Content to load your game content here
                buttonTexture[playButtonIndex] =
                Content.Load<Texture2D>("images/ButtonPlay1.png");
                buttonTexture[helpButtonIndex] =
                Content.Load<Texture2D>("images/ButtonHelp1.png");
                buttonTexture[quitButtonIndex] =
                Content.Load<Texture2D>("images/ButtonQuit1.png");

                //Background Textures
                backgroundTexture = Content.Load<Texture2D>("Textures/background image.png");
            }

            else
            {
                levelCreator = new LevelCreator(Content);
                List<GameObject> creatorObjects = levelCreator.AddToList();
                GameObject player = null;

                for (int i = 0; i < creatorObjects.Count; i++)
                {
                    Objects.Add(creatorObjects[i]);

                    if (creatorObjects[i].TypeComponent is Player)
                    {
                        player = creatorObjects[i];
                    }
                }

                camera = new PlayerCamera(player);
            }
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gameStarted = false;
                nextLevel = false;
                youWon = false;
                youDied = false;

                Objects.Clear();
                Colliders.Clear();
                LoadContent();
            }
                

            // TODO: Add your update logic here
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Should only be run once.
            if (nextLevel)
            {
                List<GameObject> creatorObjects = levelCreator.NextLevel();

                for (int i = 0; i < creatorObjects.Count; i++)
                {
                    Objects.Add(creatorObjects[i]);
                }

                nextLevel = false;
            }

            if (!gameStarted)
            {
                // update mouse variables
                MouseState mouseState = Mouse.GetState();
                mouseX = mouseState.X;
                mouseY = mouseState.Y;
                mousePressedNow = mousePressed;
                mousePressed = mouseState.LeftButton == ButtonState.Pressed;


                UpdateButtons();
            }
            else
            {
                camera.UpdateCameraMatrix();

                for (int i = 0; i < Objects.Count; i++)
                {
                    if (Objects[i].IsAlive)
                    {
                        Objects[i].Update();
                    }
                    else
                    {
                        if (Objects[i].TypeComponent is Player)
                        {
                            youDied = true;
                        }

                        if (Colliders.Contains((Collider)Objects[i].GetComponent("Collider")))
                        {
                            Colliders.Remove((Collider)Objects[i].GetComponent("Collider"));
                        }
                        Objects.Remove(Objects[i]);
                    }
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            // TODO: Add your drawing code here
            if (!gameStarted)
            {
                spriteBatch.Begin();
            }
            else
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera.CameraMatrix);
            }

            //Should only be run once.
            if (youWon)
            {
                spriteBatch.DrawString(font, "YOU WON!", 
                    new Vector2(-camera.CameraMatrix.Translation.X + 50, -camera.CameraMatrix.Translation.Y + 150), Color.DarkRed);
            }
            else if (youDied)
            {
                spriteBatch.DrawString(font, " YOU DIED\nPRESS ESC",
                    new Vector2(-camera.CameraMatrix.Translation.X + 5, -camera.CameraMatrix.Translation.Y + 50), Color.DarkRed);
            }

            if (!gameStarted)
            {
                spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height), Color.White);

                for (int i = 0; i < numberOfButtons; i++)
                    spriteBatch.Draw(buttonTexture[i], buttonRectangle[i], buttonColor[i]);
            }
            else
            {
                foreach (GameObject go in Objects)
                {
                    //OnScreen makes sure we don't draw GameObject outside of our vision.
                    if (OnScreen(go))
                    {
                        go.Draw(spriteBatch);
                    }
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool OnScreen(GameObject go)
        {
            SpriteRenderer spriteRender = (SpriteRenderer)go.GetComponent("SpriteRenderer");
            return (go.Transformer.Position.X + spriteRender.Rectangle.Width > -camera.CameraMatrix.Translation.X &&
                    go.Transformer.Position.X < -camera.CameraMatrix.Translation.X + ScreenWidth &&
                    go.Transformer.Position.Y + spriteRender.Rectangle.Height > -camera.CameraMatrix.Translation.Y &&
                    go.Transformer.Position.Y < -camera.CameraMatrix.Translation.Y + ScreenHeight);
        }

        public void NextLevel(GameObject gameObject)
        {
            if (gameObject.TypeComponent is Necklace)
            {
                nextLevel = true;
            }
        }

        public void YouWon(GameObject gameObject)
        {
            if (gameObject.TypeComponent is Player)
            {
                youWon = true;
            }
        }












        /// <summary>
        /// Below is the methods used for the menu.

        //Menu stuff
        private bool HitImageAlpha(Rectangle rect, Texture2D tex, int x, int y)
        {
            return HitImageAlpha(0, 0, tex, tex.Width * (x - rect.X) /
                rect.Width, tex.Height * (y - rect.Y) / rect.Height);
        }

        // wraps hit_image then determines if hit a transparent part of image 
        private bool HitImageAlpha(float tx, float ty, Texture2D tex, int x, int y)
        {
            if (HitImage(tx, ty, tex, x, y))
            {
                uint[] data = new uint[tex.Width * tex.Height];
                tex.GetData<uint>(data);
                if ((x - (int)tx) + (y - (int)ty) *
                    tex.Width < tex.Width * tex.Height)
                {
                    return ((data[(x - (int)tx) + (y - (int)ty) * tex.Width] & 4278190080) >> 24) > 20;
                }
            }
            return false;
        }

        // determines whether x and y is within the rectangle formed by the texture located at tx,ty
        private bool HitImage(float tx, float ty, Texture2D tex, int x, int y)
        {
            return (x >= tx &&
                x <= tx + tex.Width &&
                y >= ty &&
                y <= ty + tex.Height);
        }

        // determines the state and color of the button
        private void UpdateButtons()
        {

            for (int i = 0; i < numberOfButtons; i++)
            {

                if (HitImageAlpha(buttonRectangle[i], buttonTexture[i], mouseX, mouseY))
                {
                    buttonTimer[i] = 0.0;
                    if (mousePressed)
                    {
                        // mouse is currently down
                        buttonState[i] = State.Down;
                        buttonColor[i] = Color.Red;
                    }
                    else if (!mousePressed && mousePressedNow)
                    {
                        // mouse was just released
                        if (buttonState[i] == State.Down)
                        {
                            // button [i] was just down
                            buttonState[i] = State.Released;

                            //Button actions.
                            if (i == 0)
                            {
                                SoundEffect mummyMoan = Content.Load<SoundEffect>("Sound/zombieSound");
                                SoundEffectInstance mummyMoanInstance = mummyMoan.CreateInstance();
                                mummyMoanInstance.Volume = 0.25f;
                                mummyMoanInstance.Play();
                                
                                gameStarted = true;
                                LoadContent();
                            }
                            else if (i == 1)
                            {
                                System.Diagnostics.Debug.WriteLine("No help here.");
                            }
                            else if (i == 2)
                            {
                                this.Exit();
                            }
                        }
                    }
                    else
                    {
                        buttonState[i] = State.Hover;
                        buttonColor[i] = Color.BurlyWood;
                    }
                }
                else
                {
                    buttonState[i] = State.Up;
                    if (buttonTimer[i] > 0)
                    {
                        buttonTimer[i] = buttonTimer[i] - deltaTime;
                    }
                    else
                    {
                        buttonColor[i] = Color.White;
                    }
                }
            }
        }
    }
}
