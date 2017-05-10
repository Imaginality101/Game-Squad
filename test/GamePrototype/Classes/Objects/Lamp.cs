using GamePrototype.Classes.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePrototype.Classes.Tools
{
    class Lamp : Interactable
    {
        public Lamp(Texture2D txtr, Rectangle psRct, float depth = -1f) : base(txtr, psRct)
        {
            if (depth >= 0)
            {
                Depth = depth;
                FixedDepth = true;
            }
        }
        public override void Interact(Player user)
        {
            user.SendMessage("Used the lamp.");
            if (Game1.LightsOn == true)
            {
                Game1.LightsOn = false;
            }
            else if (Game1.LightsOn == false)
            {
                Game1.LightsOn = true;
            }
        }

        

    }
}
