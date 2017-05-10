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
    enum MainMenuState { NewGame, Continue}
    public enum CurrentRoom { Bedroom, Closet, Bathroom, WinRoom } // We'll start with just Bedroom for now, when we expand to more rooms add them to the end of the state list

    public class Game1 : Game
    {
        private PopUpManager messageDisplay;

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
        MainMenuState mainState;
        
        // Caleb - first menu object, non static for now
        Menu menu;
        //MenuState menuState; // kat
        // create attribute components specifically purposed for this class here
        GraphicsDeviceManager graphics;
        static ContentManager content; // added kat
        SpriteBatch uSpriteBatch; // this
        //Declan - this is for sounds
        GameSound music;
        GameSound music2;
        GameSound intro;
        private GameSound blep;
        private GameSound beeboop;
        // Caleb - new attribute for reading data
        //SaveData data;
        // Caleb - List<GameObject> attribute that will be assigned the the contents of the save files - we will use the rooms later
        //List<GameObject> objects;
        // Rectangle viewBounds; // I want to try to work it out so that the game changes resolution cleanly so we'll be using this for graphx

        // any rooms will be defined here as we get them added
        Room bedRoom;
        Room closetRoom;
        Room bathRoom;
        Room winRoom;

        ObjectSetup bedSet;
        ObjectSetup closetSet;
        ObjectSetup bathSet;
        ObjectSetup winRoomSet;

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
        Texture2D faceRight9;
        Texture2D faceRight10;
        Texture2D faceRight11;
        Texture2D faceRight12;
        Texture2D faceRight13;
        Texture2D faceRight14;
        Texture2D faceRight15;
        Texture2D faceRight16;
        Texture2D faceDown1;
        Texture2D faceDown2;
        Texture2D faceDown3;
        Texture2D faceDown4;
        Texture2D faceDown5;
        Texture2D faceDown6;
        Texture2D faceDown7;
        Texture2D faceDown8;
        Texture2D faceUp1;
        Texture2D faceUp2;
        Texture2D faceUp3;
        Texture2D faceUp4;
        Texture2D faceUp5;
        Texture2D faceUp6;
        Texture2D faceUp7;
        Texture2D faceUp8;

        Rectangle protagRect;
        Vector2 playerCenter;
        List<Texture2D> protagTextureRight;
        List<Texture2D> protagTextureDown;
        List<Texture2D> protagTextureUp;
        double timer;
        int i;

        // mainmenu - kat
        Texture2D mainMenu;
        Rectangle mainMenuRect;
        // textboxes in the main menu
        TextBox newGame;
        TextBox continueGame;
        Texture2D mainMenuCursor;
        Rectangle mainMenuCursorLoc;
        Texture2D blacklight;
        static bool lightsOn;
        Texture2D bedBG;
        Texture2D closetBG;
        Texture2D bathBG;
        //instuctions
        Texture2D instructionOpen;
        Texture2D instructionClose;
        Texture2D instructionMove;
        Texture2D instructionNuke;
        Texture2D instructionInteract;
        Rectangle instructionOpenRect;
        Rectangle instructionCloseRect;
        Rectangle instructionMoveRect;
        Rectangle instructionNukeRect;
        Rectangle instructionInteractRect;

        // win/lose variable - kat
        int winLose; // 0 is nothing, 1 is lost, 2 is win
        Texture2D loseScreen;
        Texture2D winScreen;



        // Keyboard states
        KeyboardState kbState;
        KeyboardState prevKbState;
        // path to the external tool
        const string PATH = "..\\..\\..\\..\\..\\ExternalTool\\bin\\Debug\\ExternalTool.exe";
        List<object> settingsData;
        public static bool bobRossMode;
        // this is for timed mode
        static bool timerMode;
        int gameTimerSeconds = 15 * 60;
        float elapsedTime = 0;
        // Caleb - used for displaying text: is temporary
        SpriteFont font;
        SpriteFont menuFont;
        SpriteFont courier36;
        static bool drawInteractText = false;
        public static bool easyMode;
        private Vector2 origin;

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
            loseScreen = content.Load<Texture2D>("death_screen");
            winScreen = content.Load<Texture2D>("win_screen");
            
            MasterContentLoader();//HERE IS WERE TEXTURES GET LOADED
            // Caleb - instantiate the textbox - Kat
            playerCenter = new Vector2(faceUp.Width / 2, faceUp.Height / 2);
            timer = .1;

            origin = new Vector2(1728 / 2, 972 / 2);


            bedRoom = new Room(bedBG, new Rectangle(((int)origin.X - (1382 / 2)) + 150, ((int)origin.Y - (972 / 2)) + 0, 1382 - 220, 972 - 15));//Perf room bounds);
            closetRoom = new Room(closetBG, new Rectangle(((int)origin.X - (1382 / 2)) + 150, ((int)origin.Y - (270)) + 0, 1382 - 220, (972 / 2) + 40));
            bathRoom = new Room(bathBG, new Rectangle(((int)origin.X - (404)) + 150, ((int)origin.Y - (334)) + 0, 768 - 170, 768 - 80));
            // Caleb - winRoom is blank
            winRoom = new Room(new Texture2D(GraphicsDevice, 1, 1), new Rectangle(((int)origin.X - (404)) + 150, ((int)origin.Y - (334)) + 0, 768 - 170, 768 - 80));

            bedSet = new ObjectSetup(Content, uSpriteBatch, GraphicsDevice,closetRoom,bathRoom);
            closetSet = new ObjectSetup(Content, uSpriteBatch, GraphicsDevice, bedRoom,null);
            bathSet = new ObjectSetup(Content, uSpriteBatch, GraphicsDevice, bedRoom,null);
            winRoomSet = new ObjectSetup(Content, uSpriteBatch, GraphicsDevice, winRoom, null);

            bedRoom.Objects = bedSet.BedroomSetup();

            // TODO: call bathroom's DisableSavedClueObjects() when bathroom is created
            bedRoom.DisableSavedClueObjects();
            player = new Player(GraphicsDevice, content, faceRight, protagTextureRight, faceUp, faceDown, bedRoom.CollisionBounds, protagTextureUp, protagTextureDown);
            // TODO: fill in the nulls in the parameters list once we have more textures

            player.PopUp += messageDisplay.GetMessage;
            Clue.LoadContent( Content.Load<Texture2D>("NewspaperFULL"), Content.Load<Texture2D>("key2"), Content.Load<Texture2D>("key1"), Content.Load<Texture2D>("Photo1"), Content.Load<Texture2D>("NewspaperFULL"), Content.Load<Texture2D>("Diary1"), Content.Load<Texture2D>("Crazy1"), Content.Load<Texture2D>("recept1"), Content.Load<Texture2D>("Ring1"), Content.Load<Texture2D>("pandant"), Content.Load<Texture2D>("rotatethebones"), Content.Load<Texture2D>("knife"), Content.Load<Texture2D>("NewspaperFULL"), Content.Load<Texture2D>("pillshere"), Content.Load<Texture2D>("stickynoteFULL"), Content.Load<Texture2D>("New1Full"), Content.Load<Texture2D>("New2Full"), Content.Load<Texture2D>("New3Full"), Content.Load<Texture2D>("New4Full"), Content.Load<Texture2D>("New5FULL"), Content.Load<Texture2D>("VeryCrazy"), Content.Load<Texture2D>("VERYCrazyDiary"), Content.Load<Texture2D>("TD1FULL-NEW"), Content.Load<Texture2D>("TD2FULL"), Content.Load<Texture2D>("TD3FULL"), Content.Load<Texture2D>("oldphoto1"), Content.Load<Texture2D>("oldphoto2"), Content.Load<Texture2D>("oldphoto3"), Content.Load<Texture2D>("CrazyDiary1"), Content.Load<Texture2D>("CrazyDiary2"), Content.Load<Texture2D>("CrazyDiary3"), Content.Load<Texture2D>("receptimageFULL"));
            Clue.LoadInventory();
            menu.LoadContent(startingPhoneState, imagePhoneState, textPhoneState, menuFont, Content.Load<Texture2D>("BlueGuy"), settingsData);
            // initialize textboxes in the main menu
            newGame = new TextBox(new Vector2(200, 700), "New game", 100, 100, courier36, new Rectangle());
            continueGame = new TextBox(new Vector2(200, 600), "Continue Game", 100, 1, courier36, new Rectangle());
            

            closetRoom.Objects = closetSet.ClosetSetup();
            closetRoom.DisableSavedClueObjects();

            bathRoom.Objects = bathSet.BathroomSetup();
            bathRoom.DisableSavedClueObjects();

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
            mainState = MainMenuState.Continue;
            //menuState = MenuState.Main; // kat
            GetSettings(true); // get settings, yes this is the first time calling it
            winLose = 0;

            Console.WriteLine("Timer mode: " + timerMode + " Bob Ross mode: " + bobRossMode + " Easy mode: " + easyMode);
            menu = new Menu();
            lightsOn = false;
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
            prevKbState = kbState;
            kbState = Keyboard.GetState();

            // NOTE: If we want to use Esc as the menu key this is pretty important to remove
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) //|| Keyboard.GetState().IsKeyDown(Keys.D4) /*kbState.IsKeyDown(Keys.Escape) && prevKbState.IsKeyDown(Keys.Escape)/* && gameState != GameState.GMenu*/)
            {
                SaveData.Save();
                Exit();
            }

            // TODO: The game's update function should primarily call the Update function of the active Room. That should run through inside the Room and update all the objects in it.
            // TODO: Check if menus are open or the open button has been pressed, and if so update them
            
            if (winLose == 2)
            {
                Thread.Sleep(5000);
                Restart();
                winLose = 0;
            }

            // win state - kat
            if (activeRoom == CurrentRoom.WinRoom)
            {
                winLose = 2; 
            }

            if (winLose == 1) // kat
            {
                Thread.Sleep(5000);
                Environment.Exit(0);
            }

            // timer ran out lose state - kat
            if (gameTimerSeconds <= 0)
            {
                winLose = 1;
            }

            switch (gameState)
            {
                case GameState.MainMenu:
                    {
                        player.SendMessage("Move using W, and S. Confirm using E.");

                        // TODO: May have buttons in main menu, the Enter key is just temporary
                        if (kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyUp(Keys.S) && mainState == MainMenuState.Continue)
                        {
                            mainState = MainMenuState.NewGame;
                            mainMenuCursorLoc.Y += 100;
                        }
                        if (kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyUp(Keys.W) && mainState == MainMenuState.NewGame)
                        {
                            mainState = MainMenuState.Continue;
                            mainMenuCursorLoc.Y -= 100;
                        }
                        if (kbState.IsKeyDown(Keys.E) && !prevKbState.IsKeyDown(Keys.E))
                        {
                            if (mainState == MainMenuState.Continue)
                            {
                                gameState = GameState.Game;
                                player.SendMessage("Move using W, A, S, and D.");
                                Thread.Sleep(500);
                            }
                            else if (mainState == MainMenuState.NewGame)
                            {
                                Restart();
                                gameState = GameState.Game;
                                Thread.Sleep(500);
                            }
                        }
                        break;
                    }
                case GameState.Game:
                    {
                        // timer for animation - kat
                        timer -= gameTime.ElapsedGameTime.TotalSeconds;
                        //intro.PlayIntro(.5f);
                        intro.IsPlayed = true;
                        if (intro.IsPlayed == true)
                        {
                            //music.PlayAsMusic(.5f);
                            music2.PlayAsMusic(.5f);
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
                                bathRoom.Update(gameTime);
                                player.Update(gameTime, bathRoom.Objects);
                                break;
                            // win condition
                            case CurrentRoom.WinRoom:
                                winLose = 2;
                                break;
                        }
                        if (kbState.IsKeyDown(Keys.Tab))// && !prevKbState.IsKeyDown(Keys.Tab))
                        {
                            gameState = GameState.GMenu;
                            beeboop.PlayAsSoundEffect(.9f);
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
                        if (kbState.IsKeyDown(Keys.D4) && !prevKbState.IsKeyDown(Keys.D4))// || kbState.IsKeyDown(Keys.Escape) && !prevKbState.IsKeyDown(Keys.Escape)) // would like to make tab later but wasnt working
                        {
                            // close menu
                            //menuState = MenuState.Main;
                            /*gameState = GameState.Game;
                            prevKbState = kbState;
                            kbState = Keyboard.GetState();
                            */
                            // close game
                            SaveData.Save();
                            Exit();
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
            // restarts the game if R is pressed
            if (kbState.IsKeyDown(Keys.R) && prevKbState.IsKeyUp(Keys.R))
            {
                Restart();
            }
            if (messageDisplay.IsDrawing)
            {
                messageDisplay.Update(gameTime);
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
            uSpriteBatch.Begin(SpriteSortMode.BackToFront);

            // draws the mainmenu - kat
            if (gameState == GameState.MainMenu)
            {
                uSpriteBatch.Draw(mainMenu, mainMenuRect, Color.White);
                newGame.Draw(uSpriteBatch);
                continueGame.Draw(uSpriteBatch);
                uSpriteBatch.Draw(mainMenuCursor, FormatDraw(mainMenuCursorLoc), Color.White);
            }

            // calls the bedroom draw command - kat
            if (gameState == GameState.Game)
            {
                //draw the instructions
                uSpriteBatch.Draw(instructionOpen, FormatDraw(instructionOpenRect), Color.White);
                uSpriteBatch.Draw(instructionClose, FormatDraw(instructionCloseRect), Color.White);
                uSpriteBatch.Draw(instructionMove, FormatDraw(instructionMoveRect), Color.White);
                uSpriteBatch.Draw(instructionInteract, FormatDraw(instructionInteractRect), Color.White);
                //uSpriteBatch.Draw(instructionNuke, FormatDraw(instructionNukeRect), Color.White);


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
                        
                        break;
                    case CurrentRoom.Bathroom:
                        if (player.PlayerRect.Y < GraphicsDevice.Viewport.Bounds.Height / 2)
                        {
                            bathRoom.Draw(uSpriteBatch);
                            player.Draw(uSpriteBatch);
                        }
                        else
                        {
                            bathRoom.Draw(uSpriteBatch);
                            player.Draw(uSpriteBatch);
                        }
                        // TODO: update bathroom
                        break;
                }
                
            }
            
            if (gameState == GameState.GMenu)
            {
                //draw the instructions
                uSpriteBatch.Draw(instructionOpen, FormatDraw(instructionOpenRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
                uSpriteBatch.Draw(instructionClose, FormatDraw(instructionCloseRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
                uSpriteBatch.Draw(instructionMove, FormatDraw(instructionMoveRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
                uSpriteBatch.Draw(instructionInteract, FormatDraw(instructionInteractRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
                uSpriteBatch.Draw(instructionNuke, FormatDraw(instructionNukeRect), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);

                switch (activeRoom)
                {
                    case CurrentRoom.Bedroom:
                        bedRoom.Draw(uSpriteBatch);
                        break;
                    case CurrentRoom.Closet:
                        closetRoom.Draw(uSpriteBatch);
                        break;
                    case CurrentRoom.Bathroom:
                        bathRoom.Draw(uSpriteBatch);
                        break;
                }
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
                uSpriteBatch.DrawString(font, string.Format("{0}:{1}", gameTimerSeconds / 60, gameTimerSeconds % 60), new Vector2(0, 50), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
            }
            // Draw textbox
            //box.Draw(uSpriteBatch);

            // end spritebatch
            if (LightsOn == false && gameState != GameState.MainMenu)
            {
                uSpriteBatch.Draw(blacklight, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, .05f);
            }

            // draw lose things - kat
            if (winLose == 1) // lost
            {
                uSpriteBatch.Draw(loseScreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
            }

            // draw win things - kat
            if (winLose == 2) // win
            {
                uSpriteBatch.Draw(winScreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0);
            }

            if (messageDisplay.IsDrawing)
            {
                messageDisplay.Draw(uSpriteBatch);
            }

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
            music2 = new GameSound("Spook4-Spidershuffle", content);
            beeboop = new GameSound("phone-beep", content);

            font = Content.Load<SpriteFont>("Arial");
            menuFont = Content.Load<SpriteFont>("Courier12");
            courier36 = Content.Load<SpriteFont>("Courier36");

            messageDisplay = new PopUpManager(font);
            

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
            mainMenuCursor = Content.Load<Texture2D>("MainMenuCursor");
            mainMenuCursorLoc = new Rectangle(150, 610, mainMenuCursor.Width, mainMenuCursor.Height);
            // phone menu - kat
            startingPhoneState = Content.Load<Texture2D>("phoneMain0NEW");
            imagePhoneState = Content.Load<Texture2D>("phoneMain5");
            textPhoneState = Content.Load<Texture2D>("phoneMain7");

            // protag - kat

            faceUp = content.Load<Texture2D>("WalkCycleBack_00000");
            faceDown = content.Load<Texture2D>("WalkCycleFront_00000");
            faceRight = content.Load<Texture2D>("WalkCycleSide_Standing");
            faceRight1 = content.Load<Texture2D>("WalkCycleSide_00000");
            faceRight2 = content.Load<Texture2D>("WalkCycleSide_00001");
            faceRight3 = content.Load<Texture2D>("WalkCycleSide_00002");
            faceRight4 = content.Load<Texture2D>("WalkCycleSide_00003");
            faceRight5 = content.Load<Texture2D>("WalkCycleSide_00004");
            faceRight6 = content.Load<Texture2D>("WalkCycleSide_00005");
            faceRight7 = content.Load<Texture2D>("WalkCycleSide_00006");
            faceRight8 = content.Load<Texture2D>("WalkCycleSide_00007");
            faceRight9 = content.Load<Texture2D>("WalkCycleSide_00008");
            faceRight10 = content.Load<Texture2D>("WalkCycleSide_00009");
            faceRight11 = content.Load<Texture2D>("WalkCycleSide_00010");
            faceRight12 = content.Load<Texture2D>("WalkCycleSide_00011");
            faceRight13 = content.Load<Texture2D>("WalkCycleSide_00012");
            faceRight14 = content.Load<Texture2D>("WalkCycleSide_00013");
            faceRight15 = content.Load<Texture2D>("WalkCycleSide_00014");
            faceRight16 = content.Load<Texture2D>("WalkCycleSide_00015");
            faceDown1 = content.Load<Texture2D>("WalkCycleFront_00000");
            faceDown2 = content.Load<Texture2D>("WalkCycleFront_00001");
            faceDown3 = content.Load<Texture2D>("WalkCycleFront_00002");
            faceDown4 = content.Load<Texture2D>("WalkCycleFront_00003");
            faceDown5 = content.Load<Texture2D>("WalkCycleFront_00004");
            faceDown6 = content.Load<Texture2D>("WalkCycleFront_00005");
            faceDown7 = content.Load<Texture2D>("WalkCycleFront_00006");
            faceDown8 = content.Load<Texture2D>("WalkCycleFront_00007");
            faceUp1 = content.Load<Texture2D>("WalkCycleBack_00000");
            faceUp2 = content.Load<Texture2D>("WalkCycleBack_00001");
            faceUp3 = content.Load<Texture2D>("WalkCycleBack_00002");
            faceUp4 = content.Load<Texture2D>("WalkCycleBack_00003");
            faceUp5 = content.Load<Texture2D>("WalkCycleBack_00004");
            faceUp6 = content.Load<Texture2D>("WalkCycleBack_00005");
            faceUp7 = content.Load<Texture2D>("WalkCycleBack_00006");
            faceUp8 = content.Load<Texture2D>("WalkCycleBack_00007");
            protagTextureUp = new List<Texture2D>();
            protagTextureUp.Add(faceUp1);
            protagTextureUp.Add(faceUp2);
            protagTextureUp.Add(faceUp3);
            protagTextureUp.Add(faceUp4);
            protagTextureUp.Add(faceUp5);
            protagTextureUp.Add(faceUp6);
            protagTextureUp.Add(faceUp7);
            protagTextureUp.Add(faceUp8);
            protagTextureDown = new List<Texture2D>();
            protagTextureDown.Add(faceDown1);
            protagTextureDown.Add(faceDown2);
            protagTextureDown.Add(faceDown3);
            protagTextureDown.Add(faceDown4);
            protagTextureDown.Add(faceDown5);
            protagTextureDown.Add(faceDown6);
            protagTextureDown.Add(faceDown7);
            protagTextureDown.Add(faceDown8);
            protagTextureRight = new List<Texture2D>();
            protagTextureRight.Add(faceRight1);
            protagTextureRight.Add(faceRight2);
            protagTextureRight.Add(faceRight3);
            protagTextureRight.Add(faceRight4);
            protagTextureRight.Add(faceRight5);
            protagTextureRight.Add(faceRight6);
            protagTextureRight.Add(faceRight7);
            protagTextureRight.Add(faceRight8);
            protagTextureRight.Add(faceRight9);
            protagTextureRight.Add(faceRight10);
            protagTextureRight.Add(faceRight11);
            protagTextureRight.Add(faceRight12);
            protagTextureRight.Add(faceRight13);
            protagTextureRight.Add(faceRight14);
            protagTextureRight.Add(faceRight15);
            protagTextureRight.Add(faceRight16);

            blacklight = content.Load<Texture2D>("black light overlay");

            instructionOpen = content.Load<Texture2D>("INSTopen");
            instructionClose = content.Load<Texture2D>("INSTclose");
            instructionMove = content.Load<Texture2D>("INSTmove");
            instructionInteract = content.Load<Texture2D>("INSTinteract");
            instructionNuke = content.Load<Texture2D>("INSTnuke4");

            instructionOpenRect = new Rectangle(0, (972) - ((int)(5*1.75)*64), (int)(1.5*128),(int)(1.5*64));
            instructionCloseRect = new Rectangle(instructionOpenRect.X, instructionOpenRect.Y + instructionOpenRect.Height, instructionOpenRect.Width, instructionOpenRect.Height);
            instructionMoveRect = new Rectangle(instructionCloseRect.X, instructionCloseRect.Y + instructionCloseRect.Height, instructionCloseRect.Width, instructionCloseRect.Height);
            instructionInteractRect = new Rectangle(instructionMoveRect.X, instructionMoveRect.Y + instructionMoveRect.Height, instructionMoveRect.Width, instructionMoveRect.Height);
            instructionNukeRect = new Rectangle(instructionInteractRect.X, instructionInteractRect.Y + instructionInteractRect.Height, instructionInteractRect.Width, instructionInteractRect.Height);

            bedBG = content.Load<Texture2D>("backgroundFULL");
            closetBG = content.Load<Texture2D>("theclosetFULL");
            bathBG = content.Load<Texture2D>("thebathroomFULL");


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
        
        public static  ContentManager ContentMan { get { return content; } }
        public static bool LightsOn { get { return lightsOn; } set { lightsOn = value; } }


        public void GetSettings(Boolean firstTime)
        {
            if (!firstTime)
            {
                System.Diagnostics.Process.Start("..\\..\\..\\..\\..\\ExternalTool\\bin\\Debug\\ExternalTool", "dependent");
                Process extTool = Process.GetProcessesByName("ExternalTool")[0];
                extTool.WaitForExit();
            }
            //data = new SaveData();
            // initializes the bedroom
            // Caleb - writes appropriate data to file, will save later
            //data.WriteBedroom();
            // Caleb - reads GameObjects from the file, stores it in objects
            //objects = data.ReadBedroom();
            settingsData = SaveData.ReadSettings();
            timerMode = (bool)settingsData[0];
            if (timerMode)
            {
                gameTimerSeconds = (int)settingsData[1] * 60;
            }
            easyMode = (bool)settingsData[2];
            bobRossMode = (bool)settingsData[3];
            fullscreen = (Boolean)settingsData[4]; // Tom - Get whether or not the window is fullscreen
            if (fullscreen)
            {
                graphics.IsFullScreen = true;
                windowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                windowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            }
            else
            {
                windowWidth = (int)settingsData[5];
                windowHeight = (int)settingsData[6];
            }
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
            drawRatio.X = (float)windowWidth / NORM_WIDTH;
            drawRatio.Y = (float)windowHeight / NORM_HEIGHT;
            graphics.ApplyChanges();

        }

        public void Restart()
        {
            // returns palyer to bedroom
            activeRoom = CurrentRoom.Bedroom;
            //lights
            lightsOn = false;
            // restores the player position to what it was at the start of the game
            player.PlayerRect = new Rectangle(1728 / 2, 972 / 2 + 50, 96, 192);
            // clears the clue menu 
            Menu.pageClue = new Clue[7,4];
            Menu.cluesNum = 0;
            // TODO: restart more rooms when we get them
            bedRoom.ReenableClueObjects();
            closetRoom.ReenableClueObjects();
            bathRoom.ReenableClueObjects();
            SaveData.Restart();
            // TODO: reconfigure so that it assigns the custom timer value
            if (timerMode)
            {
                gameTimerSeconds = (int)settingsData[1] * 60;
            }
            player.MoveBounds = bedRoom.Bounds;
        }
    }
}