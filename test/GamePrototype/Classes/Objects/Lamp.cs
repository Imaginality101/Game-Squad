﻿using GamePrototype.Classes.Objects;
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
        private Vector2 interactionPoint;

        public Lamp(Texture2D txtr, Rectangle psRct) : base(txtr, psRct)
        {

        }
        public override void Interact(Player user)
        {
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