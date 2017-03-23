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
    enum PlayerDir { FaceDown, WalkDown, FaceUp, WalkUp, FaceLeft, WalkLeft, FaceRight, WalkRight}
    class Player : GameObject, Tools.IAnimated, Tools.IControlled
    {
        KeyboardState kbState;
        KeyboardState prevKbState;
        //private Rectangle[][] animFrames; // source rectangles to be used in drawing the player
        private Rectangle moveBounds;
        private Rectangle playerRect;
        private Vector2 moveQueue;
        private PlayerDir playerDirection; // get direction the player is facing
        private List<Texture2D> walkRightSprites;
        private Texture2D faceRightSprite;
        private Texture2D faceUpSprite;
        private Texture2D faceDownSprite;
        // variables for animation
        double timer = .1;
        int i = 0;

        // TODO: Player constructor, should take the same sort of information as well as potentially a Menu object. We'd feed the overall Game's Menu into that.
        public Player(Texture2D faceRight, List<Texture2D> walkRight, Texture2D faceUp, Texture2D faceDown, Rectangle bounds, Rectangle pRect):base()
        {
            moveQueue = Vector2.Zero; // initialize moveQueue to a zero vector
            playerDirection = PlayerDir.FaceDown; // start out facing downwards for now
            moveBounds = bounds;
            playerRect = pRect;
            faceRightSprite = faceRight;
            walkRightSprites = walkRight;
            faceUpSprite = faceUp;
            faceDownSprite = faceDown;
        }
        // TODO: Update method override, should check player input and movement
        public override void Update(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            CheckInput(); // first get input to adjust movement queueing
            Move(gameTime);
            CheckBounds(moveBounds);
            ChangeDirection();

            base.Update(gameTime);
        }
        public void GetFrame()
        {
            // TODO: Kat - Method should figure out which frame rectangle is supposed to be active and pass it to an attribute bounds, which will need a property or accessor method
            
        }

        public void LoadFrames()
        {
            // TODO: Kat - This method will need to be set faceUp differently depending on individual images or spritesheets.
            // If we use sprite sheets we need it to take values for the number of animation sets, the number of frames in each set, and the pixel dimensions of each frame being pulled.
            // With that said, because of how this stuff works (to my knowledge) we don't need to pull the image file itself as part of this. We can just use the player's sprite itself for references.
            // This method shouldn't actually need to touch the Texture2D, we're just using it to set faceUp rects which will be used as sources in Draw() method.
        }

        public void CheckInput()
        {

            // TODO: This method should be called by the update function, this is where keyboard state checking should go.

            moveQueue = Vector2.Zero; // reset moveQueue every update

            kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Keys.W))
            {
                moveQueue.Y -= 2;
            }
            if (kbState.IsKeyDown(Keys.S))
            {
                moveQueue.Y += 2;
            }
            if (kbState.IsKeyDown(Keys.A))
            {
                moveQueue.X -= 2;
            }
            if (kbState.IsKeyDown(Keys.D))
            {
                moveQueue.X += 2;
            }
            prevKbState = kbState;
        }

        public void Move(GameTime gameTime)
        {
            playerRect.X += (int)(moveQueue.X); //* (gameTime.ElapsedGameTime.Seconds);
            playerRect.Y += (int)(moveQueue.Y); //* (gameTime.ElapsedGameTime.Seconds);
            //Console.WriteLine(moveQueue.ToString());
            CheckBounds(moveBounds);
        }
        public void CheckProximity(GameObject target)
        {
            // TODO: Not sure this is the best way to go about it so this may be moved or altered, but this would be used to figure out if
            // the player is close enough to a usable object to interact with it. If there are multiple close by, whichever is closer should be
            // the one interacted with.
        }

        public void CheckBounds(Rectangle roomBounds)
        {
            // TODO: This method should check whether or not the player's collision rectangle is entirely inside
            // the accepted param bounds roomBounds. Correct any discrepancies.
            if (GlobalBounds.Left < roomBounds.Left)
            {
                X += (roomBounds.Left - GlobalBounds.Left + 5);
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

        // changes the PlayerDir enum based on input
        private void ChangeDirection()
        {
            if (kbState.IsKeyDown(Keys.W) && !prevKbState.IsKeyDown(Keys.W))
            {
                playerDirection = PlayerDir.FaceUp;
            }
            if (kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyDown(Keys.W))
            {
                playerDirection = PlayerDir.WalkUp;
                //moveBounds = new Rectangle(moveBounds.X, moveBounds.Y - 2, faceUpSprite.Width, faceUpSprite.Height);
            }
            if (!kbState.IsKeyDown(Keys.W) && prevKbState.IsKeyDown(Keys.W))
            {
                playerDirection = PlayerDir.FaceUp;
            }
            if (kbState.IsKeyDown(Keys.A) && !prevKbState.IsKeyDown(Keys.A))
            {
                playerDirection = PlayerDir.FaceLeft;
            }
            if (kbState.IsKeyDown(Keys.A) && prevKbState.IsKeyDown(Keys.A))
            {
                playerDirection = PlayerDir.WalkLeft;
                //moveBounds = new Rectangle(moveBounds.X - 2, moveBounds.Y, faceUpSprite.Width, faceUpSprite.Height);
            }
            if (!kbState.IsKeyDown(Keys.A) && prevKbState.IsKeyDown(Keys.A))
            {
                playerDirection = PlayerDir.FaceLeft;
            }
            if (kbState.IsKeyDown(Keys.S) && !prevKbState.IsKeyDown(Keys.S))
            {
                playerDirection = PlayerDir.FaceDown;
            }
            if (kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyDown(Keys.S))
            {
                playerDirection = PlayerDir.WalkDown;
                //moveBounds = new Rectangle(moveBounds.X, moveBounds.Y + 2, faceUpSprite.Width, faceUpSprite.Height);
            }
            if (!kbState.IsKeyDown(Keys.S) && prevKbState.IsKeyDown(Keys.S))
            {
                playerDirection = PlayerDir.FaceDown;
            }
            if (kbState.IsKeyDown(Keys.D) && !prevKbState.IsKeyDown(Keys.D))
            {
                playerDirection = PlayerDir.FaceRight;
            }
            if (kbState.IsKeyDown(Keys.D) && prevKbState.IsKeyDown(Keys.D))
            {
                playerDirection = PlayerDir.WalkRight;
                //moveBounds = new Rectangle(moveBounds.X + 2, moveBounds.Y, faceUpSprite.Width, faceUpSprite.Height);
            }
            if (!kbState.IsKeyDown(Keys.D) && prevKbState.IsKeyDown(Keys.D))
            {
                playerDirection = PlayerDir.FaceRight;
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

        // methods to block player from moving in the cardinal directions. Useful if collisions end faceUp not being handled by the player class
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

        public override void Draw(SpriteBatch sprtBtch)
        {
            //base.Draw(sprtBtch);
            if (playerDirection == PlayerDir.FaceUp)
            {
                sprtBtch.Draw(faceUpSprite, playerRect, Color.White);
            }
            if (playerDirection == PlayerDir.WalkUp)
            {
                sprtBtch.Draw(faceUpSprite, playerRect, Color.White);
            }
            if (playerDirection == PlayerDir.FaceLeft)
            {
                sprtBtch.Draw(faceRightSprite, playerRect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            }
            if (playerDirection == PlayerDir.WalkLeft)
            {
                if (timer == 0 || timer < 0)
                {
                    i++;
                    timer = .1;
                    if (i >= walkRightSprites.Count)
                    {
                        i = 0;
                    }
                }
                sprtBtch.Draw(walkRightSprites[i], playerRect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            }
            if (playerDirection == PlayerDir.FaceDown)
            {
                sprtBtch.Draw(faceDownSprite, playerRect, Color.White);
            }
            if (playerDirection == PlayerDir.WalkDown)
            {
                sprtBtch.Draw(faceDownSprite, playerRect, Color.White);
            }
            if (playerDirection == PlayerDir.FaceRight)
            {
                sprtBtch.Draw(faceRightSprite, playerRect, Color.White);
            }
            if (playerDirection == PlayerDir.WalkRight)
            {
                if (timer == 0 || timer < 0)
                {
                    i++;
                    timer = .1;
                    if (i >= walkRightSprites.Count)
                    {
                        i = 0;
                    }
                }
                sprtBtch.Draw(walkRightSprites[i], playerRect, Color.White);
            }

        }

    }
}