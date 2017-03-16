using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GamePrototype.Classes.Objects
{

    class Door : Interactable
    {
        public Door(Texture2D txtr, Point posParam) : base(txtr, posParam)
        {
        }

        // TODO: Class needs fleshing out, but its primary difference is that interacting
        // with it should somehow prompt the game to switch to another room. Custom event handlers maybe?
        public override void Interact(GameObject user)
        {
            
        }

    }
}
