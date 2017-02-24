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
        private SpriteBatch spritesToDraw;
        private GameObject[] objectsInRoom;

        // TODO: Parameterized constructor, needs to take a collection of GameObjects as a param
        public Room(List<GameObject> roomObjs, GraphicsDevice graphics)
        {
            spritesToDraw = new SpriteBatch(graphics);
            objectsInRoom = roomObjs.ToArray();
        }
        // TODO: Update function, should go through the array of GameObjects and call all their update functions
        public void Update()
        {

        }
        // TODO: Draw method, should use spritesToDraw to Draw sprites for all enabled gameObjects in the room
        public void Draw()
        {

        }
        // TODO: Initialize method, cross-reference a list of booleans (save data?) and use it to get the state of objects
        public void Initialize()
        {

        }
    }
}