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
            throw new NotImplementedException();
        }
    }
}
