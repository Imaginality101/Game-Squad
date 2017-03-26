using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GamePrototype.Classes.Objects
{
    class ClueObject : Interactable
    {
        Clue givenClue;

        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn) : base(txtr, psRct)
        {
            givenClue = clGvn;
        }
        // Caleb - temporary constructor for demoing interaction for milestone 2
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, string nm) : base(txtr, psRct, nm)
        {

        }
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean collision) : base(txtr, psRct, collision)
        {
            givenClue = clGvn;
        }
        // Caleb - Another temporary constructor for interaction
        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean collision, string nm) : base(txtr, psRct, collision, nm)
        {
            givenClue = clGvn;
        }
        // TODO: Interaction method
        public override void Interact(Player user)
        {
            
        }
    }
}
