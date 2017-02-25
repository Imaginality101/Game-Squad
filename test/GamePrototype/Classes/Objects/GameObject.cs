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

        }
        //TODO: Needs accessor properties for Sprite and Position
        //NOTE: CALEB - I'll try - I assume read only for sprite, and I will see id I
        // can have a set for Position that only affects the player
        
        // read only sprite property
        public Texture2D Sprite
        {
            get
            {
                return sprite;
            }
        }

        // property for position; can only be set if GameObject is player
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                // TODO: once the Player class is added, include this if statement:
                // if (this is Player)
                //{
                position = value;
                //} 
            }
        }


    }
}
