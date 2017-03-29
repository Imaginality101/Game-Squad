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
using System.Threading;
using System.Diagnostics;
using System.IO;
/*Workers: Kat, Tom, Caleb, Declan
 * DisasterPiece Games
 * Game1 Class
 */
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
    enum MenuState { Main, Journal, Photos, Settings, Power} // kat
    enum CurrentRoom { Bedroom, Closet, Bathroom } // We'll start with just Bedroom for now, when we expand to more rooms add them to the end of the state list
    public class Game1 : Game
    {
        // define enums
        GameState gameState;
        CurrentRoom activeRoom;
        MenuState menuState; // kat
        // create attribute components specifically purposed for this class here
        GraphicsDeviceManager graphics;
        ContentManager content; // added kat
        SpriteBatch uSpriteBatch; // this
        //Declan - this is for sounds
        GameSound music;
        GameSound intro;
        // Caleb - new attribute for reading data
        SaveData data;
        // Caleb - List<GameObject> attribute that will be assigned the the contents of the save files - we will use the rooms later
        //List<GameObject> objects;
        // Rectangle viewBounds; // I want to try to work it out so that the game changes resolution cleanly so we'll be using this for graphx

        // any rooms will be defined here as we get them added
        Room bedRoom;
        ObjectSetup furnitureSet;

        // phone menu - kat
        Texture2D startingPhoneState;
        Texture2D imagePhoneState;
        Texture2D textPhoneState;

        // playerdir variable for walking - kat
        PlayerDir playerDirection;

        // player - kat
        Player player;
        Texture2D faceUp;
        Texture2D faceDown;
        Texture2D faceRight;
        Texture2D faceRight1;
        Texture2D faceRight2;
        Texture2D faceRight3;
        Texture2D faceRight4;
        Texture2D faceRight5;
        Texture2D faceRight6;
        Texture2D faceRight7;
        Texture2D faceRight8;
        Rectangle protagRect;
        Vector2 playerCenter;
        List<Texture2D> protagTextureRight;
        double timer;
        int i;

        // mainmenu - kat
        Texture2D mainMenu;
        Rectangle mainMenuRect;

        // Keyboard states
        KeyboardState kbState;
        KeyboardState prevKbState;
        // path to the external tool
        const string PATH = "..\\..\\..\\..\\..\\ExternalTool\\bin\\Debug\\ExternalTool.exe";
        List<object> settingsData;
        bool bobRossMode;
        // this is for timed mode
        bool timerMode;
        int gameTimerSeconds = 15 * 60;
        float elapsedTime = 0;
        // Caleb - used for displaying text: is temporary
        SpriteFont font;
        bool drawInteractText = false;
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
            intro = new GameSound("spook3-thebegining", content);
            music = new GameSound("spook3-theloop ", content);
            font = Content.Load<SpriteFont>("Arial");

            // TODO: Kat - Load texture sprites in here. What I'd recommend doing to make it easier to pass over to Declan is the use
            // of a Dictionary, with strings for the key and values being Texture2Ds. If you do decide to do it that way just add it to the
            // attributes.

            // main menu - kat
            if (bobRossMode == true)
            {
                mainMenu = Content.Load<Texture2D>("d6twRar");
            }
            else
            {
                mainMenu = Content.Load<Texture2D>("mainmenumaayybe.");
            }
            mainMenuRect = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            // phone menu - kat
            startingPhoneState = Content.Load<Texture2D>("phoneMain0");
            imagePhoneState = Content.Load<Texture2D>("phoneMain5");
            textPhoneState = Content.Load<Texture2D>("phoneMain7");

            // protag - kat
            faceUp = content.Load<Texture2D>("backStationary");
            faceDown = content.Load<Texture2D>("frontStationary");
            faceRight = content.Load<Texture2D>("profileStationary");
            faceRight1 = content.Load<Texture2D>("profile1");
            faceRight2 = content.Load<Texture2D>("profile2");
            faceRight3 = content.Load<Texture2D>("profile3");
            faceRight4 = content.Load<Texture2D>("profile4");
            faceRight5 = content.Load<Texture2D>("profile5");
            faceRight6 = content.Load<Texture2D>("profile6");
            faceRight7 = content.Load<Texture2D>("profile7");
            faceRight8 = content.Load<Texture2D>("profile8");
            protagRect = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 50, graphics.PreferredBackBufferHeight / 2 - 50, 96, 192);//<-------------THIS IS WHERE THE PLAYER RECT SIZE IS----------------
            protagTextureRight = new List<Texture2D>();
            protagTextureRight.Add(faceRight1);
            protagTextureRight.Add(faceRight2);
            protagTextureRight.Add(faceRight3);
            protagTextureRight.Add(faceRight4);
            protagTextureRight.Add(faceRight5);
            protagTextureRight.Add(faceRight6);
            protagTextureRight.Add(faceRight7);
            protagTextureRight.Add(faceRight8);
            playerCenter = new Vector2(faceUp.Width / 2, faceUp.Height / 2);
            timer = .1;
            i = 0;

            // Caleb - this is a temporary solution to loading sprites until we have a dictionary
            /*foreach (GameObject go in objects)
            {
                go.LoadContent(Content.Load<Texture2D>(go.SpriteName));
            }*/

            // loads the bedroom texture by giving the room the ability to go it locally
            bedRoom = new Room(GraphicsDevice,Content);
            furnitureSet = new ObjectSetup(Content, uSpriteBatch, GraphicsDevice);
            bedRoom.Objects = furnitureSet.BedroomSetup();
            player = new Player(faceRight, protagTextureRight, faceUp, faceDown, bedRoom.CollisionBounds,  protagRect); 
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
            gameState = GameState.MainMenu;
            activeRoom = CurrentRoom.Bedroom;
            menuState = MenuState.Main; // kat

            // TODO: Screen sizes here
            graphics.PreferredBackBufferWidth = 1728;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 972;   // set this value to the desired height of your window
            //graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
           // graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window

            //set the GraphicsDeviceManager's fullscreen property
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            data = new SaveData();
            // initializes the bedroom
            // Caleb - writes appropriate data to file, will save later
            data.WriteBedroom();
            // Caleb - reads GameObjects from the file, stores it in objects
            //objects = data.ReadBedroom();
            settingsData = data.ReadSettings();
            timerMode = (bool)settingsData[0];
            bobRossMode = (bool)settingsData[1];
            Console.WriteLine("Timer mode: " + timerMode + " Bob Ross mode: " + bobRossMode);
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
            prevKbState = kbState;
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
                        // timer for animation - kat
                        timer -= gameTime.ElapsedGameTime.TotalSeconds;
                        intro.PlayIntro(.5f);
                        if (intro.IsPlayed == true)
                        {
                            music.PlayAsMusic(.5f);
                        }
                        // Caleb - game timer code
                        if (timerMode)
                        {
                            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                            if (elapsedTime > 1000)
                            {
                                elapsedTime = 0;
                                gameTimerSeconds--;
                            }
                        }
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
                        if (kbState.IsKeyDown(Keys.Tab))// && !prevKbState.IsKeyDown(Keys.Tab))
                        {
                            gameState = GameState.GMenu;
                        }
                        player.Update(gameTime, bedRoom.Objects);
                        // Caleb - handles drawing interaction text
                        if (kbState.IsKeyDown(Keys.E) && prevKbState.IsKeyUp(Keys.E))
                        {
                            drawInteractText = true;
                        }
                        if (kbState.IsKeyUp(Keys.E) && prevKbState.IsKeyDown(Keys.E))
                        {
                            drawInteractText = false;
                        }
                        break;
                    }
                case GameState.GMenu:
                    {
                        // TODO: update the phone menu
                        // kat draws menu things 
                        if (gameState == GameState.GMenu)
                        {
                            if (kbState.IsKeyDown(Keys.D1) && !prevKbState.IsKeyDown(Keys.D1))
                            {
                                // journal menu
                                menuState = MenuState.Journal;
                            }

                            if (kbState.IsKeyDown(Keys.D2) && !prevKbState.IsKeyDown(Keys.D2))
                            {
                                // photo menu
                                menuState = MenuState.Photos;
                            }

                            if (kbState.IsKeyDown(Keys.D3) && !prevKbState.IsKeyDown(Keys.D3))
                            {
                                // settings menu
                                menuState = MenuState.Settings;
                            }

                            if (kbState.IsKeyDown(Keys.D4) && !prevKbState.IsKeyDown(Keys.D4))
                            {
                                // exit game code
                                menuState = MenuState.Power;
                            }

                            // NOT WORKING RIGHT NOW --- Fixed it, you just needed a set of parenthesis around the state checks here - Tom
                            if ((menuState == MenuState.Journal || menuState == MenuState.Photos || menuState == MenuState.Settings) && kbState.IsKeyDown(Keys.Tab) && !prevKbState.IsKeyDown(Keys.Tab))
                            {
                                // back to main menu
                                menuState = MenuState.Main;
                            }
                            

                            if (kbState.IsKeyDown(Keys.LeftShift) && !prevKbState.IsKeyDown(Keys.LeftShift)) // would like to make tab later but wasnt working
                            {
                                // close menu
                                menuState = MenuState.Main;
                                gameState = GameState.Game;
                            }

                        }
                        break;
                    }
                case GameState.Win:
                    {
                        // TODO: same with main menu, press enter to continue
                        if (kbState.IsKeyDown(Keys.Enter) && prevKbState.IsKeyUp(Keys.Enter))
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

            // draws the mainmenu - kat
            if (gameState == GameState.MainMenu)
            {
                uSpriteBatch.Draw(mainMenu, mainMenuRect, Color.White);
            }

            // calls the bedroom draw command - kat
            if (gameState == GameState.Game)
            {
                if (player.PlayerRect.Y < GraphicsDevice.Viewport.Bounds.Height / 2)
                {
                    bedRoom.Draw(uSpriteBatch);
                    player.Draw(uSpriteBatch);
                }
                else
                {
                    bedRoom.Draw(uSpriteBatch);
                    player.Draw(uSpriteBatch);
                }
            }


            // menu stuff kat
            if (gameState == GameState.GMenu && menuState == MenuState.Main)
            {
                bedRoom.Draw(uSpriteBatch);
                uSpriteBatch.Draw(startingPhoneState, new Rectangle(300, 0, 1200, 1000), Color.White);
            }
            if (gameState == GameState.GMenu &&  menuState == MenuState.Journal)
            {
                bedRoom.Draw(uSpriteBatch);
                uSpriteBatch.Draw(textPhoneState, new Rectangle(300, 0, 1200, 1000), Color.White);
            }
            if (gameState == GameState.GMenu && menuState == MenuState.Photos)
            {
                bedRoom.Draw(uSpriteBatch);
                uSpriteBatch.Draw(imagePhoneState, new Rectangle(300, 0, 1200, 1000), Color.White);
            }
            if (gameState == GameState.GMenu && menuState == MenuState.Settings)
            {
                bedRoom.Draw(uSpriteBatch);
            }
            if (gameState == GameState.GMenu && menuState == MenuState.Power)
            {
                bedRoom.Draw(uSpriteBatch);
            }

            // TODO: Caleb - draws objects; is temporary 
            /*foreach (GameObject go in objects)
             {
                 go.Draw(uSpriteBatch);
             }*/

            //furnitureSet.DrawBedroom(); //kat commented out for now

            // Caleb - draw interact text
            if (drawInteractText)
            {
                //uSpriteBatch.DrawString(font, player.FlagInteractables(bedRoom.Objects.ToArray()), Vector2.Zero, Color.White);
                //drawInteractText = false;
            }
            // draw timer if enabled
            if (timerMode)
            {
                uSpriteBatch.DrawString(font, string.Format("{0}:{1}", gameTimerSeconds / 60, gameTimerSeconds % 60), new Vector2(0, 50), Color.White);
            }
            //TODO: Remove this shit
            Texture2D testRect = Content.Load<Texture2D>("BlueGuy");
            //uSpriteBatch.Draw(testRect, player.PlayerRect, Color.Red);
            //uSpriteBatch.Draw(testRect, bedRoom.Objects[4].GlobalBounds, Color.Green);
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