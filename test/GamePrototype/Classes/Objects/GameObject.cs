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
        private Rectangle positionRect;
        // holds  name of texture; will be used later in Game1.LoadContent()
        string spriteName;
        public GameObject()
        {
            sprite = null;
            positionRect = new Rectangle();
        }

        public GameObject(Texture2D txtr, Point pos)
        {
            sprite = txtr;
            positionRect = new Rectangle(pos.X, pos.Y, sprite.Width, sprite.Height);
            enabled = true;
        }
        // Caleb - GameObject constructor with boolean "enabled"
        public GameObject(bool isEnabled, string txtrName, Point pos)
        {
            enabled = isEnabled;
            spriteName = txtrName;
            positionRect = new Rectangle(pos.X, pos.Y, sprite.Width, sprite.Height);
        }

        // TODO: Update function, might not do much for base objects/furniture but needs to be overridden by special cases
        // for cosmetic changes like the blacklight lamp, this method should check if blacklight mode is on and switch the sprite (we'll get there)
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
        
        // Position properties
        public int X
        {
            get { return positionRect.X; }
            set { positionRect.X = value; }
        }

        public int Y
        {
            get { return positionRect.Y; }
            set { positionRect.Y = value; }
        }

        public Rectangle GlobalBounds
        {
            get { return positionRect; }
        }
        // Origin property - will help with proximity checks for interactable objects, returns the global position of the sprite's center
        public Vector2 SpriteOrigin
        {
            get { return new Vector2(positionRect.X + sprite.Width / 2, positionRect.Y + sprite.Height / 2); }
        }

        // TODO: Kat - Draw method taking in a SpriteBatch param, just like how it was handled in HW2
        public void Draw(SpriteBatch sprtBtch)
        {
            // draws the object
            sprtBtch.Draw(sprite, positionRect, null, Color.White, 0f, SpriteOrigin, SpriteEffects.None, 0f);
        }
    }
}