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
        const float MSG_SCALE = 1.5f;
        const double DISPLAY_TIME = 3000;
        private SpriteFont font;
        private Rectangle drawRect;
        private Boolean drawing;
        private String message;
        private double timePassed;

        public PopUpManager(SpriteFont fnt)
        {
            font = fnt;
            drawRect = new Rectangle();
            timePassed = 0;
            message = "";
        }
        private Rectangle CalcRect(string str)
        {
            Vector2 textSize = font.MeasureString(str);
            int rectX = (1728 / 2) - (int)(textSize.X / 2);
            int rectY = 950 - (int)textSize.Y / 2;
            return Game1.FormatDraw(new Rectangle(rectX, rectY, (int)textSize.X, (int)textSize.Y));
        }

        public void Update(GameTime gameTime)
        {
            if (drawing)
            {
                timePassed += gameTime.ElapsedGameTime.TotalMilliseconds;
                if(timePassed >= DISPLAY_TIME)
                {
                    drawing = false;
                }
            }
        }
        public void Draw(SpriteBatch sprtBtch)
        {
            sprtBtch.DrawString(font, message, new Vector2(drawRect.X, drawRect.Y), Color.White, 0f, Vector2.Zero, Game1.drawRatio * MSG_SCALE, SpriteEffects.None, 0);
        }

        public void GetMessage(string msg)
        {
            message = msg;
            drawRect = CalcRect(message);
            timePassed = 0;
            drawing = true;
        }
        public Boolean IsDrawing
        {
            get { return drawing; }
        }
    }
}
