using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
/*Workers: Caleb, Tom
 * DisasterPiece Games
 * ClueObject Class
 */
namespace GamePrototype.Classes.Objects
{
    class ClueObject : Interactable
    {
        Boolean onetimeUse;
        Boolean pickedUp;
        Clue givenClue;
        Clue requiredClue;
        Vector2 interactionPoint;

        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn) : base(txtr, psRct)
        {
            givenClue = clGvn;
            onetimeUse = false;
            pickedUp = false;
        }
        // Caleb - temporary constructor for demoing interaction for milestone 2
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean oneTime) : base(txtr, psRct)
        {
            givenClue = clGvn;
            onetimeUse = oneTime;
            pickedUp = false;
        }
        // same as above but with requiredClue
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean oneTime, Clue rqClue) : base(txtr, psRct)
        {
            givenClue = clGvn;
            onetimeUse = oneTime;
            requiredClue = rqClue;
            pickedUp = false;
        }
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean collision, Boolean oneTime) : base(txtr, psRct, collision)
        {
            givenClue = clGvn;
            onetimeUse = oneTime;
            pickedUp = false;
        }

        // Constructor overload for objects whose hitbox is sized differently from the sprite
        public ClueObject(Texture2D txtr, Rectangle psRct, Rectangle clRct, Clue clGvn, Boolean oneTime) : base(txtr, psRct, clRct)
        {
            givenClue = clGvn;
            onetimeUse = oneTime;
            pickedUp = false;
        }
        // Caleb - Another temporary constructor for interaction
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean collision, string nm, Boolean oneTime) : base(txtr, psRct, collision, nm)
        {
            givenClue = clGvn;
            onetimeUse = oneTime;
            pickedUp = false;
        }


        // property for point of interaction
        public Vector2 InteractionPoint
        {
            get { return interactionPoint; }
            set { interactionPoint = value; }
        }
        public Vector2 GetGlobalInteractPoint()
        {
            Vector2 location = GlobalBounds.Location.ToVector2() + interactionPoint;
            return location;
        }
        public Boolean Found
        {
            get { return pickedUp; }
            set { pickedUp = value; }
        }
        public Clue GivenClue
        {
            get
            {
                return givenClue;
            }
        }
        // TODO: Interaction method
        public override void Interact(Player user)
        {
            // INTERACTION FUNCTIONS HERE
            // if there is a required clue
            if (requiredClue != null)
            {
                if (Enabled && Clue.Inventory.Contains(requiredClue) && !pickedUp)
                {
                    Console.WriteLine(givenClue.ToString());
                    Clue.Inventory.Add(givenClue);
                    Menu.Menu.AddClue(givenClue);
                    pickedUp = true;
                }
                else
                {
                    Console.WriteLine("You don't have the required clue");
                }
            }
            // if there isn't a required clue
            else
            {
                if (Enabled && !pickedUp)
                {
                    Console.WriteLine(givenClue.ToString());
                    Clue.Inventory.Add(givenClue);
                    Menu.Menu.AddClue(givenClue);
                    pickedUp = true;
                }
                if (onetimeUse)
                {
                    Enabled = false;
                }
            }
        }
    }
}
