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
    class Player : GameObject
    {
        // attributes
        protected Vector2 direction;
        KeyboardState kState;

        // constructor
        public Player(Vector2 pos) : base(pos)
        {
            direction = Vector2.Zero;
        }

        // property
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public void Update()
        {
            // clear direction so the player only moves with input
            direction = Vector2.Zero;
            // get keyboard state
            kState = Keyboard.GetState();
            // change direction based on input
            if (kState.IsKeyDown(Keys.A))
            {
                direction += new Vector2(-1, 0);
            }
            if (kState.IsKeyDown(Keys.D))
            {
                direction += new Vector2(1, 0);
            }
            if (kState.IsKeyDown(Keys.W))
            {
                direction += new Vector2(0, -1);
            }
            if (kState.IsKeyDown(Keys.S))
            {
                direction += new Vector2(0, 1);
            }

            position += direction;
        }

        

    }
}
