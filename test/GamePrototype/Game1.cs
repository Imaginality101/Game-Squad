using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GamePrototype.Classes;
using GamePrototype.Classes.Objects;
using GamePrototype.Classes.Tools;
using GamePrototype.Classes.Menu;
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
    public enum CurrentRoom { Bedroom, Closet, Bathroom } // We'll start with just Bedroom for now, when we expand to more rooms add them to the end of the state list

    public class Game1 : Game
    {
        // Tom - Setting variables for scaling draw
        Boolean fullscreen;
        const int NORM_WIDTH = 1728;
        const int NORM_HEIGHT = 972;

        // values for custom resolution settings being read from the external tool
        public static int windowWidth;
        public static int windowHeight;
        public static Vector2 drawRatio; // Keep track of the ratio of the current resolution to intended resolution

        // define enums
        GameState gameState;
        public static CurrentRoom activeRoom;
        
        // Caleb - first menu object, non static for now
        Menu menu;
        //MenuState menuState; // kat
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
        Room closetRoom;
        
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

        Texture2D blacklight;
        Texture2D bedBG;
        Texture2D closetBG;
        Texture2D bathBG;



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
        SpriteFont menuFont;
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

            
            MasterContentLoader();//HERE IS WERE TEXTURES GET LOADED
            // Caleb - instantiate the textbox - Kat
            playerCenter = new Vector2(faceUp.Width / 2, faceUp.Height / 2);
            timer = .1;

            bedRoom = new Room(bedBG);
            furnitureSet = new ObjectSetup(Content, uSpriteBatch, GraphicsDevice);
            bedRoom.Objects = furnitureSet.BedroomSetup();
            player = new Player(GraphicsDevice, content, faceRight, protagTextureRight, faceUp, faceDown, bedRoom.CollisionBounds);
            // TODO: fill in the nulls in the parameters list once we have more textures
            Clue.LoadContent(Content.Load<Texture2D>("NewspaperFULL"), Content.Load<Texture2D>("key1"), Content.Load<Texture2D>("key1"), Content.Load<Texture2D>("Photo1"), null, Content.Load<Texture2D>("Diary1"), Content.Load<Texture2D>("Crazy1"), null, null, null, null, null, null, null, Content.Load<Texture2D>("stickynoteFULL"));
            menu.LoadContent(startingPhoneState, imagePhoneState, textPhoneState, menuFont);

            closetRoom = new Room(closetBG);
            closetRoom.Objects = furnitureSet.ClosetSetup();


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
            //menuState = MenuState.Main; // kat

            data = new SaveData();
            // initializes the bedroom
            // Caleb - writes appropriate data to file, will save later
            data.WriteBedroom();
            // Caleb - reads GameObjects from the file, stores it in objects
            //objects = data.ReadBedroom();
            settingsData = data.ReadSettings();
            timerMode = (bool)settingsData[0];
            bobRossMode = (bool)settingsData[1];
            fullscreen = (Boolean)settingsData[2]; // Tom - Get whether or not the window is fullscreen
            if(fullscreen)
            {
                graphics.IsFullScreen = true;
                windowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                windowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
            else
            {
                windowWidth = (int)settingsData[3];
                windowHeight = (int)settingsData[4];
            }
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
            drawRatio.X = (float)windowWidth / NORM_WIDTH;
            drawRatio.Y = (float)windowHeight / NORM_HEIGHT;
            graphics.ApplyChanges();

            Console.WriteLine("Timer mode: " + timerMode + " Bob Ross mode: " + bobRossMode);
            menu = new Menu();
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
                                player.Update(gameTime, bedRoom.Objects);
                                break;
                            case CurrentRoom.Closet:
                                // TODO: update closet
                                closetRoom.Update(gameTime);
                                player.Update(gameTime, closetRoom.Objects);
                                break;
                            case CurrentRoom.Bathroom:
                                // TODO: update bathroom
                                break;
                        }
                        if (kbState.IsKeyDown(Keys.Tab))// && !prevKbState.IsKeyDown(Keys.Tab))
                        {
                            gameState = GameState.GMenu;
                        }
                        
                        // Caleb - handles drawing interaction text
                        if (kbState.IsKeyDown(Keys.E) && prevKbState.IsKeyUp(Keys.E))
                        {
                            drawInteractText = true;
                        }
                        if (kbState.IsKeyUp(Keys.E) && prevKbState.IsKeyDown(Keys.E))
                        {
                            drawInteractText = false;
                        }
                        // if C is pressed, print the inventory
                        if (kbState.IsKeyDown(Keys.C) && prevKbState.IsKeyUp(Keys.C))
                        {
                            Clue.PrintInventory();
                        }
                        break;
                    }
                case GameState.GMenu:
                    {
                        // TODO: update the phone menu
                        // kat draws menu things 
                        /*
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
                            
                        */
                        if (kbState.IsKeyDown(Keys.LeftShift) && !prevKbState.IsKeyDown(Keys.LeftShift)) // would like to make tab later but wasnt working
                        {
                            // close menu
                            //menuState = MenuState.Main;
                            gameState = GameState.Game;
                        }
                        // Caleb - updates the menu instance; we might stick the above into this method?
                        menu.Update();
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
            // Caleb - update Textbox
            //settingsTextBox.Update(kbState, prevKbState);
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
                switch (activeRoom)
                {
                    case CurrentRoom.Bedroom:
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

                        if (bedRoom.LightsOff == true)
                        {
                            uSpriteBatch.Draw(blacklight, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                        }
                        break;
                    case CurrentRoom.Closet:
                        // TODO: Draw closet
                        if (player.PlayerRect.Y < GraphicsDevice.Viewport.Bounds.Height / 2)
                        {
                            closetRoom.Draw(uSpriteBatch);
                            player.Draw(uSpriteBatch);
                        }
                        else
                        {
                            closetRoom.Draw(uSpriteBatch);
                            player.Draw(uSpriteBatch);
                        }

                        if (closetRoom.LightsOff == true)
                        {
                            uSpriteBatch.Draw(blacklight, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                        }
                        break;
                    case CurrentRoom.Bathroom:
                        // TODO: update bathroom
                        break;
                }
                
            }


            // menu stuff kat  --- move to menu draw >??????????????????????????????????????????????????????????????????????????????
            /*if (gameState == GameState.GMenu && menuState == MenuState.Main)
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
                uSpriteBatch.Draw(textPhoneState, new Rectangle(300, 0, 1200, 1000), Color.White);
                // Draw textbox
                settingsTextBox.Draw(uSpriteBatch);
            }
            if (gameState == GameState.GMenu && menuState == MenuState.Power)
            {
                bedRoom.Draw(uSpriteBatch);
            }
            */
            if (gameState == GameState.GMenu)
            {
                bedRoom.Draw(uSpriteBatch);
                menu.Draw(uSpriteBatch);
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
            // Draw textbox
            //box.Draw(uSpriteBatch);

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
        public void MasterContentLoader()
        {
            // NOTE: Here is where i will load every god damn texture and sound so that the main stops looking like garb and we cont have to pass in ContentManagers *Looks at Kat*
            intro = new GameSound("spook3-thebegining", content);
            music = new GameSound("spook3-theloop ", content);
            font = Content.Load<SpriteFont>("Arial");
            menuFont = Content.Load<SpriteFont>("Courier12");

            // main menu - kat
            if (bobRossMode == true)
            {
                mainMenu = Content.Load<Texture2D>("GameMenuBOBROSSMODE-FULL");
            }
            else
            {
                mainMenu = Content.Load<Texture2D>("GameMenuFULL");
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
            protagTextureRight = new List<Texture2D>();
            protagTextureRight.Add(faceRight1);
            protagTextureRight.Add(faceRight2);
            protagTextureRight.Add(faceRight3);
            protagTextureRight.Add(faceRight4);
            protagTextureRight.Add(faceRight5);
            protagTextureRight.Add(faceRight6);
            protagTextureRight.Add(faceRight7);
            protagTextureRight.Add(faceRight8);

            blacklight = content.Load<Texture2D>("black light overlay");

            bedBG = content.Load<Texture2D>("backgroundFULL");
            closetBG = content.Load<Texture2D>("theclosetFULL");


        }

        public static Rectangle FormatDraw(Rectangle drawRect)
        {
            Rectangle scaleRect;
            int newX = (int)(drawRatio.X * drawRect.X);
            int newY = (int)(drawRatio.Y * drawRect.Y);
            int newWidth = (int)(drawRatio.X * drawRect.Width);
            int newHeight = (int)(drawRatio.Y * drawRect.Height);
            scaleRect = new Rectangle(newX, newY, newWidth, newHeight);
            return scaleRect;
        }
        public static void ChangeRoom(CurrentRoom wehere)
        {
            //activeRoom = wehere;
        }

    }
}