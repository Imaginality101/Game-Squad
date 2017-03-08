using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InteractionAttempt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Texture2D playerSprite;
        Texture2D greenGuySprite;
        Texture2D blueGuySprite;
        Interactable greenGuy;
        Interactable blueGuy;
        SpriteFont sf;
        bool isVisible = false;
        string box = "";

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            player = new Player(new Vector2(40));
            blueGuy = new Interactable(new Vector2(0, 20), player);
            greenGuy = new Interactable(new Vector2(400, 20), player);
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

            // TODO: use this.Content to load your game content here
            playerSprite = Content.Load<Texture2D>("Player");
            greenGuySprite = Content.Load<Texture2D>("GreenGuy");
            blueGuySprite = Content.Load<Texture2D>("BlueGuy");
            player.LoadContent(playerSprite);
            greenGuy.LoadContent(greenGuySprite);
            blueGuy.LoadContent(blueGuySprite);
            try
            {
                sf = Content.Load<SpriteFont>("SpriteFont1");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                Exit();

            // TODO: Add your update logic here
            player.Update();
            base.Update(gameTime);
            // update the text
            isVisible = false;
            if (blueGuy.DistanceFromPlayer < 80)
            {
                isVisible = true;
                box = "Blue";
            }
            if (greenGuy.DistanceFromPlayer < 80)
            {
                isVisible = true;
                box = "Green";
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            // when E is pressed, evaluate whether the text should be drawn
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                if (isVisible)
                {
                    spriteBatch.DrawString(sf, string.Format("{0} is very close to the player!", box), new Vector2(0, 100),
                        Color.Black);
                }
            }
            spriteBatch.End();
            blueGuy.Draw(spriteBatch);
            greenGuy.Draw(spriteBatch);
            player.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
