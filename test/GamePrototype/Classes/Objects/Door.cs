using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/*Workers: Declan
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
        CurrentRoom destination;
        CurrentRoom prevRoom;
        Vector2 origin;

        public Door(Texture2D txtr, Rectangle psRct,CurrentRoom targetRoom, Clue rqClue) : base(txtr, psRct)
        {
            doorText = txtr;
            doorRect = psRct;
            requiredClue = rqClue;
            destination = targetRoom;
            origin = new Vector2(1728 / 2, 972 / 2);

        }
        //cheatdoor
        public Door(Texture2D txtr, Rectangle psRct, CurrentRoom targetRoom) : base(txtr, psRct)
        {
            doorText = txtr;
            doorRect = psRct;
            destination = targetRoom;
            requiredClue = null;
            origin = new Vector2(1728 / 2, 972 / 2);

        }
        //clRct
        public Door(Texture2D txtr, Rectangle psRct, CurrentRoom targetRoom, Rectangle clRct) : base(txtr, psRct,clRct)
        {
            doorText = txtr;
            doorRect = psRct;
            destination = targetRoom;
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
                    Game1.activeRoom = destination;
                    Translocate(user, destination, prevRoom);

                }
                else
                {
                    Console.WriteLine("You don't have the right key.");
                    user.SendMessage("The door won't budge, and you don't have a key.");
                }
            }
            else
            {
                Game1.activeRoom = destination;
                Translocate(user, destination, prevRoom);
            }
            prevRoom = destination;

        }
        public void Translocate(Player user,CurrentRoom destination, CurrentRoom prevRoom)
        {
            if (destination == CurrentRoom.Closet)
            {
                user.X = (int)origin.X + 470;
                user.Y = (int)origin.Y - 20;
            }
            if (destination == CurrentRoom.Bathroom)// && prevRoom == CurrentRoom.Closet)
            {
                user.X = (int)origin.X +100;
                user.Y = (int)origin.Y + 200;
            }
            if (destination == CurrentRoom.Bedroom)// && prevRoom == CurrentRoom.Closet)
            {
                user.X = (int)origin.X - 620;
                user.Y = (int)origin.Y + 200;
            }/*
            if (destination == CurrentRoom.Bedroom && prevRoom == CurrentRoom.Bathroom)
            {
                //+ 350, (int)origin.Y - 530
                user.X = (int)origin.X + 35;
                user.Y = (int)origin.Y - 500;
            }
            else
            {
                user.X = (int)origin.X + 0;
                user.Y = (int)origin.Y + 0;
            }*/
        }

    }
}
