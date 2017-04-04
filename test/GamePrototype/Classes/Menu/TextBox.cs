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
    class TextBox : MenuObject
    {
        // attributes
        Rectangle rect;
        List<TextRow> textRows;
        int startingRow = 0;
        // constructors
        public TextBox(Rectangle r, List<TextRow> rows) : base(r)
        {
            textRows = rows;
        }
    }
}
