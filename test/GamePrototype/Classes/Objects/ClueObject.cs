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
        public ClueObject(Texture2D txtr, Point posParam, Clue clGvn):base(txtr, posParam)
        {
            givenClue = clGvn;
        }
        // TODO: 
        public override void Interact(GameObject user)
        {
            
        }
    }
}
