using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
        PlayerCamera camera;

        public List<Collider> Colliders { get; } = new List<Collider>();
        public List<GameObject> Objects { get; } = new List<GameObject>();

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
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //SoundEffect effect = Content.Load<SoundEffectInstance>("");
            //musicEngine = effect.CreateInstance();
            //musicEngine.IsLooped = true;
            //musicEngine.Volume = 0.5f;
            //musicEngine.Play();

            if (!gameStarted)
            {
                // TODO: Add your initialization logic here
                graphics.PreferredBackBufferWidth = 780;  // set this value to the desired width of your window
                graphics.PreferredBackBufferHeight = 500;   // set this value to the desired height of your window
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
                LevelCreator LevelCreate = new LevelCreator(Content);
                GameObject player = LevelCreate.AddToList();

                for (int i = 0; i < LevelCreate.CreatorObjects.Count; i++)
                {
                    Objects.Add(LevelCreate.CreatorObjects[i]);
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
            if (!gameStarted)
            {
                Window.Title = "Mummy Despair - Main Menu";
            }
            else
            {
                Window.Title = "Mummy Despair - Love is in the air!";
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!gameStarted)
            {
                // update mouse variables
                MouseState mouseState = Mouse.GetState();
                mouseX = mouseState.X;
                mouseY = mouseState.Y;
                mousePressedNow = mousePressed;
                mousePressed = mouseState.LeftButton == ButtonState.Pressed;


                updateButtons();
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
                        Objects.Remove(Objects[i]);

                        if (Colliders.Contains((Collider)Objects[i].GetComponent("Collider")))
                        {
                            Colliders.Remove((Collider)Objects[i].GetComponent("Collider"));
                        }
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
                    go.Draw(spriteBatch);
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }



        //Menu stuff
        private bool hit_image_alpha(Rectangle rect, Texture2D tex, int x, int y)
        {
            return hit_image_alpha(0, 0, tex, tex.Width * (x - rect.X) /
                rect.Width, tex.Height * (y - rect.Y) / rect.Height);
        }

        // wraps hit_image then determines if hit a transparent part of image 
        private bool hit_image_alpha(float tx, float ty, Texture2D tex, int x, int y)
        {
            if (hit_image(tx, ty, tex, x, y))
            {
                uint[] data = new uint[tex.Width * tex.Height];
                tex.GetData<uint>(data);
                if ((x - (int)tx) + (y - (int)ty) *
                    tex.Width < tex.Width * tex.Height)
                {
                    return ((data[
                        (x - (int)tx) + (y - (int)ty) * tex.Width
                        ] &
                                4278190080) >> 24) > 20;
                }
            }
            return false;
        }

        // determines whether x and y is within the rectangle formed by the texture located at tx,ty
        private bool hit_image(float tx, float ty, Texture2D tex, int x, int y)
        {
            return (x >= tx &&
                x <= tx + tex.Width &&
                y >= ty &&
                y <= ty + tex.Height);
        }

        // determines the state and color of the button
        private void updateButtons()
        {

            for (int i = 0; i < numberOfButtons; i++)
            {

                if (hit_image_alpha(buttonRectangle[i], buttonTexture[i], mouseX, mouseY))
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
                                gameStarted = true;
                                LoadContent();
                            }
                            else if (i == 1)
                            {
                                System.Diagnostics.Debug.WriteLine("Go fuck yourself.");
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
