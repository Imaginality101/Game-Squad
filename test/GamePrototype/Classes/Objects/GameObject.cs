using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GamePrototype.Classes.Objects
{
    class GameObject
    {
        private bool enabled;
        private Texture2D sprite;
        private Vector2 position;
        private Rectangle hitBox;
        
        public GameObject()
        {
            sprite = null;
            position = new Vector2(0, 0);
            
        }

        public GameObject(Texture2D txtr, Vector2 strtPs)
        {
            sprite = txtr;
            position = strtPs;
            hitBox = sprite.Bounds;
        }

        // TODO: Update function, might not do much for base objects/furniture but needs to be overridden by special cases
        public virtual void Update(GameTime gameTime)
        {

        }
        //TODO: Needs accessor properties for Sprite and Position
        public Boolean Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
        public Texture2D Sprite
        {
            get { return sprite; }
        }
        
    }
}
