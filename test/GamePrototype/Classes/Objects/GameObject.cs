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

    }
}
