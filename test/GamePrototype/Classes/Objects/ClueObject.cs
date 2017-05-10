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


        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, float depth = -1) : base(txtr, psRct)
        {
            givenClue = clGvn;
            onetimeUse = false;
            pickedUp = false;
            if (depth >= 0)
            {
                Depth = depth;
                FixedDepth = true;
            }
        }
        // Caleb - temporary constructor for demoing interaction for milestone 2
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean oneTime, float depth = -1) : base(txtr, psRct)
        {
            givenClue = clGvn;
            onetimeUse = oneTime;
            pickedUp = false;
            if (depth >= 0)
            {
                Depth = depth;
                FixedDepth = true;
            }
        }
        // same as above but with requiredClue
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean oneTime, Clue rqClue, float depth = -1) : base(txtr, psRct)
        {
            givenClue = clGvn;
            onetimeUse = oneTime;
            requiredClue = rqClue;
            pickedUp = false;
            if (depth >= 0)
            {
                Depth = depth;
                FixedDepth = true;
            }
        }
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean collision, Boolean oneTime, Clue rqClue, float depth = -1) : base(txtr, psRct, collision)
        {
            givenClue = clGvn;
            onetimeUse = oneTime;
            requiredClue = rqClue;
            pickedUp = false;
            if (depth >= 0)
            {
                Depth = depth;
                FixedDepth = true;
            }
        }
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean collision, Boolean oneTime, float depth = -1) : base(txtr, psRct, collision)
        {
            givenClue = clGvn;
            onetimeUse = oneTime;
            pickedUp = false;
            if (depth >= 0)
            {
                Depth = depth;
                FixedDepth = true;
            }
        }

        // Constructor overload for objects whose hitbox is sized differently from the sprite
        public ClueObject(Texture2D txtr, Rectangle psRct, Rectangle clRct, Clue clGvn, Boolean oneTime, float depth = -1) : base(txtr, psRct, clRct)
        {
            givenClue = clGvn;
            onetimeUse = oneTime;
            pickedUp = false;
            if(depth >= 0)
            {
                Depth = depth;
                FixedDepth = true;
            }
        }
        // Caleb - Another temporary constructor for interaction
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean collision, string nm, Boolean oneTime, float depth = -1) : base(txtr, psRct, collision, nm)
        {
            givenClue = clGvn;
            onetimeUse = oneTime;
            pickedUp = false;
            if (depth >= 0)
            {
                Depth = depth;
                FixedDepth = true;
            }
        }



        public Boolean Found
        {
            get { return pickedUp; }
            set { pickedUp = value; }
        }
        public Boolean OneTimeUse
        {
            get
            {
                return onetimeUse;
            }
            set
            {
                onetimeUse = value;
            }
        }
        public Clue GivenClue
        {
            get
            {
                return givenClue;
            }
        }

        public override void Draw(SpriteBatch sprtBtch)
        {
            base.RequiredClue = requiredClue;
            base.Draw(sprtBtch);
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
                    user.SendMessage("You found a clue.");
                    pickedUp = true;
                    
                }
                else
                {
                    //Console.WriteLine("You don't have the required clue");
                    user.SendMessage("You look, but find nothing of interest.");
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
                    user.SendMessage("You found a clue.");
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
