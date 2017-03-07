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
        private Rectangle roomBounds;
        private SpriteBatch spritesToDraw;
        private GameObject[] objectsInRoom;
        private Rectangle viewBounds;

        // TODO: Parameterized constructor, needs to take a collection of GameObjects as a param
        public Room(List<GameObject> roomObjs, GraphicsDevice graphics)
        {
            spritesToDraw = new SpriteBatch(graphics);
            objectsInRoom = roomObjs.ToArray();
            viewBounds = new Rectangle(); // make this rectangle a default one for now, we'll assign a proper identity to it in updating

        }
        // TODO: Update function, should go through the array of GameObjects and call all their update functions
        public void Update(GameTime gameTime)
        {
            foreach(GameObject obj in objectsInRoom)
            {
                obj.Update(gameTime);
            }
        }
        // TODO: Draw method, should use spritesToDraw to Draw sprites for all enabled gameObjects in the room
        public void Draw()
        {
            spritesToDraw.Begin();
            foreach(GameObject obj in objectsInRoom)
            {
                if(obj.Enabled)
                {
                    
                    // draw object here: we want to use a specific overload of the Draw function especially if with spritesheets, will elaborate when we get here
                }
            }
        }
        // TODO: Initialize method, should be used to tell which objects in the room are in what state after they're all loaded in using the constructor
        public void Initialize()
        {
            
        }
    }
}