using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using GamePrototype.Classes.Objects;
using Microsoft.Xna.Framework.Content;
using GamePrototype.Classes.Tools;
/*Workers: Kat, Tom, Declan, Caleb
 * DisasterPiece Games
 * Room Class
 * Note: Right now since our objective for Milestone 2 was a single room with the basic functionality, this class ended up more specialized towards that one room. For Milestone 3 that should be moved out of the class' intrinsic properties
 * and into the space of things we set up as we go for the sake of scaling up to 3 rooms.
 */

namespace GamePrototype.Classes
{

    class Room
    {
        Texture2D roomBG; // Texture for the room itself, draw this before any of the objects in it
        private Rectangle roomBounds; // The size of the room, will be used for bounds checks
        private Rectangle roomColl;// collision box for the room
        private List<GameObject> objectsInRoom; // as requested, list format
        Vector2 origin; //added: Delcan //Use: using this to center the texture of the room. I dont want to use the viewport bounds because it would scale the texture based on the window size. Id like to have the bg relatively static and then have the furniture relative to this texture.
        
        // private Rectangle viewBounds; // Doesn't necessarily need to be used unless we go for resolution scalability

        bool lightsOff;

        SpriteBatch spriteBatch;
        List<Texture2D> textureList;
        List<Rectangle> rectangleList;

        // TODO: Parameterized constructor, needs to take a collection of GameObjects as a param
        public Room(Texture2D bg, Rectangle roomCollisions)
        {
            origin = new Vector2(1728 / 2, 972 / 2);
            roomBG = bg;
            roomBounds = new Rectangle((int)origin.X - (1382 / 2), (int)origin.Y - (972 / 2), 1382, 972);
            roomColl = roomCollisions;
            lightsOff = true;
           
        }

        // TODO: Initialize method, should be used to tell which objects in the room are in what state after they're all loaded in using the constructor
        public void Initialize()
        {

        }

        // TODO: Update function, should go through the array of GameObjects and call all their update functions
        public void Update(GameTime gameTime)
        {
            if (objectsInRoom != null)
            {
                foreach (GameObject obj in objectsInRoom)
                {
                    obj.Update(gameTime);
                }
            }
        }
        // TODO: Kat - Draw method, take a SpriteBatch param and draw any enabled objects in the room by calling their draw methods with it
        public void Draw(SpriteBatch sprtBtch)
        {
            // draws the room texture image/background
            sprtBtch.Draw(roomBG, Game1.FormatDraw(roomBounds), Color.White);

            // foreach gameobject in the list of the objects in the room
            if (objectsInRoom != null)
            {
                // kat trying a different way
                for (int i = 0; i < objectsInRoom.Count; i++)
                {
                    objectsInRoom[i].Draw(sprtBtch);
                }
            }
        }
        // Caleb - Marks ClueObjects already gotten from previous games as picked up
        public void DisableSavedClueObjects()
        {
            string[] clues = SaveData.GetSaveFileData().Split(' ');
            foreach (GameObject go in objectsInRoom)
            {
                if (go is ClueObject)
                {
                    ClueObject co = (ClueObject)go;
                    foreach (string key in clues)
                    {
                        // checks the null case
                        if (key == "")
                        {
                            continue;
                        }
                        if (co.GivenClue == Clue.Clues[key])
                        {
                            co.Found = true;
                            if (co.OneTimeUse)
                            {
                                co.Enabled = false;
                            }
                        }
                    }
                }
            }
        }
        // Caleb - reenables ClueObjects when restarted
        public void ReenableClueObjects()
        {
            foreach (GameObject go in objectsInRoom)
            {
                if (go is ClueObject)
                {
                    ClueObject co = (ClueObject)go;
                    co.Found = false;
                    co.Enabled = true;
                }
            }
        }

        // NOTE: Declan - This was moved from the constructor, so when you initialize the Room(s) make sure you remember to
        public List<GameObject> Objects
        {
            get { return objectsInRoom; }
            set { objectsInRoom = value; }
        }

        public Rectangle Bounds// the actual bounds of the room
        {
            get { return roomBounds; }
        }
        public Rectangle CollisionBounds//Declan - bounds that hold the player in taking into account its sprite access point
        {
            get { return roomColl; }
        }

        
    }
}