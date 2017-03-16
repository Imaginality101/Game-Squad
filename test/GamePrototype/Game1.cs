using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GamePrototype.Classes;
using GamePrototype.Classes.Objects;
using GamePrototype.Classes.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace GamePrototype
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    
        // NOTE: As of whenever this gets pushed most of the program (or at least enough to get going) is planned out.
        // Anything you don't see where it should be, feel free to ask me but it'll probably be coming through the pipeline along the way
        // as I have more time. If you see something relevant to whatever was assigned for you to do, feel free to do it.
        // Go nuts - Tom
        
    // enums for use in program, we need a GameState and a CurrentRoom
    enum GameState { MainMenu, Game, GMenu, Win}
    enum CurrentRoom { Bedroom, Closet, Bathroom } // We'll start with just Bedroom for now, when we expand to more rooms add them to the end of the state list
    public class Game1 : Game
    {
        // define enums
        GameState gameState;
        CurrentRoom activeRoom;
        // create attribute components specifically purposed for this class here
        GraphicsDeviceManager graphics;
        ContentManager content; // added kat
        SpriteBatch uSpriteBatch; // this
        // Caleb - new attribute for reading data
        SaveData data;
        // Caleb - List<GameObject> attribute that will be assigned the the contents of the save files - we will use the rooms later
        List<GameObject> objects;
        // Rectangle viewBounds; // I want to try to work it out so that the game changes resolution cleanly so we'll be using this for graphx

        // any rooms will be defined here as we get them added
        Room bedRoom;

        // phone menu
        Texture2D startingPhoneState;
        Texture2D imagePhoneState;
        Texture2D textPhoneState;

        // Keyboard states
        KeyboardState kbState;
        KeyboardState prevKbState;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
        }

        // NOTE: this method will be long AF so it needs to be moved down to the bottom of the class
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            uSpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: Kat - Load texture sprites in here. What I'd recommend doing to make it easier to pass over to Declan is the use
            // of a Dictionary, with strings for the key and values being Texture2Ds. If you do decide to do it that way just add it to the
            // attributes.

            // phone menu
            startingPhoneState = Content.Load<Texture2D>("phoneMain0");
            imagePhoneState = Content.Load<Texture2D>("phoneMain5");
            textPhoneState = Content.Load<Texture2D>("phoneMain7");

            // Caleb - this is a temporary solution to loading sprites until we have a dictionary
            foreach (GameObject go in objects)
            {
                go.LoadContent(Content.Load<Texture2D>(go.SpriteName));
            }

            // loads the bedroom texture by giving the room the ability to go it locally
            bedRoom = new Room(GraphicsDevice,Content);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // initialize enums
            gameState = GameState.Game;//<---------------------------------HEY LOOK AT ME----------IM CHANGED FOR TESTING------------
            activeRoom = CurrentRoom.Bedroom;
            data = new SaveData();
            // initializes the bedroom
            // Caleb - writes appropriate data to file, will save later
            data.WriteBedroom();
            // Caleb - reads GameObjects from the file, stores it in objects
            objects = data.ReadBedroom();

            base.Initialize();
           
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
            // NOTE: If we want to use Esc as the menu key this is pretty important to remove
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: The game's update function should primarily call the Update function of the active Room. That should run through inside the Room and update all the objects in it.
            // TODO: Check if menus are open or the open button has been pressed, and if so update them
            kbState = Keyboard.GetState();
            switch (gameState)
            {
                case GameState.MainMenu:
                    {
                        // TODO: May have buttons in main menu, the Enter key is just temporary
                        if (kbState.IsKeyDown(Keys.Enter) && !prevKbState.IsKeyDown(Keys.Enter))
                        {
                            gameState = GameState.Game;
                        }
                        break;
                    }
                case GameState.Game:
                    {
                        // switch between rooms, update the right room
                        switch (activeRoom)
                        {
                            case CurrentRoom.Bedroom:
                                bedRoom.Update(gameTime);
                                break;
                            case CurrentRoom.Closet:
                                // TODO: update closet
                                break;
                            case CurrentRoom.Bathroom:
                                // TODO: update bathroom
                                break;
                        }
                        if (kbState.IsKeyDown(Keys.Tab) && !prevKbState.IsKeyDown(Keys.Tab))
                        {
                            gameState = GameState.GMenu;
                        }
                        break;
                    }
                case GameState.GMenu:
                    {
                        // TODO: update the phone menu
                        // kat draws menu things 
                        if (gameState == GameState.GMenu)
                        {
                            // main menu
                            uSpriteBatch.Draw(startingPhoneState, new Rectangle(0, 0, 100, 100), Color.White);

                            if (kbState.IsKeyDown(Keys.D1) && !prevKbState.IsKeyDown(Keys.D1))
                            {
                                // journal menu
                                uSpriteBatch.Draw(textPhoneState, new Rectangle(0, 0, 100, 100), Color.White);
                            }

                            if (kbState.IsKeyDown(Keys.D2) && !prevKbState.IsKeyDown(Keys.D2))
                            {
                                // photo menu
                                uSpriteBatch.Draw(imagePhoneState, new Rectangle(0, 0, 100, 100), Color.White);
                            }

                            if (kbState.IsKeyDown(Keys.D3) && !prevKbState.IsKeyDown(Keys.D3))
                            {
                                // settings menu
                            }

                            if (kbState.IsKeyDown(Keys.D4) && !prevKbState.IsKeyDown(Keys.D4))
                            {
                                // exit game code
                            }

                            if (kbState.IsKeyDown(Keys.Tab) && !prevKbState.IsKeyDown(Keys.Tab))
                            {
                                // close menu
                                gameState = GameState.Game;
                            }

                        }
                        break;
                    }
                case GameState.Win:
                    {
                        // TODO: same with main menu, press enter to continue
                        if (kbState.IsKeyDown(Keys.Enter) && !prevKbState.IsKeyDown(Keys.Enter))
                        {
                            gameState = GameState.MainMenu;
                        }
                        break;
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
            GraphicsDevice.Clear(Color.Black); 

            // TODO: Call the Draw Command of the active Room here, we'll have it handle drawing just to keep the code in a more relevant place
            //activeRoom.Draw(); // this will blow up until we have the rooms initializing property, so be careful

            // begin spritebatch
            uSpriteBatch.Begin();

            // calls the bedroom draw command - kat
            bedRoom.Draw(uSpriteBatch);

            // TODO: Caleb - draws objects; is temporary 
           /*foreach (GameObject go in objects)
            {
                go.Draw(uSpriteBatch);
            }*/

            // end spritebatch
            uSpriteBatch.End();

            // TODO: Check if menus are open, and draw them after the room if they are so that the room itself stays visible
            base.Draw(gameTime);
        }

        // TODO: Save method, should go through the process to store important information.
        // Each room should probably have its own binary file, we mainly want those to have booleans for the active states of objects in the room
        // There should also be a file more specific to the overall game, with info on player's active room, position and any modifiers
        public void Save()
        {

        }

        // TODO: Load method, needs to read the binary files from the Save command and read them in. Should then use that info to set up stuff
        public void Load()
        {
            // NOTE: the Room class has an initialize function, if you do the individual loading code with a fed parameter for the information collections/variables
            // that should be easier since you can then just call Initialize for each Room in here
        }
    }
}