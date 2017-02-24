using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public void Update()
        {

        }
        // TODO: Draw method, should use spritesToDraw to Draw sprites for all enabled gameObjects in the room
        public void Draw()
        {
            spritesToDraw.Begin();
            foreach(GameObject obj in objectsInRoom)
            {
                if(obj.Enabled)
                {

                }
            }
        }
        // TODO: Initialize method, cross-reference a list of booleans (save data?) and use it to get the state of objects
        public void Initialize()
        {

        }
    }
}