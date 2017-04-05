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
        // collection of all the rows of text
        List<string> textRows;
        // the first row drawn
        int startingRow = 0;
        // the string of all the text, may not be necessary
        string fullText = "";
        // number of rows that can be viewed at a time
        int viewLimit;
        // collection of text that is visible
        string[] visibleTextRows;
        // string of all the visible text
        string visibleText = "";
        // array of the words in fullText
        string[] words;
        SpriteFont font;
        // constructors
        // will parse text param into a list of TextRows
        public TextBox(Vector2 pos, string text, int charLimit, int vLimit, SpriteFont fontParam) : base(pos)
        {
            textRows = new List<string>();
            font = fontParam;
            viewLimit = vLimit;
            visibleTextRows = new string[viewLimit];
            string currRow = "";
            words = text.Split(' ');
            foreach (string s in words)
            {
                while (true)
                {
                    // if there is enough room on the row for the word, add it to the row
                    if (s.Length < charLimit - currRow.Length + 1)
                    {
                        currRow += s + " ";
                        break;
                    }
                    // else make a new row
                    else
                    {
                        currRow += '\n';
                        fullText += currRow;
                        textRows.Add(currRow);
                        currRow = "";
                    }
                }
            }
            // add the last row
            currRow += '\n';
            fullText += currRow;
            textRows.Add(currRow);
            currRow = "";
            /*foreach (char c in text)
            {
                currRow += c;
                if (currRow.Length == charLimit)
                {
                    currRow += '\n';
                    fullText += currRow;
                    textRows.Add(currRow);
                    currRow = "";
                }
            }
            // catches if there is an unfinished row
            if (currRow.Length != 0)
            {
                currRow += '\n';
                fullText += currRow;
                textRows.Add(currRow);
                currRow = "";
            }*/
        }
        // overridden Draw()
        public override void Draw(SpriteBatch spriteBatch)
        {
            // draw the selected rows from textRows by adding them to visibleTextRows
            for (int i = startingRow; i < (startingRow + viewLimit) && i < textRows.Count; i++)
            {
                visibleTextRows[i - startingRow] = textRows[i];
            }
            // create a string from visibleTextRows
            foreach (string s in visibleTextRows)
            {
                visibleText += s;
            }
            // draw
            spriteBatch.DrawString(font, visibleText, position, Color.White);
            // clear variable
            visibleText = "";
        }
        // Update Method, scrolls through the text
        public void Update(KeyboardState kbState, KeyboardState prevKbState)
        {
            // scrolls the textbox up or down based on key presses 
            if ((kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyUp(Keys.W)) && startingRow != 0)
            {
                startingRow--;
            }
            if ((kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyUp(Keys.S)) && startingRow != textRows.Count - viewLimit)
            {
                startingRow++;
            }
        }
    }
}
