using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GamePrototype.Classes.Objects
{
    class Player : GameObject, Tools.IAnimated
    {
        private Rectangle[] animFrames;
        private Vector2 moveQueue;
        // TODO: Update method override, should check player input and movement
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public void GetFrame()
        {
            // TODO: Method should figure out which frame is supposed to be active
        }

        public void LoadFrames()
        {
            // TODO: This method will need to be set up differently depending on individual images or spritesheets.
            // If we use sprite sheets we need it to take values for the number of animation sets, the number of frames in each set, and the pixel dimensions of each frame being pulled.

        }
    }
}
