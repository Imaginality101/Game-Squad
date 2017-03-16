using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GamePrototype.Classes.Objects
{
    enum PlayerDir { FaceDown, FaceUp, FaceLeft, FaceRight}
    enum PlayerState { Still, Moving, Touching}
    class Player : GameObject, Tools.IAnimated, Tools.IControlled
    {
        private Rectangle[][] animFrames; // source rectangles to be used in drawing the player
        private Vector2 moveQueue;
        private PlayerDir playerDirection; // get direction the player is facing
        private PlayerState playerState; // what the player is doing

        // TODO: Player constructor, should take the same sort of information as well as potentially a Menu object. We'd feed the overall Game's Menu into that.
        public Player(Texture2D spriteSheet, Point startingPos):base(spriteSheet, startingPos)
        {
            moveQueue = Vector2.Zero; // initialize moveQueue to a zero vector
            playerDirection = PlayerDir.FaceDown; // start out facing downwards for now
            playerState = PlayerState.Still; // not moving at first
        }
        // TODO: Update method override, should check player input and movement
        public override void Update(GameTime gameTime)
        {
            CheckInput();
            base.Update(gameTime);
        }
        public void GetFrame()
        {
            // TODO: Kat - Method should figure out which frame rectangle is supposed to be active and pass it to an attribute rect, which will need a property or accessor method
            
        }

        public void LoadFrames()
        {
            // TODO: Kat - This method will need to be set up differently depending on individual images or spritesheets.
            // If we use sprite sheets we need it to take values for the number of animation sets, the number of frames in each set, and the pixel dimensions of each frame being pulled.
            // With that said, because of how this stuff works (to my knowledge) we don't need to pull the image file itself as part of this. We can just use the player's sprite itself for references.
            // This method shouldn't actually need to touch the Texture2D, we're just using it to set up rects which will be used as sources in Draw() method.
        }

        public void CheckInput()
        {

            // TODO: This method should be called by the update function, this is where keyboard state checking should go.
            KeyboardState kbState = Keyboard.GetState();
            
        }

        public void CheckProximity(GameObject target)
        {
            // TODO: Not sure this is the best way to go about it so this may be moved or altered, but this would be used to figure out if
            // the player is close enough to a usable object to interact with it. If there are multiple close by, whichever is closer should be
            // the one interacted with.
        }

        public Boolean CheckBounds(Rectangle roomBounds)
        {
            // TODO: This method should check whether or not the player's collision rectangle is entirely inside
            // the accepted param rect roomBounds.
            return true;
        }

        public Boolean isColliding(GameObject target)
        {
            if ((target != this && (GlobalBounds.Intersects(target.GlobalBounds))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}