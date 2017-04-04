using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GamePrototype.Classes;
using GamePrototype.Classes.Objects;
using GamePrototype.Classes.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePrototype.Classes.Menu
{
    class Icon : MenuObject
    {
        // attribute
        Texture2D image;
        // constructor
        public Icon(Texture2D img, Vector2 pos) : base(pos)
        {
            image = img;
        }
    }
}
