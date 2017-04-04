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
    class TextRow : MenuObject
    {
        // attributes
        Vector2 position;
        string text;
        // constructor
        public TextRow(Vector2 pos, string t) : base(new Rectangle(pos.ToPoint(), Point.Zero))
        {
            text = t;
        }
    }
}
