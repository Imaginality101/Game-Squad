using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/*Workers: Tom
 * DisasterPiece Games
 * Door Class
 */

namespace GamePrototype.Classes.Objects
{

    class Door : Interactable
    {
        Texture2D doorText;
        Rectangle doorRect;
        Clue requiredClue;
        CurrentRoom wannago;
        Vector2 origin;

        public Door(Texture2D txtr, Rectangle psRct,CurrentRoom wenothere, Clue rqClue) : base(txtr, psRct)
        {
            doorText = txtr;
            doorRect = psRct;
            requiredClue = rqClue;
            wannago = wenothere;
            origin = new Vector2(1728 / 2, 972 / 2);

        }
        //cheatdoor
        public Door(Texture2D txtr, Rectangle psRct, CurrentRoom wenothere) : base(txtr, psRct)
        {
            doorText = txtr;
            doorRect = psRct;
            wannago = wenothere;
            requiredClue = null;
            origin = new Vector2(1728 / 2, 972 / 2);

        }

        // TODO: Class needs fleshing out, but its primary difference is that interacting
        // with it should somehow prompt the game to switch to another room. Tom's recommendation - custom event handlers. If someone wants to tackle that verbally raise your hand to me,
        // otherwise I'll do it when we get to Milestone 3 and have multiple rooms.
        public override void Interact(Player user)
        {
            // if there is a required clue
            if (requiredClue != null)
            {
                if (Enabled && Clue.Inventory.Contains(requiredClue))
                {
                    user.X = (int)origin.X;
                    user.Y = (int)origin.Y;
                    Game1.activeRoom = wannago;
                }
                else
                {
                    Console.WriteLine("You don't have the right key.");
                }
            }
            else
            {
                user.X = (int)origin.X;
                user.Y = (int)origin.Y;
                Game1.activeRoom = wannago;
            }
                
            
        }

    }
}
