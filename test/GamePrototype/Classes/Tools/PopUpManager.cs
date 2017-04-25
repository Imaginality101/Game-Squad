using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Tom - Popup manager to be worked on for milestone 4. Will accomplish the migration of messages to the player ("you can't use this," "found a clue," etc) away from a console output to a proper ingame display.
namespace GamePrototype.Classes.Tools
{
    class PopUpManager
    {
        private SpriteFont font;
        private Rectangle drawRect;

        public PopUpManager(SpriteFont fnt)
        {
            font = fnt;
        }
        private Rectangle CalcRect()
        {
            return new Rectangle(0, 0, 0, 0);
        }

        public void Draw(SpriteBatch sprtBtch)
        {
            
        }
    }
}
