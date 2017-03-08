using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InteractionAttempt
{
    class GameObject
    {
        // attributes
        protected Vector2 position;
        protected Texture2D image;
        // constructor
        public GameObject(Vector2 pos)
        {
            position = pos;
        }

        public void LoadContent(Texture2D img)
        {
            image = img;
        }
        // does what the Game1.Draw() would do, called by Game1.Draw()
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(image, position, Color.White);
            spriteBatch.End();
        }
    }
}
