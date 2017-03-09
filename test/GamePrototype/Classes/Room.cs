using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using GamePrototype.Classes.Objects;


namespace GamePrototype.Classes
{
    class Room
    {
        private Texture2D roomBG; // Texture for the room itself, draw this before any of the objects in it
        private Rectangle roomBounds; // The size of the room, will be used for bounds checks
        private List<GameObject> objectsInRoom; // as requested, list format
        // private Rectangle viewBounds; // Doesn't necessarily need to be used unless we go for resolution scalability
               
        // TODO: Parameterized constructor, needs to take a collection of GameObjects as a param
        public Room(Rectangle bounds, Texture2D bg)
        {
            roomBounds = bounds; // Declan - If you're setting up stuff, this
        }
        // TODO: Update function, should go through the array of GameObjects and call all their update functions
        public void Update(GameTime gameTime)
        {
            foreach(GameObject obj in objectsInRoom)
            {
                obj.Update(gameTime);
            }
        }
        // TODO: Kat - Draw method, take a SpriteBatch param and draw any enabled objects in the room by calling their draw methods with it
        public void Draw(SpriteBatch sprtBtch)
        {
            // draws the room texture image/background
            sprtBtch.Draw(roomBG, roomBounds, Color.White);

            // foreach gameobject in the list of the objects in the room
            foreach(GameObject gmBjct in objectsInRoom)
            {
                // if the GameObject is enabled
                if (gmBjct.Enabled == true)
                {
                    // calls the objects draw method
                    gmBjct.Draw(sprtBtch);
                }
            }
        }
        // TODO: Initialize method, should be used to tell which objects in the room are in what state after they're all loaded in using the constructor
        public void Initialize()
        {
            
        }

        // NOTE: Declan - This was moved from the constructor, so when you initialize the Room(s) make sure you remember
        // to then 
        public List<GameObject> Objects
        {
            get { return objectsInRoom; }
            set { objectsInRoom = value; }
        }
    }
}