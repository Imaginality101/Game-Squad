using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InteractionAttempt
{
    class Interactable: GameObject
    {
        // attributes
        // reference to player
        Player player;
        // constructor
        public Interactable(Vector2 pos, Player plr) : base(pos)
        {
            player = plr;
        }

        // properties
        public double DistanceFromPlayer
        {
            get
            {
                return Vector2.Distance(this.position, player.Position);
            }
        }

        // proprietary Update() called by Game1.Update()
        public void Update()
        {

        }
    }
}
