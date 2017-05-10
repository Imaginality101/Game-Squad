using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using GamePrototype;
using GamePrototype.Classes.Tools;
using Microsoft.Xna.Framework.Content;
using GamePrototype.Classes.Objects;
/*Workers: Kat, Tom, Caleb
* DisasterPiece Games
* Menu Class
*/
namespace GamePrototype.Classes.Menu
{
    enum Category { Main, Journal, Clues, Photos, Settings, Power }
    enum SelectedEntry { TopLeft, TopRight, BotLeft, BotRight, Upper, Lower }

    class Menu : Tools.IControlled
    {
        KeyboardState kbState;
        KeyboardState prevKbState;
        private Category activeMenu;
        private SelectedEntry selectedEntry;
        Clue openedClue; // so the menu knows if the player's chosen a clue to look at
        // deprecated, use Clue.Inventory
        //Dictionary<Clue, Boolean> clueList; // to keep track of
        // first index is the page on the menu, second index is which space it is on: 0 is top left, 1 is top right, 2 is bottom left, 3 is bottom right 
        public static Clue[,] pageClue = new Clue[7, 4];
        int cluePageIndex = 0;


        // icons
        /*
        Icon newsPaper;
        Icon bathroomKey;
        Icon closetKey;
        Icon oldPhoto;
        Icon newPhoto;
        Icon tenantDiary;
        Icon crazyPersonDiary;
        Icon receipt;
        Icon ring;
        Icon pendant;
        Icon bones;
        Icon jaggedKnife;
        Icon spaCoupon;
        Icon medicineBottle;
        Icon stickyNote;
        */
        // phone textures
        Texture2D mainPhoneMenu;
        Texture2D cluesPhoneMenu;
        Texture2D textPhoneMenu;
        // textboxes
        TextBox settingsTextBox;
        TextBox clueTextBox;
        // highlights the selected clue icon in the clue menu
        Texture2D clueCursor;
        // list of clues caleb says is useless
        List<Clue> clues;
        // font for displaying text
        SpriteFont menuFont;

        // kat - for drawing images
        int imageClueDraw = 0;

        // image menu box locations
        Rectangle box1 = new Rectangle(755, 230, 70, 100);
        Rectangle box2 = new Rectangle(845, 230, 70, 100);
        Rectangle box3 = new Rectangle(755, 380, 70, 100);
        Rectangle box4 = new Rectangle(845, 380, 70, 100);
        private GameSound blep;

        // Use this for the blown up images, its the size of the room, will maximize readibility hopefully, cannot currently select one to test it however
        Rectangle blownUpImage = new Rectangle((1728 / 2) - (1382 / 2), 0, 1382, 972);

        public Menu()
        {
            activeMenu = Category.Main;
            selectedEntry = SelectedEntry.TopRight;
            blep = new GameSound("phone-selection", Game1.ContentMan);

            // commented out because it is deprecated
            /*clueList = new Dictionary<Clue, Boolean>();
            foreach(Clue cl in Clue.Clues.Values)
            {
                clueList.Add(cl, false);
            }*/
        }
        // TODO: Load icons in Game1, pass them here in an array
        // possibly deprecated, will get images from clues when added to the inventory
        public void LoadContent(Texture2D main, Texture2D clues, Texture2D text, SpriteFont mFont, Texture2D cursor, List<object> settingsParam)
        {
            //clues = Clue.Inventory;
            //newsPaper = new Icon(nws, new Rectangle(755, 230, 70, 100)); // box 1 location
            //stickyNote = new Icon(stcky, new Rectangle(845, 230, 70, 100)); // box 2 location
            //tenantDiary = new Icon(tenant, new Rectangle(845, 380, 70, 100)); // box 3 location
            //crazyPersonDiary = new Icon(crazy, new Rectangle(0, 150, 70, 100));
            //newsPaper = new Icon(nws, new Rectangle(0, 0, 50, 50));
            //stickyNote = new Icon(stcky, new Rectangle(0, 50, 50, 50));
            mainPhoneMenu = main;
            cluesPhoneMenu = clues;
            textPhoneMenu = text;
            menuFont = mFont;
            List<object> settings = settingsParam;
            string settingsString = "";
            settingsString += "Timer mode: " + settings[0].ToString() + " ";
            settingsString += "Easy Mode: " + settings[2].ToString() + " ";
            settingsString += "Bob Ross mode: " + settings[3].ToString() + " ";
            if ((bool)settings[4])
            {
                settingsString += "Fullscreen enabled" + " ";
            }
            else
            {
                settingsString += "Resolution: " + settings[5].ToString() + " by " + settings[6].ToString() + " ";
            }
            settingsTextBox = new TextBox(new Vector2(760, 220), settingsString, 15, 15, mFont, new Rectangle(0, 0, 0, 0));
            clueCursor = cursor;
        }
        public void Update()
        {
            CheckInput();
        }

        public void CheckInput()
        {
            kbState = Keyboard.GetState();
            // pasted Game1 code here
            // TODO: update the phone menu
            // kat draws menu things 
            /*
            if (imageClueDraw > 0) // not workuing
            {
                if (kbState.IsKeyDown(Keys.Enter) == true && prevKbState.IsKeyUp(Keys.Enter) == true)
                {
                    imageClueDraw = 0;
                }
            }

            /*if (kbState.IsKeyDown(Keys.D1) && !prevKbState.IsKeyDown(Keys.D1))
            {
                // journal menu
                activeMenu = Category.Journal;
            }*/

            if (kbState.IsKeyDown(Keys.D2) && !prevKbState.IsKeyDown(Keys.D2))
            {
                // photo menu
                activeMenu = Category.Clues;
            }

            if (kbState.IsKeyDown(Keys.D3) && !prevKbState.IsKeyDown(Keys.D3))
            {
                // settings menu
                activeMenu = Category.Settings;
            }

            //if (kbState.IsKeyDown(Keys.D4) && !prevKbState.IsKeyDown(Keys.D4))
            //{
                // exit game code
                // ADD SAVE CODE FOR EXTERNAL TOOL HERE PLEASEEEE<3
                // activeMenu = Category.Power;
                // SaveData.Save();
                // Environment.Exit(0);
            //}

            // NOT WORKING RIGHT NOW --- Fixed it, you just needed a set of parenthesis around the state checks here - Tom
            if ((activeMenu == Category.Clues || activeMenu == Category.Photos || activeMenu == Category.Settings) && kbState.IsKeyDown(Keys.D1) && !prevKbState.IsKeyDown(Keys.D1))
            {
                // back to main menu
                activeMenu = Category.Main;
            }
            if (activeMenu == Category.Clues)
            {
                if (imageClueDraw == 0)
                {
                    switch (selectedEntry)
                    {
                        case SelectedEntry.TopLeft:
                            {
                                // moves between entries
                                if (kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyUp(Keys.S))
                                {
                                    selectedEntry = SelectedEntry.BotLeft;
                                    blep.PlayAsSoundEffect(.9f);
                                }
                                else if (kbState.IsKeyDown(Keys.D) && prevKbState.IsKeyUp(Keys.D))
                                {
                                    selectedEntry = SelectedEntry.TopRight;
                                    blep.PlayAsSoundEffect(.9f);
                                }
                                // handles scrolling
                                else if (cluePageIndex != 0 && (kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyUp(Keys.W)))
                                {
                                    selectedEntry = SelectedEntry.BotLeft;
                                    blep.PlayAsSoundEffect(.9f);
                                    cluePageIndex--;
                                }
                                break;
                            }
                        case SelectedEntry.TopRight:
                            {
                                // moves between entries
                                if (kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyUp(Keys.S))
                                {
                                    selectedEntry = SelectedEntry.BotRight;
                                    blep.PlayAsSoundEffect(.9f);
                                }
                                else if (kbState.IsKeyDown(Keys.A) && prevKbState.IsKeyUp(Keys.A))
                                {
                                    selectedEntry = SelectedEntry.TopLeft;
                                    blep.PlayAsSoundEffect(.9f);
                                }
                                // handles scrolling
                                else if (cluePageIndex != 0 && (kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyUp(Keys.W)))
                                {
                                    selectedEntry = SelectedEntry.BotRight;
                                    blep.PlayAsSoundEffect(.9f);
                                    cluePageIndex--;
                                }
                                break;
                            }
                        case SelectedEntry.BotLeft:
                            {
                                // moves between entries
                                if (kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyUp(Keys.W))
                                {
                                    selectedEntry = SelectedEntry.TopLeft;
                                    blep.PlayAsSoundEffect(.9f);
                                }
                                else if (kbState.IsKeyDown(Keys.D) && prevKbState.IsKeyUp(Keys.D))
                                {
                                    selectedEntry = SelectedEntry.BotRight;
                                    blep.PlayAsSoundEffect(.9f);
                                }
                                // handles scrolling
                                else if (cluePageIndex != 6 && (kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyUp(Keys.S)))
                                {
                                    selectedEntry = SelectedEntry.TopLeft;
                                    blep.PlayAsSoundEffect(.9f);
                                    cluePageIndex++;
                                }
                                break;
                            }
                        case SelectedEntry.BotRight:
                            {
                                // moves between entries
                                if (kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyUp(Keys.W))
                                {
                                    selectedEntry = SelectedEntry.TopRight;
                                    blep.PlayAsSoundEffect(.9f);
                                }
                                else if (kbState.IsKeyDown(Keys.A) && prevKbState.IsKeyUp(Keys.A))
                                {
                                    selectedEntry = SelectedEntry.BotLeft;
                                    blep.PlayAsSoundEffect(.9f);
                                }
                                // handles scrolling
                                else if (cluePageIndex != 6 && (kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyUp(Keys.S)))
                                {
                                    selectedEntry = SelectedEntry.TopRight;
                                    blep.PlayAsSoundEffect(.9f);
                                    cluePageIndex++;
                                }
                                break;
                            }
                    }
                }
                if (kbState.IsKeyDown(Keys.Enter) && prevKbState.IsKeyUp(Keys.Enter))
                {
                    if (imageClueDraw > 0)
                    {
                        imageClueDraw = 0;
                    }
                    else
                    {
                        switch (selectedEntry)
                        {
                            case SelectedEntry.TopLeft:
                                if (pageClue[cluePageIndex, 0] != null)
                                {
                                    if (pageClue[cluePageIndex, 0].BlownClueImage != null)
                                    {
                                        imageClueDraw = 1;
                                    }
                                    else
                                    {
                                        clueTextBox = new TextBox(new Vector2(760, 220), pageClue[cluePageIndex, 0].ToString(), 15, 15, menuFont, new Rectangle(0, 0, 0, 0));
                                        activeMenu = Category.Journal;
                                    }
                                }
                                break;
                            case SelectedEntry.TopRight:
                                if (pageClue[cluePageIndex, 1] != null)
                                {
                                    if (pageClue[cluePageIndex, 1].BlownClueImage != null)
                                    {
                                        imageClueDraw = 2;
                                    }
                                    else
                                    {
                                        clueTextBox = new TextBox(new Vector2(760, 220), pageClue[cluePageIndex, 1].ToString(), 15, 15, menuFont, new Rectangle(0, 0, 0, 0));
                                        activeMenu = Category.Journal;
                                    }
                                }
                                break;
                            case SelectedEntry.BotLeft:
                                if (pageClue[cluePageIndex, 2] != null)
                                {
                                    if (pageClue[cluePageIndex, 2].BlownClueImage != null)
                                    {
                                        imageClueDraw = 3;
                                    }
                                    else
                                    {
                                        clueTextBox = new TextBox(new Vector2(760, 220), pageClue[cluePageIndex, 2].ToString(), 15, 15, menuFont, new Rectangle(0, 0, 0, 0));
                                        activeMenu = Category.Journal;
                                    }
                                }
                                break;
                            case SelectedEntry.BotRight:
                                if (pageClue[cluePageIndex, 3] != null)
                                {
                                    if (pageClue[cluePageIndex, 3].BlownClueImage != null)
                                    {
                                        imageClueDraw = 4;
                                    }
                                    else
                                    {
                                        clueTextBox = new TextBox(new Vector2(760, 220), pageClue[cluePageIndex, 3].ToString(), 15, 15, menuFont, new Rectangle(0, 0, 0, 0));
                                        activeMenu = Category.Journal;
                                    }
                                }
                                break;
                        }
                    }
                }
                // stops drawing the image clue when exiting the clues menu
                if ((kbState.IsKeyDown(Keys.D2) && prevKbState.IsKeyUp(Keys.D2)) || (kbState.IsKeyDown(Keys.D3) && prevKbState.IsKeyUp(Keys.D3)) || (kbState.IsKeyDown(Keys.D4) && prevKbState.IsKeyUp(Keys.D4)))
                {
                    imageClueDraw = 0;
                }
            }
            prevKbState = kbState;
        }
        // TODO: add icon to 2D array when clue is collected
        public static void AddClue(Clue addedClue)
        {
            // find the first empty space in pageClue

            // outer loop for pageClue array
            for (int i = 0; i < 7; i++)
            {
                // inner loop for pageClue array
                for (int j = 0; j < 4; j++)
                {
                    if (pageClue[i, j] == null)
                    {
                        // now we have the indexes of the final entry
                        // handle special case where j is 3
                        /*if (j == 3)
                        {
                            i++;
                            j = 0;
                            pageClue[i, j] = addedClue;
                        }
                        else*/
                        //{

                        pageClue[i, j] = addedClue;
                        //}
                        return;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // pasted Game1 code here
            if (activeMenu == Category.Main)
            {
                //bedRoom.Draw(uSpriteBatch);
                spriteBatch.Draw(mainPhoneMenu, Game1.FormatDraw(new Rectangle(300, 0, 1200, 1000)), Color.White);
            }
            if (activeMenu == Category.Journal)
            {
                //bedRoom.Draw(uSpriteBatch);
                spriteBatch.Draw(textPhoneMenu, Game1.FormatDraw(new Rectangle(300, 0, 1200, 1000)), Color.White);
                if (clueTextBox != null)
                {
                    clueTextBox.Draw(spriteBatch);
                }
            }
            if (activeMenu == Category.Settings)
            {
                //bedRoom.Draw(uSpriteBatch);
                spriteBatch.Draw(textPhoneMenu, Game1.FormatDraw(new Rectangle(300, 0, 1200, 1000)), Color.White);
                // Draw textbox
                settingsTextBox.Draw(spriteBatch);
            }
            if (activeMenu == Category.Clues)
            {
                spriteBatch.Draw(cluesPhoneMenu, Game1.FormatDraw(new Rectangle(300, 0, 1200, 1000)), Color.White);
                // draw the clue cursor based on the selected entry
                switch (selectedEntry)
                {
                    case SelectedEntry.TopLeft:
                        spriteBatch.Draw(clueCursor, Game1.FormatDraw(box1), new Color(Color.Black, 50f));
                        break;
                    case SelectedEntry.TopRight:
                        spriteBatch.Draw(clueCursor, Game1.FormatDraw(box2), new Color(Color.Black, 50f));
                        break;
                    case SelectedEntry.BotLeft:
                        spriteBatch.Draw(clueCursor, Game1.FormatDraw(box3), new Color(Color.Black, 50f));
                        break;
                    case SelectedEntry.BotRight:
                        spriteBatch.Draw(clueCursor, Game1.FormatDraw(box4), new Color(Color.Black, 50f));
                        break;
                }
                // will draw the clue icons
                // inner loop for pageClue array
                for (int j = 0; j < 4; j++)
                {
                    Clue curr = pageClue[cluePageIndex, j];
                    if (curr != null)
                    {
                        switch (j) // why in god's name is this where we decide to use switch statements - Tom
                        {
                            case 0:
                                spriteBatch.Draw(curr.ClueImage, Game1.FormatDraw(box1), Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(curr.ClueImage, Game1.FormatDraw(box2), Color.White);
                                break;
                            case 2:
                                spriteBatch.Draw(curr.ClueImage, Game1.FormatDraw(box3), Color.White);
                                break;
                            case 3:
                                spriteBatch.Draw(curr.ClueImage, Game1.FormatDraw(box4), Color.White);
                                break;
                        }
                    }
                }
                

                if (imageClueDraw > 0)
                {
                    spriteBatch.Draw(pageClue[cluePageIndex, imageClueDraw - 1].BlownClueImage, Game1.FormatDraw(blownUpImage), Color.White);
                }

                /*if (Clue.Inventory.Contains(Clue.Clues["News1"]) || Clue.Inventory.Contains(Clue.Clues["News2"]) || Clue.Inventory.Contains(Clue.Clues["News3"]) || Clue.Inventory.Contains(Clue.Clues["News4"]) || Clue.Inventory.Contains(Clue.Clues["News5"]))
                {
                    newsPaper.Draw(spriteBatch);
                }
                if (Clue.Inventory.Contains(Clue.Clues["StickyNote"]))
                {
                    stickyNote.Draw(spriteBatch);
                }
                if (Clue.Inventory.Contains(Clue.Clues["TenantDiary1"]) || Clue.Inventory.Contains(Clue.Clues["TenantDiary2"]) || Clue.Inventory.Contains(Clue.Clues["TenantDiary3"]))
                {
                    tenantDiary.Draw(spriteBatch);
                }
                }
            
            if (activeMenu == Category.Settings)
            {
                //bedRoom.Draw(uSpriteBatch);
                spriteBatch.Draw(textPhoneMenu, Game1.FormatDraw(new Rectangle(300, 0, 1200, 1000)), Color.White);
                // Draw textbox
                settingsTextBox.Draw(spriteBatch);
            }
            
            if (activeMenu == Category.Power)
            {
                //bedRoom.Draw(uSpriteBatch);
            }
            // end of pasted code
            /*
            if (activeMenu == Category.Main)
            {
                //bedRoom.Draw(spriteBatch);
                spriteBatch.Draw(mainPhoneMenu, new Rectangle(300, 0, 1200, 1000), Color.White);
            }
            if (activeMenu == Category.Journal)
            {
                //bedRoom.Draw(uSpriteBatch);
                spriteBatch.Draw(textPhoneMenu, new Rectangle(300, 0, 1200, 1000), Color.White);
            }
            if (activeMenu == Category.Photos)
            {
                //bedRoom.Draw(uSpriteBatch);
                spriteBatch.Draw(cluesPhoneMenu, new Rectangle(300, 0, 1200, 1000), Color.White);
            }
            if (activeMenu == Category.Settings)
            {
                //bedRoom.Draw(uSpriteBatch);
                spriteBatch.Draw(textPhoneMenu, new Rectangle(300, 0, 1200, 1000), Color.White);
                // Draw textbox
                settingsTextBox.Draw(spriteBatch);
            }
            
            if (Clue.Inventory.Contains(Clue.Clues["News1"]) || Clue.Inventory.Contains(Clue.Clues["News2"]) || Clue.Inventory.Contains(Clue.Clues["News3"]) || Clue.Inventory.Contains(Clue.Clues["News4"]) || Clue.Inventory.Contains(Clue.Clues["News5"]))
            {
                newsPaper.Draw(spriteBatch);
            }
            if (Clue.Inventory.Contains(Clue.Clues["StickyNote"]))
            {
                stickyNote.Draw(spriteBatch);
            }
            if (Clue.Inventory.Contains(Clue.Clues["TenantDiary1"]) || Clue.Inventory.Contains(Clue.Clues["TenantDiary2"]) || Clue.Inventory.Contains(Clue.Clues["TenantDiary3"]))
            {
                tenantDiary.Draw(spriteBatch);
            }
            if (Clue.Inventory.Contains(Clue.Clues["CrazyDiary1"]) || Clue.Inventory.Contains(Clue.Clues["CrazyDiary2"]) || Clue.Inventory.Contains(Clue.Clues["CrazyDiary3"]))
            {
                crazyPersonDiary.Draw(spriteBatch);
            }
            */
            }
        }

        // TODO: Menu class will need an update and draw method, should check for input
        // Needs to have a way of tracking which menu item it's on, we may also need a more categorical version of this class to hold other menus
        // If the player hits whatever the method of selection is, it should call an Open() method of the menu item highlighted
        // The menu view itself also needs a property and attribute to check if it's enabled, as well as an Open method for itself which toggles that on/Close method for off
        // Other than the main view of the phone itself and maybe settings, menus should use lists for content since as the player finds clues they go there
        // Implements IControlled, use this to check user input while in the menu. Whoever does player updating should make sure they check if the menu is open before checking for input.
        // - Tom
    }
}
