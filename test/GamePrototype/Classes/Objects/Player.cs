﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GamePrototype.Classes.Objects
{
    enum PlayerDir { FaceDown, WalkDown, FaceUp, WalkUp, FaceLeft, WalkLeft, FaceRight, WalkRight}
    class Player : GameObject, Tools.IAnimated, Tools.IControlled
    {
        KeyboardState kbState;
        GameObject flaggedInteractable;
        private Rectangle[][] animFrames; // source rectangles to be used in drawing the player
        private Rectangle moveBounds;
        private Vector2 moveQueue;
        private PlayerDir playerDirection; // get direction the player is facing

        // TODO: Player constructor, should take the same sort of information as well as potentially a Menu object. We'd feed the overall Game's Menu into that.
        public Player(Texture2D spriteSheet, Point startingPos, Rectangle bounds):base(spriteSheet, startingPos)
        {
            moveQueue = Vector2.Zero; // initialize moveQueue to a zero vector
            playerDirection = PlayerDir.FaceDown; // start out facing downwards for now
            moveBounds = bounds;
        }
        // TODO: Update method override, should check player input and movement
        public override void Update(GameTime gameTime)
        {
            CheckInput(); // first get input to adjust movement queueing
            Move(gameTime);
            CheckBounds(moveBounds);

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

            moveQueue = Vector2.Zero; // reset moveQueue every update

            kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Keys.Up))
            {
                moveQueue.Y -= 10;
            }
            if (kbState.IsKeyDown(Keys.Down))
            {
                moveQueue.Y += 10;
            }
            if (kbState.IsKeyDown(Keys.Left))
            {
                moveQueue.X -= 10;
            }
            if (kbState.IsKeyDown(Keys.Right))
            {
                moveQueue.X += 10;
            }
        }

        public void Move(GameTime gameTime)
        {
            X += (int)(moveQueue.X * gameTime.ElapsedGameTime.Seconds);
            Y += (int)(moveQueue.Y * gameTime.ElapsedGameTime.Seconds);

            CheckBounds(moveBounds);
        }
        public float CheckProximity(GameObject target)
        {
            // This method will return the length of a vector between the two objects' global origin coordinates
            Vector2 difference = SpriteOrigin - target.SpriteOrigin;
            return Math.Abs(difference.Length());
        }

        public void FlagInteractables(GameObject[] targets)
        {
            Interactable closest = null; // use this temporary instance of the object to track which interactable in the room is closest to the player
            float minDistance = moveBounds.Right; // ideally nothing should be interactable beyond the width of how far the player can move so use this as the default
            foreach(GameObject obj in targets)
            {
                if(obj is Interactable) // if the object is usable check its proximity
                {
                    if (minDistance >= CheckProximity(obj)) // looping through this will find which interactable is the closest to the player
                    {
                        minDistance = CheckProximity(obj);
                        closest = (Interactable)obj;
                    }
                    else if (((Interactable)obj).Usable == true)
                    {
                        ((Interactable)obj).Usable = false;
                        flaggedInteractable = null;
                    }
                }
                if (closest != null && minDistance <= ((Sprite.Height / 2) + 20)) // if something was found in a reasonable proximity
                {
                    closest.Usable = true;
                    flaggedInteractable = closest;
                }
            }
        }
        public void CheckBounds(Rectangle roomBounds)
        {
            // TODO: This method should check whether or not the player's collision rectangle is entirely inside
            // the accepted param rect roomBounds. Correct any discrepancies.
            if (GlobalBounds.Left < roomBounds.Left)
            {
                X += (roomBounds.Left - GlobalBounds.Left + 5);
                Console.WriteLine("Left");
            }
            else if (GlobalBounds.Right > roomBounds.Right)
            {
                X += (roomBounds.Right - GlobalBounds.Right - 5);
            }
            
            if (GlobalBounds.Top < roomBounds.Top)
            {
                Y += (roomBounds.Top - GlobalBounds.Top + 5);
            }
            else if (GlobalBounds.Bottom > roomBounds.Bottom)
            {
                Y += (roomBounds.Bottom - GlobalBounds.Bottom - 5);
            }

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

        // methods to block player from moving in the cardinal directions. Useful if collisions end up not being handled by the player class
        public void BlockUp()
        {
            Y += 10;
        }
        public void BlockDown()
        {
            Y -= 10;
        }
        public void BlockLeft()
        {
            X += 10;
        }
        public void BlockRight()
        {
            X -= 10;
        }

    }
}