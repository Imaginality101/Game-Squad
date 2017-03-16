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
using Microsoft.Xna.Framework.Content;


namespace GamePrototype.Classes
{

    class Room
    {
        Texture2D roomBG; // Texture for the room itself, draw this before any of the objects in it
        private Rectangle roomBounds; // The size of the room, will be used for bounds checks
        private List<GameObject> objectsInRoom; // as requested, list format
        Vector2 origin; //added: Delcan //Use: using this to center the texture of the room. I dont want to use the viewport bounds because it would scale the texture based on the window size. Id like to have the bg relatively static and then have the furniture relative to this texture.
        GraphicsDevice graphics;//Added: Declan //Uses: allows referanced to the graphics bounds
        ContentManager content;//Added Delcan //Uses: Allows for the room clas to load the content locally so that the main Game1 doen't become overrun with texture loading
        // private Rectangle viewBounds; // Doesn't necessarily need to be used unless we go for resolution scalability

        // TODO: Parameterized constructor, needs to take a collection of GameObjects as a param
        public Room(GraphicsDevice gd, ContentManager ctm)
        {
            content = ctm;
            graphics = gd;
            origin = new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);
            roomBG = content.Load<Texture2D>("backgroundFULL");
            roomBounds = new Rectangle((int)origin.X - (1382 / 2), (int)origin.Y - (972 / 2), 1382, 972);
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