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
// deprecated do not use
namespace GamePrototype.Classes.Menu
{
    class TextRow : MenuObject
    {
        string text;
        SpriteFont font;
        // constructor
        public TextRow(Vector2 posi, string t, SpriteFont fontParam, Rectangle pos) : base(pos)
        {
            text = t;
            font = fontParam;
        }
        // overridden Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, Color.White);
        }
    }
}
