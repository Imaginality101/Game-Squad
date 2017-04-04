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
        Clue givenClue;


        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn) : base(txtr, psRct)
        {
            givenClue = clGvn;
            onetimeUse = false;
        }
        // Caleb - temporary constructor for demoing interaction for milestone 2
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, string nm) : base(txtr, psRct, nm)
        {
            givenClue = clGvn;
            onetimeUse = false;
        }
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean collision) : base(txtr, psRct, collision)
        {
            givenClue = clGvn;
            onetimeUse = true;
        }

        // Constructor overload for objects whose hitbox is sized differently from the sprite
        public ClueObject(Texture2D txtr, Rectangle psRct, Rectangle clRct, Clue clGvn) : base(txtr, psRct, clRct)
        {
            givenClue = clGvn;
            onetimeUse = true;
        }
        // Caleb - Another temporary constructor for interaction
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean collision, string nm) : base(txtr, psRct, collision, nm)
        {
            givenClue = clGvn;
            onetimeUse = true;
        }

        // TODO: Interaction method
        public override void Interact(Player user)
        {
            // INTERACTION FUNCTIONS HERE
            if (Enabled)
            {
                Console.WriteLine(givenClue.ToString());
                Clue.Inventory.Add(givenClue);
            }
            if (onetimeUse)
            {

                Enabled = false;

            }
        }
    }
}
