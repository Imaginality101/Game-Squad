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

        public ClueObject(Texture2D txtr, Rectangle psRct, Clue clGvn, Boolean collision) : base(txtr, psRct, collision)
        {
            givenClue = clGvn;
        }
        // TODO: Interaction method
        public override void Interact(Player user)
        {
            
        }
    }
}
